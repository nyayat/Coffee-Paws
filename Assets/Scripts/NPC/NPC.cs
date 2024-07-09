using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.IO;


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
	
	public GameObject food
	{
		get { return m_food; }
		
	}
	private GameObject m_food;
	
	private string m_pathRecipes = "Assets/Resources/Recipes/Finished";

	public GameObject m_CameraNPC;
	public GameObject m_PrefabThoughtCanvas;
	
	private GameObject m_ThoughtCanvas;
	private CameraNPC m_camNPC;
	private GameObject m_goCamNPC;

	void CameraPreparation(){
		GameObject ThoughtUI = GameObject.Find("ThoughtUI");
		// ThoughtCanvas instantiation
		m_ThoughtCanvas = Instantiate(m_PrefabThoughtCanvas, transform.position, Quaternion.identity);
		m_ThoughtCanvas.transform.SetParent(ThoughtUI.transform, false);
		
		// CamNPC instantiation
		GameObject m_goCamNPC= (Instantiate(m_CameraNPC, transform.position, Quaternion.identity));
		m_camNPC =		m_goCamNPC.GetComponent<CameraNPC>();
		(m_ThoughtCanvas.GetComponent<Canvas>()).worldCamera = m_camNPC.GetComponent<UnityEngine.Camera>();
		m_camNPC.SetTarget(this.transform);
		//m_goCamNPC.transform.SetParent(transform, false);
		
		

		//m_ThoughtCanvas.transform
	}

	// Start is called before the first frame update
	void Start()
	{

		CameraPreparation();

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
		
		//on cherche toutes les recettes dans ce dossier
		DirectoryInfo d = new DirectoryInfo(m_pathRecipes);
		var files = d.GetFiles("*.prefab");
		
		int NRecipe = UnityEngine.Random.Range(0, files.Length);
		var file = files[NRecipe];
		
		
		//
		GameObject _prefabFood = Resources.Load<GameObject>("Recipes/Finished/" + Path.GetFileNameWithoutExtension(file.Name)) as GameObject;

		m_food = GameObject.Instantiate(_prefabFood, new Vector3(0.779999971f,3.3900001f,-2.69000006f), Quaternion.identity);
		//		m_food.transform.localScale = (new Vector3(0.15f, 0.15f, 0.15f));
		//m_food.transform.localScale.z *= 0.15f;

		/*
		UnityEditor.TransformWorldPlacementJSON:
		{"position":{"x":6.037570476531982,"y":1.5444457530975342,"z":-9.357662200927735},"rotation":{"x":-0.7071068286895752,"y":0.0,"z":0.0,"w":0.7071068286895752},"scale":{"x":16.149999618530275,"y":8.699999809265137,"z":14.819999694824219}}
		Vector3(0.779999971,3.70000005,-2.24000001)
		Vector3(270,0,0)
		Vector3(16.1499996,8.69999981,14.8199997)
		*/
		m_food.transform.position = new Vector3(0.779999971f,3.70000005f,-2.24000001f);
		m_food.transform.eulerAngles = new Vector3(270f,0f,0f);
/* 		m_food.transform.localScale = new Vector3(m_food.transform.localScale.x * (1/2),m_food.transform.localScale.y * (1/2),0.2f); */
		m_food.transform.localScale = new Vector3(16.1499996f,8.69999981f,14.8199997f);
		m_food.transform.SetParent(m_ThoughtCanvas.transform, false);
		
		

		Debug.Log("Je veux manger... " + m_food);

	}
	public void StopThinking()
	{
		Debug.Log("I stop thinkin");
	
		Debug.Log("food : "+m_food);
	if (m_food != null)
		  Debug.Log("Je pense plus");
          Destroy(m_food);
		  //UnityEngine.Object.DestroyImmediate(m_food);
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
		/* Debug.Log("J'y vais..."); */
		agent.destination = new Vector3(7.5999999f, 0f, -23.2900009f);
	}

	public void DestroyNPC()
	{

		Destroy(this.m_goCamNPC);
		Destroy(this.m_camNPC);
		Destroy(this.m_ThoughtCanvas);
		Destroy(this.gameObject);
	}
	/* --------- End Leaving state --------- */

	/* --------- Begin  state --------- */
	/* --------- End  state --------- */

}