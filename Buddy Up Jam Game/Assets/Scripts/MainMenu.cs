using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool viewing = true;
    public GameObject cam;
    private PlayerCam playerCam;
    public GameObject player;
    private PlayerMovement playerMovement;

    public float transitionTime = 0.75f;
    private float transitionTimer = 0f;
    private bool transitionFinished = false;

    private Vector3 cameraTargetVector;

    public Button playButton;
    public Button optionsButton;
    public Button creditButton;
    public Button quitButton;

    public CanvasGroup canvasGroup;

    public GameObject credits;
    public Button creditsBackButton;

    public PauseMenu pauseMenu;

    public AudioClip notificationSound;
    public AudioSource notificationSource;
    private void Start()
    {
        playerCam = cam.GetComponent<PlayerCam>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        playerCam.locked = true;
        cam.transform.position = transform.position;

        pauseMenu = PauseMenu.Instance;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        playButton.onClick.AddListener(Play);
        optionsButton.onClick.AddListener(pauseMenu.OpenOptions);

        creditButton.onClick.AddListener(OpenCredits);
        creditsBackButton.onClick.AddListener(CloseCredits);

        quitButton.onClick.AddListener(pauseMenu.QuitGame);
        

        if (LevelManager.GetLevel() > 0)
        {
            canvasGroup.alpha = 0;
            Play();
        }
    }

    private void Update()
    {

        if (!viewing && !transitionFinished)
        {
            transitionTimer += Time.deltaTime;

            float adjustedTransitionTime = easeInOutSine(transitionTimer / transitionTime);

            cam.transform.position = transform.position + (cameraTargetVector * adjustedTransitionTime);
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, (1 - adjustedTransitionTime) * 90f);

            if(LevelManager.GetLevel() <= 0)
                canvasGroup.alpha = (1 - adjustedTransitionTime);

            if (transitionTimer >= transitionTime)
                transitionFinished = true;
        }
        else if (!viewing && transitionFinished)
        {
            notificationSource.PlayOneShot(notificationSound);
            playerMovement.enabled = true;
            playerCam.locked = false;
            gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        viewing = false;
        transitionFinished = false;
        transitionTimer = 0;

        cameraTargetVector = player.transform.GetChild(2).transform.position - transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }

    //https://easings.net/#easeInOutSine
    private float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }
}
