using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Vector3 currentTarget;
    private Animator anim;
    private SpriteRenderer mossSprite;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        mossSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }     
        Movement();
    }

    void Movement()
    {
        if (currentTarget == pointA.position)
        {
            mossSprite.flipX = true;
        }
        else
        {
            mossSprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }


}
