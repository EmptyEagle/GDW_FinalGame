using UnityEngine;
using UnityEngine.EventSystems;

public class TitleMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject firstSelectedButton;
    private GameObject lastSelectedButton;

    void Start()
    {
        lastSelectedButton = firstSelectedButton;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            eventSystem.SetSelectedGameObject(lastSelectedButton);
        }
        lastSelectedButton = eventSystem.currentSelectedGameObject;
    }
}
