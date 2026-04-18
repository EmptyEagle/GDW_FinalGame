using UnityEngine;

public class Laser : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    public void SetLaserActive(bool active)
    {
        if (active)
        {
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }
    }
}
