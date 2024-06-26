public abstract class State
{

    protected NPC npc;
    protected StateMachine stateMachine;

    protected State(NPC _npc, StateMachine _stateMachine)
    {
        this.npc = _npc;
        this.stateMachine = _stateMachine;
    }

    /*   protected void DisplayOnUI(UIManager.Alignment alignment)
      {
          UIManager.Instance.Display(this, alignment);
      } */
    public virtual void Enter()
    {
        /*         DisplayOnUI(UIManager.Alignment.Left); */
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }

}