using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode AttackKey;
    public BoxCollider2D shovelSwing;
    public float shovelSwingTime = 0.5f;
    public float moveSpeed = 5f;
    public float jumpForce = 5;
    public bool canJump = true;
    public bool facingRight;
    public Vector2 StartPosistion;
    void Start()
    {
        shovelSwing.enabled = false;
    }

    void Update()
    {
        if (Input.GetKey(LeftKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * moveSpeed);
            if (facingRight) { Flip(); }
        }
        if (Input.GetKey(RightKey))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveSpeed);
            if (!facingRight) { Flip(); }
        }
        if (Input.GetKeyDown(UpKey) && canJump == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            canJump = false;
        }
        if (Input.GetKey(AttackKey))
        {
            swingShovel(shovelSwingTime);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            canJump = true;
        }
        if (col.gameObject.tag == "Respawn")
        {
            transform.position = StartPosistion;
        }
    }

    IEnumerator swingShovel(float swingTime)
    {
        shovelSwing.enabled = true;
        yield return new WaitForSeconds(swingTime);
        shovelSwing.enabled = false;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
