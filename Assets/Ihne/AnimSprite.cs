using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameRate = 1f / 6f;

    private SpriteRenderer spr;

    private int frame;

    // Start is called before the first frame update
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;

        if(frame >= sprites.Length)
        {
            frame = 0;
        }

        if(frame >= 0 && frame < sprites.Length)
        {
            spr.sprite = sprites[frame];
        }
        
    }
}
