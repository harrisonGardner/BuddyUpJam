using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionText : MonoBehaviour
{
    private Vector3 targetPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.05f);
    }

    public void SetTargetPosition(Vector3 target)
    {
        targetPos = target;
    }
}
