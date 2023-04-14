using RPG.FSM.Manager;
using UnityEngine;

namespace RPG.FSM.State.Move
{
    public sealed class IdleState : BaseState
    {
        public IdleState(StateManager stateManager, Animator animator) : base(stateManager, animator)
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
            Debug.Log(name + " update");

            if (Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "pl0000_00003"
                && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                Animator.SetFloat(AnimatorStringToHash.HorizontalVelocity, 0.01f, 0.99f, Time.deltaTime);
            }
            else
            {
                Animator.SetFloat(AnimatorStringToHash.HorizontalVelocity, 0.01f);
            }

            base.OnUpdate();
        }

        protected override void Transition()
        {
            if (!InputSystem.Instance.MoveAction.ReadValue<Vector2>().Equals(Vector2.zero))
            {
                StateManager.TransitionState(FSMState.Move);
            }

            if (!InputSystem.Instance.isGround)
            {
                StateManager.TransitionState(FSMState.Jump);
            }
        }

        public override void OnExit()
        {
            Debug.Log("exit " + name);
        }
    }
}