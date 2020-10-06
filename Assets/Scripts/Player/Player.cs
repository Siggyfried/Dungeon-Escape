using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;

    private Rigidbody2D rigid;
    [SerializeField]
    private float jumpForce = 5.0f;
    private bool resetJump = false;
    [SerializeField]
    private float speed = 5f;
    private bool grounded = false;

    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;
    private SpriteRenderer swordArcSprite;

    public int Health { get; set; }
  
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }
    void Update()
    {
        CalculateMovement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && isGrounded() == true)
        {
            playerAnim.Attack();
        }
    }

    private void CalculateMovement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
        grounded = isGrounded();

        if (move > 0)
        {
            Flip(true);
        }
        else if (move < 0)
        {
            Flip(false);
        }

        if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.Space)) && isGrounded() == true)
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
            swordArcSprite.flipX = false;
            swordArcSprite.flipY = false;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceRight == false)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipX = true;
            swordArcSprite.flipY = true;

            Vector3 newPos = swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            swordArcSprite.transform.localPosition = newPos;
        }
    }
   
    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
        
    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }

        Debug.Log("Player Damaged");
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            playerAnim.Death();
        }
    }
    
    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
