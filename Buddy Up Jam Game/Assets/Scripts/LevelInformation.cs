using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    public static LevelInformation Instance;

    [SerializeField]
    public List<DreamLevelParameters> dreamLevels = new List<DreamLevelParameters>();

    public void Awake()
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

    public DreamLevelParameters GetLevelParameters(int level)
    {
        return dreamLevels[level];
    }
}
