using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private Text textHolder;
        [SerializeField] private string input;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder));

        }

    }

}
