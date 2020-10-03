using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spider : Enemy, IDamageable
{

    public GameObject acidEffectPrefab;
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public override void Update()
    {
        
    }

    public void Damage()
    {
        Health--;
        if (Health < 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    public override void Movement()
    {
       //sit still
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, quaternion.identity);
    }


}
