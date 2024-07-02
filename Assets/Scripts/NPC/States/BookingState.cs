public class BookingState : State
{

    public BookingState(NPC _npc, StateMachine _stateMachine) : base(_npc, _stateMachine) { }
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
        npc.BookTable();
        if (npc.tableTook >= 0) stateMachine.ChangeState(npc.walking);

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}