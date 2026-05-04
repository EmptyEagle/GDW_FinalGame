using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private KeyCode key_Pause = KeyCode.Escape;
    public EventSystem eventSystem;
    public GameObject firstSelectedButton;
    private ScoreManager scoreManager;
    private GameObject lastSelectedButton;
    private AudioSource musicSource;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        lastSelectedButton = firstSelectedButton;
    }

    void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key_Pause))
        {
            if (pauseMenu.activeSelf != true)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        lastSelectedButton = eventSystem.currentSelectedGameObject;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        scoreManager.SetPaused(true);
        musicSource.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        scoreManager.SetPaused(false);
        musicSource.UnPause();
    }
}
