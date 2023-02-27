using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenu;

    public Button resumeButton;
    public Button optionsButton;
    public Button optionsBackButton;
    public Button quitButton;

    public Toggle arachnophobiaButton;
    public Toggle triggerButton;

    private PlayerCam playerCam;

    private CursorLockMode camLockState;
    private bool cursorVisibleState = false;
    private bool playerCamPrevLocked = false;

    private void Start()
    {
        Camera.main.TryGetComponent<PlayerCam>(out playerCam);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        resumeButton.onClick.AddListener(Resume);
        optionsButton.onClick.AddListener(OpenOptions);
        optionsBackButton.onClick.AddListener(CloseOptions);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = camLockState;
        Cursor.visible = cursorVisibleState;
        CloseOptions();
        playerCam.locked = playerCamPrevLocked;
    }

    void Pause()
    {
        camLockState = Cursor.lockState;
        cursorVisibleState = Cursor.visible;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCamPrevLocked = playerCam.locked;
        playerCam.locked = true;
    }

    void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

    void CloseOptions()
    {
        optionsMenu.SetActive(false);
    }

    public void LoadMenu()
    {

    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
