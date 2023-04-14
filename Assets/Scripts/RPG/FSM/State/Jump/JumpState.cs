using RPG.FSM.Manager;
using UnityEngine;

namespace RPG.FSM.State.Jump
{
    public sealed class JumpState : BaseState
    {
        public JumpState(StateManager stateManager, Animator animator) : base(stateManager, animator)
        {
            name = ToString();
        }

        public override void OnEnter()
        {
            Debug.Log("enter " + name);
            
            InputSystem.Instance.DisableInput();
            Animator.SetFloat(AnimatorStringToHash.PlayerAttitude, 1.1f);
            Animator.SetFloat(AnimatorStringToHash.VerticalVelocity, InputSystem.Instance.VerticalVelocity);
        }

        public override void OnUpdate()
        {
            Debug.Log(name + " update");
            Animator.SetFloat(AnimatorStringToHash.VerticalVelocity, InputSystem.Instance.VerticalVelocity, 0.9f, Time.deltaTime);

            base.OnUpdate();
        }

        protected override void Transition()
        {
            if (InputSystem.Instance.VerticalVelocity < -9.8f && 
                Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "pl0000_00018" &&
                Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
            {
                StateManager.TransitionState(FSMState.Idle);
            }
        }

        public override void OnExit()
        {
            Debug.Log("exit " + name);
            
            InputSystem.Instance.EnableInput();
        }
    }
}