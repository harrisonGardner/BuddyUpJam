using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Message
{
    public string timestamp;
    public string sender;
    public string message;

    public Message(string timestamp, string sender, string message)
    {
        this.timestamp = timestamp;
        this.sender = sender;
        this.message = message;
    }
}
