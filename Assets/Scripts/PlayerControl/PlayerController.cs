using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        private Transform _playerNode;
        private CharacterController _characterController;
        private Animator _animator;

        private PlayerInput _playerInput;
        private PUNISHING _inputAction;
        private Vector2 moveInput;
        private static readonly int Property = Animator.StringToHash("Speed");

        private void OnEnable()
        {
            _inputAction?.Enable();
        }
        
        private void OnDisable()
        {
            _inputAction?.Disable();
        }

        private void Update()
        {
            _animator.SetFloat(Property, moveInput.magnitude);
        }

        private void OnAnimatorMove()
        {
            _characterController.SimpleMove(_animator.velocity);
        }

        public void Init(Transform playerNode, Animator animator, CharacterController characterController)
        {
            _playerNode = playerNode;
            _animator = animator;
            _characterController = characterController;
            _playerInput = playerNode.GetComponent<PlayerInput>();

            _inputAction = new PUNISHING();
            _playerInput.actions = _inputAction.asset;

            _inputAction.Player.Move.performed += GetMoveInput;
        }

        #region 输入响应相关

        private void GetMoveInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        #endregion
    }
}