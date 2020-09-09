using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterHighlight : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    //When mouse hovers over button
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("onHover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.ResetTrigger("onHover");
    }
}
