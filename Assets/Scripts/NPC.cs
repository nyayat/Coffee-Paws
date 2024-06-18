using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

    public Tables tables;
    private Transform table;
    private NavMeshAgent agent;
    private int rand;
    private int table_take = -1;

    // Start is called before the first frame update
    void Start()
    {

        rand = UnityEngine.Random.Range(0, tables.numTable);
        Debug.Log(rand);
        agent = GetComponent<NavMeshAgent>();
    }

    void Awake()
    {
    }
    // Update is called once per frame

    void Update()
    {
        /* for (int i = 0; i < tables.childCount; i++)
        {
            Debug.Log(tables.GetChild(i));
        } */
        if (table_take < 0) this.reserveTable();
        agent.destination = tables.getTables().GetChild(table_take).position;
    }

    void reserveTable()
    {
        while (table_take < 0)
        {
            if (tables.reserveTable(rand))
            {
                table_take = rand;
            }
            else
            {
                rand = UnityEngine.Random.Range(0, tables.numTable);
            }
        }
    }

}

