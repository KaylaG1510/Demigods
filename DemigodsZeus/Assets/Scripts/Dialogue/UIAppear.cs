using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    [SerializeField] private Image textHolder;
    [SerializeField] private Text reaperDialogue;

    [Header("Character Image")]
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;

    private void Awake()
    {
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textHolder.enabled = true;
            reaperDialogue.enabled = true;
            imageHolder.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textHolder.enabled = false;
            reaperDialogue.enabled = false;
            imageHolder.enabled = false;
        }
    }
}
