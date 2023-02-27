using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedInteraction : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        if (LevelManager.messagesRead && LevelManager.GetLevel() < LevelManager.amountOfLevels)
        {
            SceneFade.Instance.SceneTransition("Dream World");
        }
    }

    public string GetInteractionInstructions()
    {
        if (LevelManager.messagesRead && LevelManager.GetLevel() < LevelManager.amountOfLevels)
            return "E to Sleep";
        else
            return "";
    }
}
