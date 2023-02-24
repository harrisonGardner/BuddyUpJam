using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour, IInteraction
{
    private bool interacting = false;
    private bool interactionCalledOnThisFrame = false;
    private GameObject player;

    private PlayerMovement playerMovement;
    private PlayerCam playerCam;
    private PlayerInteraction playerInteraction;


    public void Interact()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        interacting = true;
        player = GameObject.FindGameObjectWithTag("Player");

        playerInteraction = player.GetComponent<PlayerInteraction>();
        playerInteraction.interactionTextCanvas.SetActive(false);
        playerInteraction.enabled = false;

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        playerCam = Camera.main.GetComponent<PlayerCam>();
        playerCam.enabled = false;

        Camera.main.transform.position = transform.GetChild(0).position;
        Camera.main.transform.rotation = transform.GetChild(0).rotation;
        interactionCalledOnThisFrame = true;
    }

    private void DeInteract()
    {
        playerInteraction.enabled = true;
        playerInteraction.interactionTextCanvas.SetActive(true);
        playerMovement.enabled = true;
        playerCam.enabled = true;
        interacting = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        interactionCalledOnThisFrame = true;
        LevelTracker.messagesRead = true;
    }

    private void Awake()
    {
        LevelTracker.messagesRead = false;
    }

    private void Update()
    {
        if (interacting && !interactionCalledOnThisFrame)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DeInteract();
            }
        }

        interactionCalledOnThisFrame = false;
    }
}
