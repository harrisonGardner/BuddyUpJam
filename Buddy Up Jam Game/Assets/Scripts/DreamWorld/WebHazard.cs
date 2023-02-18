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
        spiderChild.transform.forward = transform.up;
    }

    public void Update()    
    {
        if (targetSet)
        {
            float angleToTarget = Vector3.SignedAngle(spiderChild.transform.position, target, spiderChild.transform.right);
            spiderChild.transform.Rotate(new Vector3(0, 0, angleToTarget), Space.Self);

            Vector3 moveTowardsPosition = Vector3.MoveTowards(spiderChild.transform.position, target, spiderMoveSpeed * Time.deltaTime);
            spiderChild.transform.position = moveTowardsPosition;
        }
    }

    public void SetTarget(Vector3 targetPos)
    {
        target = targetPos + (transform.up * spiderChild.transform.localScale.z/2);
        targetSet = true;
    }

}
