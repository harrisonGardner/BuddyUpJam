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
    private Vector3 initialPlane;

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
            //spiderChild.transform.localEulerAngles = new Vector3(90, spiderChild.transform.localEulerAngles.y, spiderChild.transform.localEulerAngles.z);

            Vector3 moveTowardsPosition = Vector3.MoveTowards(spiderChild.transform.position, target, spiderMoveSpeed * Time.deltaTime);

            float angleToTarget = Vector3.SignedAngle(spiderChild.transform.position, target, spiderChild.transform.right);
            //spiderChild.transform.Rotate(new Vector3(0, 0, angleToTarget), Space.Self);
            //spiderChild.transform.localEulerAngles = new Vector3(initialPlane.x, initialPlane.y, angleToTarget + 90);

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

    public void GetRandomPathTarget()
    { 
    }
}
