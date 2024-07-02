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
    public Transform tables
    {
        get { return m_tables; }
    }
    public Transform m_tables;

    public static int numTable
    {
        get { return m_numTable; }
    }
    private static int m_numTable = -1;
    private static List<bool> m_tableTook = new List<bool>();
    // Awake is called before Start
    void Awake()
    {
        m_numTable = m_tables.childCount;
        m_tableTook = new List<bool>(new bool[m_numTable]);
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
        int idx = m_tableTook.IndexOf(false);
        m_tableTook[idx] = true;
        return idx;
    }


    public void FreeTable(int n)
    {
        m_tableTook[n] = false;
    }
    bool AnyFreeTable()
    {
        return m_tableTook.Contains(false);
    }


    public int BookTable()
    {
        if (AnyFreeTable())
        {
            int index = TakeTable();

            Debug.Log("La table n°" + index + " est libre");
            return index;
        }
        /* Debug.Log("Pas de table libre.."); */
        return -1;
    }




}

