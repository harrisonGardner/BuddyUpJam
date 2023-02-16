using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject cam;
    private GameObject interactionTextCanvas;

    public float textOffset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        interactionTextCanvas = GameObject.FindGameObjectWithTag("InteractionTextCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 50f))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                interactionTextCanvas.SetActive(true);
                interactionTextCanvas.GetComponent<InteractionText>().SetTargetPosition(hit.point + (cam.transform.position - interactionTextCanvas.transform.position).normalized * textOffset + new Vector3(0, 0.5f, 0));
                interactionTextCanvas.transform.forward = interactionTextCanvas.transform.position - cam.transform.position;
            }
            else
            {
                interactionTextCanvas.SetActive(false);
            }
        }
        else
        {
            interactionTextCanvas.SetActive(false);
        }
    }
}
