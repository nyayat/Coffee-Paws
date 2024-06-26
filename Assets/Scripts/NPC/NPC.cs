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
    private Tables tables;
    private NavMeshAgent agent;
    private int tableTook = -1;


    // Start is called before the first frame update
    void Start()
    {


        /* --- Init --- */
        tables = FindObjectOfType<Tables>();
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

    public int GetTableTook()
    {
        return this.tableTook;
    }

    public void BookTable()
    {
        if (tableTook < 0)
        {
            tableTook = tables.BookTable();
        }
        /*  else
         {
             Debug.Log("Tu as déjà une table");
         } */
    }

    public void setTables(Tables tables)
    {
        this.tables = tables;
    }

    /* --------- End Reserving state --------- */

    /* --------- Begin Walking state --------- */
    public void Destination()
    {
        agent.destination = tables.GetTables().GetChild(tableTook).position;
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
    /* --------- End Waiting state --------- */



    /* --------- Begin Leaving state --------- */
    /* --------- End Leaving state --------- */

    /* --------- Begin  state --------- */
    /* --------- End  state --------- */

}