public class ThinkingState : State
{
    public ThinkingState(NPC _npc, StateMachine _stateMachine) : base(_npc, _stateMachine) { }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {

        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        npc.Think();

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}