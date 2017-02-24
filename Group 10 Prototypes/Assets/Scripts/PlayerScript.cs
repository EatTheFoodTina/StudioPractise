using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode JumpKey;
    public KeyCode DownKey;
    public KeyCode AttackKey;
    public GameObject oppenent;
    public GameObject oppenentsShovel;
    public BoxCollider2D shovelSwing; // shovel collider
    public float shovelSwingTime = 0.1f; // time in seconds that the shovel collider exists for
    public float moveSpeed = 5f;
    public float jumpForce = 5;
    public float shovelForce;
    bool canJump = true;
    bool jump2 = true;
    public bool facingLeft;
    public GameObject respawnLocation;
    float jumpXDirection;
    float jumpYDirection;

    void Start()
    {
        shovelSwing.enabled = false;
        respawn();
    }
    void Update()
    {
        if (Input.GetKey(LeftKey)) // move left
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * moveSpeed);
            if (!facingLeft) { Flip(); }
        }
        if (Input.GetKey(RightKey)) // move right
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * moveSpeed);
            if (facingLeft) { Flip(); }
        }
        if (Input.GetKeyDown(JumpKey)) // jump
        {
            if (canJump)
            {
                //Vector2 jumpDirection = new Vector2(jumpXDirection, jumpYDirection);
                //if (jumpXDirection > 0) {
                //    jumpDirection += Vector2.right;
                //}
                //else if (jumpXDirection < 0) {
                //    jumpDirection += Vector2.left;
                //}
                //jumpDirection.Normalize();
                //GetComponent<Rigidbody2D>().AddForce(jumpDirection * jumpForce);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
                canJump = false;
            }
            else if (jump2)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * (jumpForce + 100));
                jump2 = false;
            }
        }
        if (Input.GetKeyDown(AttackKey)) // swing shovel
        {
            StartCoroutine(swingShovel(shovelSwingTime));
        }
        if (transform.position.y < -5)
        { // if the player falls off the platform
            respawn();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            canJump = true; // can jump if touching ground
            jump2 = true; // enable double jump when the player lands
            jumpXDirection = transform.position.x - col.transform.position.x;
            jumpYDirection = transform.position.y - col.transform.position.y;
        }
        if (col.gameObject.tag == "spike") { respawn(); } // respawn if player hits spike
    }
    void respawn()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = respawnLocation.transform.position + new Vector3(0, 0.8f, 0);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == oppenentsShovel)
        {
            float xDif = oppenent.transform.position.x - transform.position.x;
            float yDif = oppenent.transform.position.y - transform.position.y;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(xDif, yDif).normalized * shovelForce);
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
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}