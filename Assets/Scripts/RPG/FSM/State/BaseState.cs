using System;
using RPG.FSM.Base;
using RPG.FSM.Manager;
using UnityEngine;

namespace RPG.FSM.State
{
    public abstract class BaseState : IState
    {
        protected string name;

        protected readonly StateManager StateManager;

        protected readonly Animator Animator;

        protected BaseState(StateManager stateManager, Animator animator)
        {
            StateManager = stateManager;
            Animator = animator;
        }

        public abstract void OnEnter();

        public virtual void OnUpdate()
        {
            Transition();
        }

        protected abstract void Transition();

        public abstract void OnExit();
        protected Action OnExitCallBack;
    }
}