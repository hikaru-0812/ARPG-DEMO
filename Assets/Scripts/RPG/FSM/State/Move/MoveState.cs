using RPG.FSM.Manager;
using UnityEngine;

namespace RPG.FSM.State.Move
{
    public sealed class MoveState : BaseState
    {
        public MoveState(StateManager stateManager, Animator animator) : base(stateManager, animator)
        {
            name = ToString();
        }

        public override void OnEnter()
        {
            Debug.Log("enter " + name);
            
            Animator.SetFloat(AnimatorStringToHash.PlayerAttitude, 0.0f);
        }

        public override void OnUpdate()
        {
            Animator.SetFloat(AnimatorStringToHash.HorizontalVelocity, InputSystem.Instance.TargetSpeed, 0.05f, Time.deltaTime);
            Debug.Log(name + " update");
            
            base.OnUpdate();
        }

        protected override void Transition()
        {
            if (InputSystem.Instance.MoveAction.ReadValue<Vector2>().Equals(Vector2.zero))
            {
                StateManager.TransitionState(FSMState.Idle);
            }
            
            if (!InputSystem.Instance.isGround)
            {
                StateManager.TransitionState(FSMState.Jump);
            }
        }

        public override void OnExit()
        {
            Animator.SetFloat(AnimatorStringToHash.HorizontalVelocity, 0.2f);
            
            Debug.Log("exit " + name);
        }
    }
}