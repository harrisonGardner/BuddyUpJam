using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteraction : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        Debug.Log("Interacted with desk");
    }

    private void Update()
    {

    }
}
