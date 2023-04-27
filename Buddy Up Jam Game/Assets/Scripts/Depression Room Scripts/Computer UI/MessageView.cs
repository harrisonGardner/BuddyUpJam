using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageView : MonoBehaviour
{
    public List<MessageSequence> messageSequences = new List<MessageSequence>();
    public List<string> messageSequenceFileName = new List<string>();
    private List<Message> messages = new List<Message>();
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
                if (pointer >= messages.Count)
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
        TextAsset jsonText = Resources.Load<TextAsset>("MessageSequences/" + messageSequenceFileName[level]);

        MessageSequenceJson sequence = JsonUtility.FromJson<MessageSequenceJson>(jsonText.text);

        messages = sequence.messages;

        for (int i = 0; i < messages.Count; i++)
        {
            if (messages[i].sender != "" && messages[i].sender != "-")
            {
                sender.text = messages[i].sender;
                break;
            }
        }

        for (int i = 0; i < sequence.preSentMessageCount; i++)
        {
            RevealNextMessage();
            pointer++;
        }
    }

    public void RevealNextMessage()
    {
        if (pointer < messages.Count)
        {
            if (messages[pointer].timestamp != "")
            {
                GameObject message = GameObject.Instantiate(timeStampPrefab, scrollView.transform);
                message.GetComponent<RectTransform>().localPosition = new Vector3(430, -totalMessagesHeight, 0);
                message.GetComponent<TextMeshProUGUI>().text = messages[pointer].timestamp;

                totalMessagesHeight += 100;
            }
            else
            {
                GameObject message = GameObject.Instantiate(messagePrefab, scrollView.transform);
                message.GetComponent<RectTransform>().localPosition = new Vector3((messages[pointer].sender == "" || messages[pointer].sender == "You" ? 1150 : 200), -totalMessagesHeight, 0);
                message.GetComponent<MessageObject>().message.text = messages[pointer].message;
                message.GetComponent<MessageObject>().message.ForceMeshUpdate();
                message.GetComponent<RectTransform>().sizeDelta = new Vector2(message.GetComponent<RectTransform>().sizeDelta.x, message.GetComponent<MessageObject>().message.renderedHeight + 10f);
                message.GetComponent<MessageObject>().sender.text = messages[pointer].sender;

                if (messages[pointer].sender != "You")
                    audioSource.PlayOneShot(receivedSound);
                else
                    audioSource.PlayOneShot(sentSound);

                totalMessagesHeight += message.GetComponent<MessageObject>().message.renderedHeight + 80f;
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
