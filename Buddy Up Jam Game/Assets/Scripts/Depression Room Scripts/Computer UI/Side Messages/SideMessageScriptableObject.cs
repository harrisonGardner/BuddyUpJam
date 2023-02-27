using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SideMessageScriptableObject", menuName = "SideMessageScriptableObject/SideMessageScriptableObject", order = 1)]
public class SideMessageScriptableObject : ScriptableObject
{
    public List<string> date = new List<string>();
    public List<string> sender = new List<string>();
    public List<string> message = new List<string>();
    public List<string> messageCount = new List<string>();
}
