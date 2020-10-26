using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{

    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        // Parameters of all values needed in the inspector 
        protected IEnumerator WriteText(string input, Text textHolder, float delay, float delayBetweenLines, Font textFont)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            // Lets user go through dialogue with left click on mouse 
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;
        }
    }
}
