using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
	// States
	public StateMachine movementSM;
	public LeavingState leaving;
	public BookingState booking;
	public WalkingState walking;
	public ThinkingState thinking;
	public WaitingState waiting;

	//Variables
	public Tables tables
	{
		get { return m_tables; }
		set { m_tables = value; }
	}
	private Tables m_tables;
	/*------------------------*/
	private NavMeshAgent agent;
	/*------------------------*/
	public int tableTook
	{
		get { return m_tableTook; }
		set { m_tableTook = value; }
	}
	private int m_tableTook = -1;
	/*------------------------*/
	public bool served
	{
		get { return m_served; }
	}
	private bool m_served = false;
	/*------------------------*/
	public int mood
	{
		get { return m_mood; }
	}
	private int m_mood = 0;
	Vector3 initPos;


	// Start is called before the first frame update
	void Start()
	{


		/* --- Init --- */
		m_tables = FindObjectOfType<Tables>();
		agent = GetComponent<NavMeshAgent>();

		/* --- States --- */
		movementSM = new StateMachine();


		booking = new BookingState(this, movementSM);
		waiting = new WaitingState(this, movementSM);
		leaving = new LeavingState(this, movementSM);
		thinking = new ThinkingState(this, movementSM);
		walking = new WalkingState(this, movementSM);

		movementSM.Initialize(booking);




	}

	void Awake()
	{
	}
	// Update is called once per frame

	void Update()
	{


		movementSM.CurrentState.HandleInput();

		movementSM.CurrentState.LogicUpdate();




	}

	private void FixedUpdate()
	{
		movementSM.CurrentState.PhysicsUpdate();
	}


	/* --------- Begin Reserving state --------- */



	public void BookTable()
	{
		if (m_tableTook < 0)
		{
			m_tableTook = m_tables.BookTable();
		}
	}
	/* 

		/* --------- End Reserving state --------- */

	/* --------- Begin Walking state --------- */
	public void Destination()
	{
		agent.destination = m_tables.tables.GetChild(m_tableTook).position;
	}

	public bool Arrived()
	{
		if (agent.pathPending)
			return false;
		return agent.remainingDistance <= agent.stoppingDistance;
	}
	/* --------- End Walking state --------- */


	/* --------- Begin Thinking state --------- */
	public void Think()
	{
		Debug.Log("Je pense à ce que je vais manger...");
	}

	/* --------- End Thinking state --------- */


	/* --------- Begin Waiting state --------- */
	public void ChangeMood()
	{
		/*0 = normal | 1 = ennuyé | 2 = faché => la prochaine fois part */

		Debug.Log("Je ne suis plus ... " + mood);
		this.m_mood++;
	}
	public void Serve()
	{
		this.m_served = true;
	}


	public void Pay()
	{
		Debug.Log("J'ai payé");
	}



	/* --------- End Waiting state --------- */



	/* --------- Begin Leaving state --------- */
	public void FreeTable()
	{
		m_tables.FreeTable(m_tableTook);
		Debug.Log("J'ai libéré la table n°" + m_tableTook);
		m_tableTook = -1;

	}
	public void GoodBye()
	{
		Debug.Log("J'y vais...");
		agent.destination = new Vector3(7.5999999f, 0f, -23.2900009f);
	}

	public void Destroy()
	{
		UnityEngine.Object.DestroyImmediate(this);
	}
	/* --------- End Leaving state --------- */

	/* --------- Begin  state --------- */
	/* --------- End  state --------- */

}