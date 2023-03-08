using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSequenceToJson : MonoBehaviour
{
    [SerializeField] private bool convert = false;
    [SerializeField] private bool load = false;
    [SerializeField] private string fileName;

    [SerializeField] public MessageSequence sequenceToConvert;

    public List<Message> messages = new List<Message>();

    private void Update()
    {
        if (convert)
        {
            Convert();
        }
        if(load)
        {
            Load();
        }
    }

    private void Convert()
    {
        MessageSequenceJson messageJson = new MessageSequenceJson();

        for (int i = 0; i < sequenceToConvert.message.Count; i++)
        {
            messageJson.preSentMessageCount = sequenceToConvert.presentMessageCount;
            messageJson.messages.Add(new Message(sequenceToConvert.timestamp[i], sequenceToConvert.sender[i], sequenceToConvert.message[i]));
        }

        Debug.Log(JsonUtility.ToJson(messageJson, true));

        convert = false;
    }

    private void Load()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("MessageSequences/PreLevel1");
        
        MessageSequenceJson sequence = JsonUtility.FromJson<MessageSequenceJson>(jsonText.text);

        Debug.Log(sequence.preSentMessageCount);

        load = false;
    }
}