using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public List<GameObject> platforms = new List<GameObject>();
    int pointer = 0;

    public void Start()
    {
        //Get list of platforms
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            platforms.Add(transform.GetChild(i).gameObject);
        }
    }

    public void RevealNextPlatform()
    {
        if (pointer < platforms.Count)
        {
            platforms[pointer].GetComponent<Platform>().RevealPlatform();
            pointer++;
        }
    }
}
