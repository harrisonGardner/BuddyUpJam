using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool highlight = false;
    public string mainText = "What the item is";
    public string subText = "About the item stuff goes here";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highlight = false;
    }

    /// <summary>
    /// Sets the object to be currently highlighted
    /// </summary>
    public void Highlight()
    {
        highlight = true;
    }
}
