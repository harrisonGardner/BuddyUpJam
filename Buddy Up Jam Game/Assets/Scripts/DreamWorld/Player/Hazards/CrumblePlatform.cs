using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblePlatform : MonoBehaviour
{
    public float crumbleTime = 1f;
    private float crumbleTimer = 0f;

    public float timeToReappear = 5f;
    private float timeToReappearTimer = 0f;

    private bool crumbling = false;
    private bool crumbled = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (crumbling)
        {
            if (crumbleTimer > 0)
            {
                crumbleTimer -= Time.deltaTime;
            }
            else if (!crumbled)
            {
                Crumble();
            }
        }

        if (timeToReappearTimer > 0)
        {
            timeToReappearTimer -= Time.deltaTime;
        }
        else if(crumbled)
        {
            ResetPlatform();
        }
    }

    public void StartCrumble()
    {
        crumbleTimer = crumbleTime;
        crumbling = true;
    }

    private void Crumble()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        timeToReappearTimer = timeToReappear;
        crumbled = true;
        crumbling = false;
    }

    public void ResetPlatform()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        crumbleTimer = 0f;
        timeToReappearTimer = 0f;
        crumbled = false;
    }
}
