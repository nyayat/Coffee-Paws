/* using System.Collections;
using System.Collections.Generic;
using System.IO; */

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
public class Tables : MonoBehaviour
{
    public Transform tables;
    private static int numTable = -1;
    private static List<bool> tableTook = new List<bool>();
    // Awake is called before Start
    void Awake()
    {
        numTable = tables.childCount;
        tableTook = new List<bool>(new bool[numTable]);
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {

    }

    int TakeTable()
    {
        int idx = tableTook.IndexOf(false);
        tableTook[idx] = true;
        return idx;
    }


    void FreeTable(int n)
    {
        tableTook[n] = false;
    }
    bool AnyFreeTable()
    {
        return tableTook.Contains(false);
    }


    public int BookTable()
    {
        /*  Debug.Log("NB DE TABLE  : " + numTable);
         Debug.Log("LA TAILLE DE TABLE TAKES : " + tableTook.Count);
         Debug.Log("\n N : " + n); */
        if (AnyFreeTable())
        {
            int index = TakeTable();

            Debug.Log("La table n°" + index + " est libre");
            return index;
        }
        /* Debug.Log("Pas de table libre.."); */
        return -1;
    }

    public Transform GetTables()
    {
        return tables;
    }

    public int GetNumTable()
    {
        return numTable;
    }


}

