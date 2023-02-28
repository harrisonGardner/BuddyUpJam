using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundSwap : MonoBehaviour
{
    public AudioClip rainy;
    public AudioClip sunny;
    public AudioSource ambient;
    public AudioSource window;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.GetLevel() >= 3)
        {
            if (ambient.clip != sunny)
            {
                ambient.Stop();
                ambient.clip = sunny;
                ambient.Play();
                window.enabled = false;
            }
        }
        else
        {
            if (ambient.clip != rainy)
            {
                ambient.Stop();
                ambient.clip = rainy;
                ambient.Play();
                window.enabled = true;
            }
        }
    }
}
