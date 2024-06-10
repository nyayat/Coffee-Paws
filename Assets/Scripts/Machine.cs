using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Machine : MonoBehaviour
{
    private List<string> ingredients = new List<string>(); // Utilisation d'une liste pour ajouter des ingrédients
    private string recipe;
    public string csvFilePath = "Assets/CSV/recipes.csv";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutIngredient(GameObject ingredient)
    {
        Debug.Log("You put " + ingredient.name);
        ingredients.Add(ingredient.name);
        Destroy(ingredient);
    }

    public void ReadIngredients()
    {
        foreach (string ingredient in ingredients)
        {
            Debug.Log("You have in the machine" + ingredient);
        }
    }

    /*void UseMachine()
    {
        // Lire le fichier CSV
        string[] lines = File.ReadAllLines(csvFilePath);

        foreach (string line in lines)
        {
            // Split the line by comma
            string[] parts = line.Split(',');

            // Check if the line is valid and has the necessary parts
            if (parts.Length < 3)
                throw new System.Exception("Invalid line in CSV: " + line);

            // Get the machine name, ingredients, and recipe
            if(parts[0].Trim() != this.gameObject.name)
                continue;

            string[] requiredIngredients = parts[1].Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            string recipeName = parts[2].Trim();

            // Check if the ingredients match
            bool allIngredientsMatch = false;

            if(requiredIngredients.Length == ingredients.Count)
            {
                foreach (string oneIngredient in requiredIngredients)
                {
                    if(!ingredients.Contains(oneIngredient))
                    {
                        recipe = "WrongIngredients";
                        return;
                    }
                }
            }
            
            recipe = recipeName;
            Debug.Log("Recipe found: " + recipe);
            return;
        }
        // Si aucune recette n'est trouvée
        Debug.Log("No matching recipe found.");
    }*/
}
