using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject laserToTrigger;
    private Laser laserToTriggerScript;
    public bool isOnButton;

    void Start()
    {
        laserToTriggerScript = laserToTrigger.GetComponent<Laser>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Button press");
            laserToTriggerScript.SetLaserActive(isOnButton);
        }
    }
}
