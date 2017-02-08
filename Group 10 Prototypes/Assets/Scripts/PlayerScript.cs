using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode UpKey;
    public KeyCode DownKey;
    public KeyCode AttackKey;
    public GameObject oppenent;
    public GameObject oppenentsShovel;
    public BoxCollider2D shovelSwing;
    public float shovelSwingTime = 0.1f;
    public float moveSpeed = 5f;
    public float jumpForce = 5;
    public float shovelForce;
    public bool canJump = true;
    public bool facingRight;
    public GameObject respawnLocation;
    void Start()
    {
        shovelSwing.enabled = false;
        respawn();
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
        if (Input.GetKeyDown(AttackKey))
        {
            StartCoroutine(swingShovel(shovelSwingTime));
        }
        if (transform.position.y < -5) { respawn(); }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground") { canJump = true; }
        if (col.gameObject.tag == "spike") { respawn(); }
    }
    void respawn()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = respawnLocation.transform.position + new Vector3(0, 0.8f, 0);
    }
    void OnTriggerCollision(Collision2D col)
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
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}