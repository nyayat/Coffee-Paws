using Unity.VisualScripting;

public class WaitingState : State
{

	private float period = 0.0f;
	private float max_time = 5.0f;
	public WaitingState(NPC _npc, StateMachine _stateMachine) : base(_npc, _stateMachine) { }
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
		if (period > max_time)
		{

			npc.ChangeMood();
			period = 0;

		}
		period += UnityEngine.Time.deltaTime;
		if (npc.served)
		{
			npc.Pay();
			stateMachine.ChangeState(npc.leaving);
		}
		else if (npc.mood > 2) { stateMachine.ChangeState(npc.leaving); }

	}
	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}



}