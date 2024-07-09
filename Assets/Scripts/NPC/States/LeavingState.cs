public class LeavingState : State
{
	public LeavingState(NPC _npc, StateMachine _stateMachine) : base(_npc, _stateMachine) { }
	public override void Enter()
	{
		base.Enter();
		npc.FreeTable();
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

		npc.GoodBye();
		if (npc.Arrived()) {npc.DestroyNPC(); }
	}
	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}