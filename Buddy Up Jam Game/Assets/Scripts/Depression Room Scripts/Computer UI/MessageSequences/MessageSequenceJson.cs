using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MessageSequenceJson
{
    public int preSentMessageCount = 0;
    public List<Message> messages = new List<Message>();
}
