using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public string mainText = "What the item is";
    //public string subText = "About the item stuff goes here";

    public List<string> interactionTextList = new List<string>();

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

    public void Interact()
    {
        if(interaction != null)
        {
            interaction.Interact();
        }
    }

    public string GetInteractionText(int level)
    {
        if (level < interactionTextList.Count && level >= 0)
        {
            return interactionTextList[level];
        }
        return interactionTextList[interactionTextList.Count - 1];
    }
}
