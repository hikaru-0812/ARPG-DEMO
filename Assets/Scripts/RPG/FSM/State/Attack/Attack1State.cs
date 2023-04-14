using System;
using System.Threading.Tasks;
using RPG.FSM.Manager;
using UnityEngine;

namespace RPG.FSM.State.Attack
{
    public sealed class Attack1State : BaseState
    {
        public Attack1State(StateManager stateManager, Animator animator, Action onExitCallBack = null) : base(stateManager, animator)
        {
            name = ToString();
            OnExitCallBack += onExitCallBack;
        }
        
        public override void OnEnter()
        {
            Debug.Log("enter " + name);
        }

        public override void  OnUpdate()
        {
            Debug.Log(name + " update");
            base.OnUpdate();
        }

        protected override async void Transition()
        {
            if (InputSystem.Instance.AttackAction.WasPressedThisFrame())
            {
                StateManager.TransitionState(FSMState.Attack2);
            }
            else
            {
                await Task.Delay(2000);
                StateManager.TransitionState(FSMState.Idle);
            }
        }

        public override void OnExit()
        {
            Debug.Log("exit " + name);
            OnExitCallBack?.Invoke();
        }
    }
}