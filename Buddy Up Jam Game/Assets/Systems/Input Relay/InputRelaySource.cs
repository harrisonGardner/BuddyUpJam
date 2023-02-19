using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRelaySource : MonoBehaviour
{
    [SerializeField] LayerMask RaycastMask = ~0;
    [SerializeField] float RaycastDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //retrvive a ray based on the mouse location
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        //raycast to find what we hit
        RaycastHit hitResult;
        if(Physics.Raycast(mouseRay, out hitResult, RaycastDistance, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            //ignore if not us
            if(hitResult.collider.gameObject != gameObject)
            {
                return;
            }
            Debug.Log(hitResult.textureCoord);
        }
    }
}
