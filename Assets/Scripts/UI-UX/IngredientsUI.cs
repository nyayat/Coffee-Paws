using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class IngredientsUI : MonoBehaviour
{

	public string pathIngredients = "Assets/Resources/Foods";

	public GameObject prefabButton;
	public RectTransform ParentPanel;
	void Start()
	{
		DirectoryInfo d = new DirectoryInfo(pathIngredients);
		foreach (var file in d.GetFiles("*.prefab"))
		{
			Debug.Log("/Foods/" + file.Name);
			//
			GameObject goButton = Instantiate(prefabButton);
			goButton.transform.SetParent(ParentPanel, false);

			var button = GetComponent<UnityEngine.UI.Button>();
			button.onClick.AddListener(() => FooOnClick(file.Name));

			var _prefab = Resources.Load<GameObject>("Foods/" + Path.GetFileNameWithoutExtension(file.Name)) as GameObject;

			GameObject goPrefabIngredient = GameObject.Instantiate(_prefab);
			goPrefabIngredient.transform.SetParent(goButton.transform, false);
		}
	}

	void Update()
	{

	}
	void FooOnClick(string test)
	{
		Debug.Log("FooOnClick");
		Debug.Log(test);
	}
}
