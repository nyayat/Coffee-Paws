using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tables : MonoBehaviour
{
    public Transform tables;
    private static int numTable = -1;
    private static List<bool> table_takes = new List<bool>();
    // Awake is called before Start
    void Awake()
    {
        numTable = tables.childCount;
        table_takes = new List<bool>(new bool[numTable]);
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        /*   for (int i = 0; i < table_takes.Count; i++)
          {
              if (table_takes[i])
                  Debug.Log("La table n°" + (i + 1) + "est prise");
          } */
    }

    void takeTable(int n)
    {
        table_takes[n] = true;
    }


    void freeTable(int n)
    {
        table_takes[n] = false;
    }

    bool isFree(int n)
    {

        return !table_takes[n];
    }

    public bool reserveTable(int n)
    {
        /*  Debug.Log("NB DE TABLE  : " + numTable);
         Debug.Log("LA TAILLE DE TABLE TAKES : " + table_takes.Count);
         Debug.Log("\n N : " + n); */
        if (isFree(n))
        {
            takeTable(n);
            return true;
        }
        return false;
    }

    public Transform getTables()
    {
        return tables;
    }

    public int getNumTable()
    {
        return numTable;
    }


}

