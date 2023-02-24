using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject cam;
    public GameObject interactionTextCanvas;

    private float interactionTextDefaultScale = 0.01f;

    public float interactDistance = 5f;

    public float textOffset = 1f;

    private GameObject lastHighlighted = null;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        interactionTextCanvas = GameObject.FindGameObjectWithTag("InteractionTextCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            LevelTracker.SetLevel(LevelTracker.GetLevel() + 1);
        if (Input.GetKeyDown(KeyCode.LeftControl))
            LevelTracker.SetLevel(LevelTracker.GetLevel() - 1);

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance))
        {
            GameObject highlightedObject = hit.collider.gameObject;

            if (highlightedObject.CompareTag("Interactable"))
            {
                interactionTextCanvas.SetActive(true);

                //Snap text to newly highlighted object
                if (highlightedObject != lastHighlighted)
                    interactionTextCanvas.transform.position = hit.point;

                interactionTextCanvas.GetComponent<InteractionText>().SetTargetPosition(hit.point + (cam.transform.position - interactionTextCanvas.transform.position).normalized * textOffset + new Vector3(0, interactionTextCanvas.GetComponent<RectTransform>().sizeDelta.y * (interactionTextCanvas.GetComponent<RectTransform>().localScale.y/2), 0) );
                interactionTextCanvas.transform.forward = interactionTextCanvas.transform.position - cam.transform.position;

                float scale = interactionTextDefaultScale * (Vector3.Distance(interactionTextCanvas.transform.position, cam.transform.position)/10f);
                interactionTextCanvas.transform.localScale = new Vector3(scale, scale, scale);

                //First child is main text, second child is sub text
                //interactionTextCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hit.collider.gameObject.GetComponent<Interactable>().mainText;
                //interactionTextCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hit.collider.gameObject.GetComponent<Interactable>().subText;

                interactionTextCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hit.collider.gameObject.GetComponent<Interactable>().GetInteractionText(LevelTracker.GetLevel());

                if (Input.GetKeyDown(KeyCode.E))
                    highlightedObject.GetComponent<Interactable>().Interact();

                lastHighlighted = hit.collider.gameObject;
            }
            else
            {
                interactionTextCanvas.SetActive(false);
                lastHighlighted = null;
            }
        }
        else
        {
            interactionTextCanvas.SetActive(false);
            lastHighlighted = null;
        }
    }
}
