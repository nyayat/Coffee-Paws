using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{

    private Tables tables;
    private NavMeshAgent agent;
    private int rand;
    private int table_take = -1;

    // Start is called before the first frame update
    void Start()
    {
        tables = FindObjectOfType<Tables>();
        rand = UnityEngine.Random.Range(0, tables.getNumTable());
        Debug.Log(rand);
        agent = GetComponent<NavMeshAgent>();

        if (table_take < 0) this.reserveTable();
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

        Debug.Log("J'AI PRIS LA TABLE " + table_take);
        Debug.Log("getchild " + tables.getTables().GetChild(table_take).position);
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
                rand = UnityEngine.Random.Range(0, tables.getNumTable());
            }
        }
    }

    public void setTables(Tables tables)
    {
        this.tables = tables;
    }

}

