using UnityEngine;

public class Laser : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    private SpriteRenderer spriteRend;
    
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        InvokeRepeating("AnimateLaser", 0f, 0.15f);
    }
    
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

    private void AnimateLaser()
    {
        spriteRend.flipX = !spriteRend.flipX;
    }
}
