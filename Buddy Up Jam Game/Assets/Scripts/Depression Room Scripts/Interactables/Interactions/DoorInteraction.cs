using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        if (LevelManager.messagesRead && LevelManager.GetLevel() <= LevelManager.amountOfLevels && LevelManager.readyToLeave)
        {
            SceneFade.Instance.GameEndTransition();
        }
    }

    public string GetInteractionInstructions()
    {
        if (LevelManager.messagesRead && LevelManager.GetLevel() <= LevelManager.amountOfLevels && LevelManager.readyToLeave)
            return "E to go OutSide";
        else
            return "";
    }
}
