using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDialogue : MonoBehaviour
{
    private GameObject dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.Find("DialogueHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            dialogueBox.SetActive(true);
        }

        if (Time.timeScale == 0)
        {
            dialogueBox.SetActive(false);
        }
    }
}
