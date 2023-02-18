using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebHazard : MonoBehaviour
{
    //The player's position
    private Vector3 target;
    private bool targetSet = false;

    public float spiderMoveSpeed = 5f;
    private GameObject spiderChild;

    public void Start()
    {
        spiderChild = transform.GetChild(0).gameObject;
        spiderChild.transform.parent = null;
        spiderChild.transform.up = transform.up;
    }

    public void Update()    
    {
        if (targetSet)
        {
            spiderChild.transform.right = spiderChild.transform.position - target;

            Vector3 moveTowardsPosition = Vector3.MoveTowards(spiderChild.transform.position, target, spiderMoveSpeed * Time.deltaTime);

            float angleToTarget = Vector3.SignedAngle(spiderChild.transform.position, target, spiderChild.transform.right);

            spiderChild.transform.position = moveTowardsPosition;

            if (Vector3.Distance(spiderChild.transform.position, target) < 0.5f)
            {
                targetSet = false;
                spiderChild.transform.up = transform.up;
            }
        }
    }

    public void SetTarget(Vector3 targetPos)
    {
        target = targetPos + (transform.up * spiderChild.transform.localScale.z/2);
        targetSet = true;
    }
}
