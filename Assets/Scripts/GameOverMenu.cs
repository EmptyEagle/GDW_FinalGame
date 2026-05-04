using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject firstSelectedButton;
    private GameObject lastSelectedButton;
    private AudioSource musicSource;

    void Start()
    {
        lastSelectedButton = firstSelectedButton;
    }
    
    void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstSelectedButton);
        musicSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        StartCoroutine(MusicFadeOut());
        musicSource.volume = 1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        lastSelectedButton = eventSystem.currentSelectedGameObject;
    }

    IEnumerator MusicFadeOut()
    {
        float musicVolume = 1f;
        for (int i = 0; i < 10; i++)
        {
            musicVolume -= 0.1f;
            musicSource.volume = musicVolume;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
