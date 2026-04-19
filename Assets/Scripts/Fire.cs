using UnityEngine;

public class Fire : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        InvokeRepeating("AnimateFire", 0f, 0.1f);
    }
    
    private void AnimateFire()
    {
        spriteRend.flipX = !spriteRend.flipX;
    }
}
