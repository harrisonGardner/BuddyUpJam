using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    public List<MessageSequence> messages = new List<MessageSequence>();
    private int pointer = 0;

    public ComputerInteraction computerInteraction;

    public TextMeshProUGUI sender;
    public GameObject messagePrefab;
    public GameObject timeStampPrefab;
    public GameObject scrollView;
    public ScrollRect scrollRect;

    public TextMeshProUGUI instructions;

    private float contentStartHeight = 825f;
    private float totalMessagesHeight = 50f;

    private int level = -1;

    public Scrollbar scrollbar;

    public AudioClip sentSound;
    public AudioClip receivedSound;

    public AudioSource audioSource;

    public void Update()
    {
        if (LevelManager.GetLevel() != level)
        {
            level = LevelManager.GetLevel();
            InitializeMessages();
        }

        if (computerInteraction.interacting)
        {
            scrollbar.value += (Input.mouseScrollDelta.y * 35f) / scrollRect.content.sizeDelta.y;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RevealNextMessage();
                pointer++;
                if (pointer >= messages[level].sender.Count)
                {
                    LevelManager.messagesRead = true;
                    if (LevelManager.GetLevel() >= 3)
                    {
                        LevelManager.readyToLeave = true;
                    }
                    instructions.text = "E to Stop Viewing Messages";
                }
            }
        }
    }

    private void InitializeMessages()
    {
        for (int i = 0; i < messages[level].sender.Count; i++)
        {
            if (messages[level].sender[i] != "" && messages[level].sender[i] != "-")
            {
                sender.text = messages[level].sender[i];
                break;
            }
        }

        for (int i = 0; i < messages[level].presentMessageCount; i++)
        {
            RevealNextMessage();
            pointer++;
        }
    }

    public void RevealNextMessage()
    {
        if (pointer < messages[level].sender.Count)
        {

            if (messages[level].timestamp[pointer] != "")
            {
                GameObject message = GameObject.Instantiate(timeStampPrefab, scrollView.transform);
                message.GetComponent<RectTransform>().localPosition = new Vector3(430, -totalMessagesHeight, 0);
                message.GetComponent<TextMeshProUGUI>().text = messages[level].timestamp[pointer];

                totalMessagesHeight += 100; //150 is space between messages, 100 is height of messages
            }
            else
            {
                GameObject message = GameObject.Instantiate(messagePrefab, scrollView.transform);
                message.GetComponent<RectTransform>().localPosition = new Vector3((messages[level].sender[pointer] == "" || messages[level].sender[pointer] == "You" ? 1150 : 200), -totalMessagesHeight, 0);
                message.GetComponent<MessageObject>().message.text = messages[level].message[pointer];
                message.GetComponent<MessageObject>().sender.text = messages[level].sender[pointer];

                if (messages[level].sender[pointer] != "You")
                    audioSource.PlayOneShot(receivedSound);
                else
                    audioSource.PlayOneShot(sentSound);

                totalMessagesHeight += 150; //150 is space between messages, 100 is height of messages
            }
            
            if (totalMessagesHeight > contentStartHeight)
            {
                scrollbar.gameObject.SetActive(true);
                scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollView.GetComponent<RectTransform>().sizeDelta.x, totalMessagesHeight + 100);
                StartCoroutine("ScrollToBottom");
            }
        }
    }

    private void ScrollToBottom()
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
