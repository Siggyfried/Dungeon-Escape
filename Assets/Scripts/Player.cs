using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField]
    private float jumpForce = 5.0f;
    private bool resetJump = false;
    [SerializeField]
    private float speed = 5f;
    private bool grounded = false;

    private PlayerAnimation playerAnim;

    private SpriteRenderer playerSprite;
   
  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    
    void Update()
    {
        CalculateMovement();

        if (Input.GetMouseButtonDown(0) && isGrounded() == true)
        {
            playerAnim.Attack();
        }
    }

    private void CalculateMovement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        grounded = isGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
            playerAnim.Jump(true);
        }

            rigid.velocity = new Vector2(move * speed, rigid.velocity.y);

            playerAnim.Move(move);
        
    }

    
    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        if (hitInfo.collider != null)
        {
            if (resetJump == false)
            {
                playerAnim.Jump(false);
                return true;               
            }
        }

        return false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            playerSprite.flipX = false;
        }
        else if (faceRight == false)
        {
            playerSprite.flipX = true;
        }
    }
   
    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
        
 

}
