using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MessageSequence", menuName = "MessageSequence/MessageSequence", order = 1)]
public class MessageSequence : ScriptableObject
{
    public int presentMessageCount = 1;

    public List<string> timestamp = new List<string>();
    public List<string> sender = new List<string>();
    public List<string> message = new List<string>();
}
