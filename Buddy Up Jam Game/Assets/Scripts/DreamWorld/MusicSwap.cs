using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwap : MonoBehaviour
{
    AudioSource source;

    public static MusicSwap Instance;

    public AudioClip minor;
    public AudioClip major;

    public float transitionTime = 2f;
    private float transitionTimer = 0f;

    private bool slowAudio = false;
    private bool speedAudio = false;

    private bool swap = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (slowAudio)
        {
            transitionTimer += Time.deltaTime;
            source.pitch = 1 - ((transitionTimer / transitionTime) * 0.5f);
            if (transitionTimer >= transitionTime)
            {
                slowAudio = false;
                float time = source.time / source.clip.length;
                source.clip = major;
                source.Play();
                source.time = source.clip.length * time;
                if(swap)
                    speedAudio = true;
            }
        }
        else if (speedAudio)
        {
            transitionTimer -= Time.deltaTime;
            source.pitch = 1 - ((transitionTimer / transitionTime) * 0.5f);
            if (transitionTimer <= 0f)
            {
                speedAudio = false;
                source.pitch = 1;
            }
        }
    }

    public void Swap()
    {
        Slow();
        swap = true;
    }

    public void Slow()
    {
        slowAudio = true;
        transitionTimer = 0f;
    }
}
