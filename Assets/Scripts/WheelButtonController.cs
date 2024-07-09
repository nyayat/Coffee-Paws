using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheelButtonController : MonoBehaviour
{
	public int id;
	private Animator anim;
	public Image selectedItem;
	private bool selected = false;
	public Sprite icon;
	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update()
	{

		if (selected)
		{
			Debug.Log("Selected");
			selectedItem.sprite = icon;
		}

	}

	public void Selected()
	{
		selected = true;
		WheelController.ingredientId = id;
	}

	public void Deselected()
	{
		selected = false;
		WheelController.ingredientId = 0;
	}

	public void HoverEnter()
	{
		anim.SetBool("Hover", true);
	}

	public void HoverExit()
	{
		anim.SetBool("Hover", false);
	}
}
