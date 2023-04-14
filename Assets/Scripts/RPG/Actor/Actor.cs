using RPG.FSM.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Actor
{
    public class Actor : MonoBehaviour
    {
        public GameObject weapon;
    
        private Animator _animator;

        private CharacterController _characterController;
        
        private InputActionAsset _inputActionAsset;

        private const float GravitationalAcceleration = -9.8f;
        private const float JumpVelocity = 8.0f;
        private float _verticalVelocity;
        
        private Transform _playerTransform;
        private Vector2 _playerInputVec;
        private Vector3 _playerMovement = Vector3.zero;
        private bool _isRunning;
        private bool _isJumping;
        private float _currentSpeed;
        private float _targetSpeed;
        private const float WalkSpeed = 1.5f;
        private const float RunSpeed = 3.5f;
        private const int CacheSize = 3;
        private readonly Vector3[] _velocityCache = new Vector3[CacheSize];
        private int _currentCacheIndex;
        private Vector3 _averageVelocity;
        private const float FallMultiplier = 1.5f;
        private bool _isGrounded;
        private const float GroundCheckOffset = 0.5f;

        private Transform _cameraTransform;
        
        private StateManager _stateManager;
        private int _attackNumber;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _characterController = GetComponent<CharacterController>();

            _inputActionAsset = GetComponent<PlayerInput>().actions;
            InputSystem.Instance.MoveAction = _inputActionAsset.FindAction("Move");
            InputSystem.Instance.AttackAction = _inputActionAsset.FindAction("Attack");
            InputSystem.Instance.MoveAction.Enable();
            InputSystem.Instance.AttackAction.Enable();

            _playerTransform = transform;
            if (Camera.main != null)
            {
                _cameraTransform = Camera.main.transform;
            }

            _stateManager = new StateManager(_animator);
        }

        private void Start()
        {
            InputSystem.Instance.MoveAction.performed += GetMoveInput;
            // InputSystem.Instance.AttackAction.performed += ;
            
            // _moveAction.performed += (ctx) =>
            // {
            //     if (ctx.ReadValue<Vector2>())
            //     {
            //         stateManager.TransitionState(FSMState.WALK);
            //     }
            // };
            //
            // _attackAction.performed += (ctx) =>
            // {
            //     attackNumber++;
            //     if (attackNumber > 3)
            //     {
            //         attackNumber = 0;
            //     }
            // };
            
            weapon.SetActive(false);
        }

        private void Update()
        {
            CheckGround();
            _stateManager.Update();
            CalculateVerticalVelocity();
            Jump();
            PlayerRotate();
            PlayerMove();
        }

        private void OnAnimatorMove()
        {
            if (_stateManager.GetCurrentState() == FSMState.Jump)
            {
                // 使用落地前几帧的平均速度移动
                _averageVelocity.y = _verticalVelocity;
                var playerDeltaMovement = _averageVelocity * Time.deltaTime;
                _characterController.Move(playerDeltaMovement);
            }
            else
            {
                var playerDeltaMovement = _animator.deltaPosition;
                playerDeltaMovement.y = _verticalVelocity * Time.deltaTime;
                _characterController.Move(playerDeltaMovement);
                CalculateAverageVelocity(_animator.velocity);
            }
        }

        public void GetMoveInput(InputAction.CallbackContext ctx)
        {
            _playerInputVec = ctx.ReadValue<Vector2>();
        }
        
        public void GetRunInput(InputAction.CallbackContext ctx)
        {
            _isRunning = ctx.ReadValue<float>() > 0;
        }
        
        public void GetJumpInput(InputAction.CallbackContext ctx)
        {
            _isJumping = ctx.ReadValue<float>() > 0;
        }

        private void CalculateVerticalVelocity()
        {
            InputSystem.Instance.isGround = _isGrounded;
            
            if (_isGrounded)
            {
                _verticalVelocity = GravitationalAcceleration * Time.deltaTime;
            }
            else
            {
                
                if (_verticalVelocity <= 0)// 下降阶段
                {
                    _verticalVelocity += GravitationalAcceleration * FallMultiplier * Time.deltaTime;
                }
                else // 上升阶段
                {
                    _verticalVelocity += GravitationalAcceleration * Time.deltaTime;
                }
                
            }
            
            InputSystem.Instance.VerticalVelocity = _verticalVelocity;
        }

        private void CalculateAverageVelocity(Vector3 newVelocity)
        {
            _velocityCache[_currentCacheIndex] = newVelocity;
            _currentCacheIndex++;

            // 取模防止溢出
            _currentCacheIndex %= CacheSize;

            var average = Vector3.zero;
            foreach (var vel in _velocityCache)
            {
                average += vel;
            }

            _averageVelocity = average / CacheSize;
        }

        private void Jump()
        {
            if (_isGrounded && _isJumping)
            {
                _verticalVelocity = JumpVelocity;
            }
        }

        private void CheckGround()
        {
            var radius = _characterController.radius;
            _isGrounded = Physics.SphereCast(_playerTransform.position + (Vector3.up * GroundCheckOffset),
                radius, Vector3.down, out _,
                GroundCheckOffset - radius + _characterController.skinWidth * 2);

            InputSystem.Instance.isGround = _isGrounded;
        }

        private void PlayerRotate()
        {
            if (_playerInputVec.Equals(Vector2.zero))
            {
                _playerMovement = Vector3.zero;
                return;
            }
            
            var cameraForward = _cameraTransform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();
            
            // 运动方向受输入方向和相机方向共同影响
            var targetRotation = Quaternion.LookRotation(
                Quaternion.LookRotation(new Vector3(_playerInputVec.x, 0, _playerInputVec.y)) * cameraForward);
            _playerTransform.rotation =
                Quaternion.Slerp(_playerTransform.rotation, targetRotation, 10.0f * Time.deltaTime);

            _playerMovement = targetRotation * Vector3.forward;
        }

        private void PlayerMove()
        {
            _targetSpeed = _isRunning ? RunSpeed : WalkSpeed;
            _targetSpeed *= _playerMovement.magnitude;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, 0.9f);
            InputSystem.Instance.TargetSpeed = _targetSpeed;
        }
    }
}