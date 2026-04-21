using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject firstSelectedButton;
    private GameObject lastSelectedButton;

    void Start()
    {
        lastSelectedButton = firstSelectedButton;
    }
    
    void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        lastSelectedButton = eventSystem.currentSelectedGameObject;
    }
}
