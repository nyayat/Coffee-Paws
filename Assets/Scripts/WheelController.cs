using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public Animator anim;
    private bool wheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int ingredientId;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            wheelSelected = !wheelSelected;
        }

        if(wheelSelected)
        {
            anim.SetBool("OpenWheel", true);
        }
        else
        {
            anim.SetBool("OpenWheel", false);
        }

        switch(ingredientId)
        {
            case 0: // nothing is selected
                selectedItem.sprite = noImage;
                break;
            case 1: //Butter
                Debug.Log("Butter");
                break;
            case 2: //Flour
                Debug.Log("Flour");
                break;
            case 3: //Chocolate
                Debug.Log("Chocolate");
                break;
        }

        
    }
}
