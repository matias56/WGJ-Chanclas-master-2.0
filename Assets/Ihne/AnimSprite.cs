using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameRate = 1f / 6f;

    private SpriteRenderer spr;

    private int frame;

    public bool dis;

    // Start is called before the first frame update
    void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        if(!dis)
        {
            InvokeRepeating(nameof(Animate), frameRate, frameRate);
        }
        
    }

    public void OnDisable()
    {
        CancelInvoke(nameof(Animate));
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
