using UnityEngine;

public class ReaperDialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;

    // Method to activate dialogue
    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }

    // Method for when the dialogue is active 
    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
    }
}
