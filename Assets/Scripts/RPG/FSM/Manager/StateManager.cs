using System.Collections.Generic;
using RPG.FSM.State;
using RPG.FSM.State.Attack;
using RPG.FSM.State.Jump;
using RPG.FSM.State.Move;
using UnityEngine;

namespace RPG.FSM.Manager
{
    public class StateManager
    {
        private readonly Dictionary<FSMState, BaseState> _states = new Dictionary<FSMState, BaseState>(6);
        private FSMState _currentStateKey;
        private BaseState _currentState;

        public StateManager(Animator animator)
        {
            _currentState = new IdleState(this, animator);
            _currentStateKey = FSMState.Idle;
            
            _states.Add(FSMState.Idle, _currentState);
            _states.Add(FSMState.Move, new MoveState(this, animator));
            _states.Add(FSMState.Jump, new JumpState(this, animator));
            _states.Add(FSMState.Attack1, new Attack1State(this, animator));
            _states.Add(FSMState.Attack2, new Attack2State(this, animator));
            _states.Add(FSMState.Attack3, new Attack3State(this, animator));
            
            _currentState.OnEnter();
        }

        public void TransitionState(FSMState targetState)
        {
            if (_currentStateKey == targetState)
            {
                return;
            }
            
            _currentState?.OnExit();
            if (!_states.TryGetValue(targetState, out _currentState))
            {
                return;
            }
            _currentStateKey = targetState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState.OnUpdate();
        }
        
        public void AddState(FSMState stateKey, BaseState state)
        {
            if (state == null)
            {
                return;
            }
            
            if(!_states.ContainsKey(stateKey))
            {
                _states.Add(stateKey, state);
            }
        }
        
        public void DeleteState(FSMState stateKey)
        {
            if(_states.ContainsKey(stateKey))
            {
                _states.Remove(stateKey);
            }
        }

        public FSMState GetCurrentState()
        {
            return _currentStateKey;
        }
    }
}