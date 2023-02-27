using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideMessageView : MonoBehaviour
{
    public GameObject sideMessagePrefab;

    public List<SideMessageScriptableObject> sideMessages = new List<SideMessageScriptableObject>();

    private int pointer = 0;
    private int level = -1;

    private void Update()
    {
        if (LevelManager.GetLevel() != level)
        {
            level = LevelManager.GetLevel();
            InitializeMessages();
        }
    }

    private void InitializeMessages()
    {
        for (int i = 0; i < sideMessages[level].sender.Count; i++)
            RevealNextMessage();
    }

    private void RevealNextMessage()
    {
        GameObject sideMessage = Instantiate(sideMessagePrefab, transform);
        sideMessage.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, pointer * -110, 0);

        if (pointer == 0)
        {
            sideMessage.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1);
        }

        SideMessageObject sideMessageObject = sideMessage.GetComponent<SideMessageObject>();

        sideMessageObject.sender.text = sideMessages[level].sender[pointer];
        sideMessageObject.message.text = sideMessages[level].message[pointer];
        sideMessageObject.date.text = sideMessages[level].date[pointer];
        sideMessageObject.messageCount.text = sideMessages[level].messageCount[pointer];

        pointer++;
    }
}
