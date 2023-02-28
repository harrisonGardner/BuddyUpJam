using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillowInteraction : MonoBehaviour
{
    public int levelToMoveOn = 2;
    public Transform newPosition;
    private void Update()
    {
        if (LevelManager.GetLevel() >= levelToMoveOn)
        {
            transform.localPosition = newPosition.localPosition;
        }
    }
}
