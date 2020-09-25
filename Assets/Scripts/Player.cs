using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField]
    private float jumpForce = 5.0f;
    private bool resetJump = false;
    [SerializeField]
    private float speed = 5f;
   
  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        CalculateMovement();


    }

    private void CalculateMovement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(move * speed, rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }
    }

    
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
        if (hitInfo.collider != null)
        {
            if (resetJump == false)
            {
                return true;
            }
        }

        return false;
    }
   
    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
        
 

}
