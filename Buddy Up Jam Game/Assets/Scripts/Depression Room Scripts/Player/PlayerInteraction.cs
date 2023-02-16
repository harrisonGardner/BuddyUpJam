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
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactDistance))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                interactionTextCanvas.SetActive(true);

                //Snap text to newly highlighted object
                if (hit.collider.gameObject != lastHighlighted)
                    interactionTextCanvas.transform.position = hit.point;

                interactionTextCanvas.GetComponent<InteractionText>().SetTargetPosition(hit.point + (cam.transform.position - interactionTextCanvas.transform.position).normalized * textOffset + new Vector3(0, interactionTextCanvas.GetComponent<RectTransform>().sizeDelta.y * (interactionTextCanvas.GetComponent<RectTransform>().localScale.y/2), 0) );
                interactionTextCanvas.transform.forward = interactionTextCanvas.transform.position - cam.transform.position;

                float scale = interactionTextDefaultScale * (Vector3.Distance(interactionTextCanvas.transform.position, cam.transform.position)/10f);
                interactionTextCanvas.transform.localScale = new Vector3(scale, scale, scale);

                //First child is main text, second child is sub text
                interactionTextCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = hit.collider.gameObject.GetComponent<Interactable>().mainText;
                interactionTextCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hit.collider.gameObject.GetComponent<Interactable>().subText;

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
