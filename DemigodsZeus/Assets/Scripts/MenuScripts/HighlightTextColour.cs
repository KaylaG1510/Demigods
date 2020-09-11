using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightTextColour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text theText;

    //?Burnt Orange
    private Color32 highlightColour = new Color32(207, 78, 8, 255);

    public GameObject t;
    public float posX;
    public float posY;

    void Start()
    {
        t.GetComponent<Renderer>().enabled = false;
    }

    //When mouse hovers over button
    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = highlightColour;

        t.transform.position = new Vector3(posX, posY, 0);
        t.GetComponent<Renderer>().enabled = true;
    }

    //When mouse stops hovering over button
    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.white;
        t.GetComponent<Renderer>().enabled = false;
    }

    //Button disabled
    public void OnDisable()
    {
        theText.color = Color.white;
        t.GetComponent<Renderer>().enabled = false;
    }
}
