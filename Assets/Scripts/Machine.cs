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
        string ingredientName = ingredient.name.Replace("(Clone)", "").Trim();
        Debug.Log("You put " + ingredient.name);
        ingredients.Add(ingredientName);
        Destroy(ingredient);
    }

    public void ReadIngredients()
    {
        foreach (string ingredient in ingredients)
        {
            Debug.Log("You have in the machine" + ingredient);
        }
    }

    public bool UseMachine()
    {
        recipe = null;
        // Lire le fichier CSV
        string[] lines = File.ReadAllLines(csvFilePath);

        foreach (string line in lines)
        {
            // Split the line by comma
            string[] parts = line.Split(',');

            // Check if the line is valid and has the necessary parts
            if (parts.Length < 3)
                throw new System.Exception("Invalid line in CSV: " + line);

            // Go to another line if it's not the right machine in the CSV
            if (parts[0].Trim() != this.gameObject.name)
            {
                Debug.Log("Machine name does not match.");
                continue;
            }

            string[] requiredIngredients = parts[1].Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            string recipeName = parts[2].Trim();

            // Check if the number of ingredients match
            if (requiredIngredients.Length != ingredients.Count)
            {
                Debug.Log("Wrong number of ingredients for recipe");
                continue;
            }

            // Check if all ingredients match
            bool allIngredientsMatch = true;
            foreach (string oneIngredient in requiredIngredients)
            {
                Debug.Log("THE TRUTH" + oneIngredient);
                Debug.Log("THE TRUTH" + ingredients[0]);
                if (!ingredients.Contains(oneIngredient))
                {
                    allIngredientsMatch = false;
                    break;
                }
            }

            if (allIngredientsMatch)
            {
                recipe = recipeName;
                Debug.Log("Recipe found: " + recipe);
                return true;
            }
        }

        // Si aucune recette n'est trouvée
        if(ingredients.Count > 0)
        {
            recipe = "WrongIngredients";
            return true;
        }
        
        Debug.Log("No matching recipe found.");
        return false;
    }

    public GameObject PickUpRecipe()
    {
        if(recipe != null)
        {
            Debug.Log("You picked up the recipe: " + recipe);
            return Resources.Load<GameObject>("Recipes/" + recipe);
        }
        return null;
    }

    public void ClearMachine()
    {
        ingredients.Clear();
    }

    public void UseTrash(GameObject ingredient)
    {
        Debug.Log("You put " + ingredient.name + " in the trash");
        Destroy(ingredient);
    }
}

