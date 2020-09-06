using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightTextColour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text theText;
    //?Burnt orange
    private Color32 highlightColour = new Color32(207, 78, 8, 255);

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = highlightColour;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.white;
    }
}
