using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Machine : MonoBehaviour
{
    private List<string> ingredients = new List<string>(); // Utilisation d'une liste pour ajouter des ingrédients
    private string recipe;
    public string csvFilePath = "Assets/recipes.csv";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void putIngredient(string ingredient)
    {
        ingredients.Add(ingredient);
    }
}
