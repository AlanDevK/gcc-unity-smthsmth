using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;

    public void Interact(){
        dialogueBox.SetActive(true);
    }

    public void NotInteract(){
        dialogueBox.SetActive(false);
    }
}
