namespace RPG.FSM.Base
{
    public interface IState
    { 
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}
