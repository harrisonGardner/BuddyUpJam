using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public string mainText = "What the item is";
    //public string subText = "About the item stuff goes here";

    public List<string> interactionTextList = new List<string>();
    public List<string> interactionTextListPostMessages = new List<string>();

    public int disappearAfterLevel = 4;

    private IInteraction interaction;

    // Start is called before the first frame update
    void Start()
    {
        interaction = GetComponent<IInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (LevelManager.GetLevel() >= disappearAfterLevel)
        {
            gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if(interaction != null)
        {
            interaction.Interact();
        }
    }

    public string GetInteractionText(int level)
    {
        level = Mathf.Clamp(level, 0, interactionTextList.Count - 1);

        if (interactionTextListPostMessages.Count == interactionTextList.Count)
            return (LevelManager.messagesRead ? (interactionTextListPostMessages[level] != "" ? interactionTextListPostMessages[level] : interactionTextList[level] ) : interactionTextList[level]); // I should really stop nesting ternary operators
        else
            return interactionTextList[level];
    }
}
