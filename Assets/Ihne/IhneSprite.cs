using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IhneSprite : MonoBehaviour
{
    public SpriteRenderer spr { get; private set; }
  

    public Sprite idle;
    public JumpSprite jump;
    public AnimSprite run;
    public Sprite attack;
    public Ihne p;
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
        spr.enabled = true;
    }

    private void OnDisable()
    {
        spr.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {


        



        if (p.IsGrounded() == true)
        {
            jump.enabled = false;
            run.enabled = p.running;
        }
        else
        {
            run.enabled = false;
            jump.enabled = true;
        }

        if (!p.running && p.IsGrounded())
        {
            spr.sprite = idle;
            run.enabled = false;
        }
       
    }
}
