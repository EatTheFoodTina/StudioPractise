using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode JumpKey;
    public KeyCode AttackFrontKey;
    public KeyCode AttackDownKey;
    public GameObject oppenent;
    public GameObject oppenentsShovel;
    public BoxCollider2D shovelSwingFront; // front shovel collider
    public BoxCollider2D shovelSwingDown; // down shovel collider
    public float shovelSwingTime = 0.1f; // time in seconds that the shovel collider exists for
    public float moveSpeed = 5f;
    public float jumpForce = 5;
    public float shovelForce;
    public float exForce = 100;
    bool canJump = true;
    bool jump2 = true;
    public bool facingLeft;
    public bool wallJump = false;
    public GameObject respawnLocation;
    public float spawnVertOffset = 1f;
    float jumpXDirection;
    float jumpYDirection;
    Rigidbody2D rb;
    public GameObject healthBar;
    HealthController health;
    AudioSource sound;
    public AudioClip snowImpact;
    //public AudioClip snowCrunch;
    public AudioClip grunt;
    void Start()
    {
        health = healthBar.GetComponent<HealthController>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        shovelSwingFront.enabled = false;
        shovelSwingDown.enabled = false;
        respawn();
    }
    void Update()
    {
        if (Input.GetKey(LeftKey)) // move left
        {
            rb.AddForce(Vector2.left * moveSpeed);
            if (!facingLeft) { Flip(); }
        }
        if (Input.GetKey(RightKey)) // move right
        {
            rb.AddForce(Vector2.right * moveSpeed);
            if (facingLeft) { Flip(); }
        }
        if (Input.GetKeyDown(JumpKey)) // jump
        {
            if (canJump)
            {
                //Vector2 jumpDirection = new Vector2(jumpXDirection, jumpYDirection);
                //if (jumpXDirection > 0 && jumpYDirection > -1 && jumpYDirection < 1 && wallJump)
                //{
                //    jumpDirection += Vector2.right;
                //}
                //else if (jumpXDirection < 0 && jumpYDirection > -1 && jumpYDirection < 1 && wallJump)
                //{
                //    jumpDirection += Vector2.left;
                //}
                //else if (jumpYDirection > 1)
                //{
                //    jumpDirection = Vector2.up;
                //}
                //else if (jumpYDirection < -1 && wallJump)
                //{
                //    jumpDirection = Vector2.down;
                //}
                //jumpDirection.Normalize();
                //rb.velocity = (jumpDirection * jumpForce) + new Vector2(rb.velocity.x, 0);
                rb.velocity = (Vector2.up * jumpForce) + new Vector2(rb.velocity.x, 0);
                canJump = false;
            }
            else if (jump2 && !wallJump)
            {
                rb.velocity = (Vector2.up * jumpForce) + new Vector2(rb.velocity.x, 0);
                jump2 = false;
            }
        }
        if (Input.GetKeyDown(AttackFrontKey)) // swing shovel
        {
            StartCoroutine(swingShovel(shovelSwingTime));
        }
        if (Input.GetKeyDown(AttackDownKey))
        {
            StartCoroutine(swingShovelDown(shovelSwingTime));
        }
        if (transform.position.y < -5)
        { // if the player falls off the platform
            health.damage(2);
            respawn();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "alcohol")
        {
            col.gameObject.SetActive(false);
            health.damage(-2);
        }
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
        {
            if (!canJump) { sound.PlayOneShot(snowImpact); }
            canJump = true; // can jump if touching ground
            jump2 = true; // enable double jump when the player lands
            //jumpXDirection = transform.position.x - col.transform.position.x;
            //jumpYDirection = transform.position.y - col.transform.position.y;
        }
        if (col.gameObject.tag == "spike")
        {
            respawn(); // respawn if player hits spike
            health.damage(2);
        }
        if (col.gameObject.tag == "explosive")
        {
            rb.AddForce(new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized * exForce);
            health.damage(2);
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        //if (col.gameObject.tag == "ground" && !rb.velocity.Equals(Vector2.zero))
        //  {
        //     sound.PlayOneShot(snowCrunch);
        //  }
    }
    void respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = respawnLocation.transform.position + new Vector3(0, spawnVertOffset, 0);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == oppenentsShovel)
        {
            sound.PlayOneShot(grunt);
            float xDif = transform.position.x - oppenent.transform.position.x;
            float yDif = transform.position.y - oppenent.transform.position.y;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(xDif, yDif).normalized * shovelForce);
            health.damage(1);
        }
    }
    IEnumerator swingShovel(float swingTime)
    {
        shovelSwingFront.enabled = true;
        yield return new WaitForSeconds(swingTime);
        shovelSwingFront.enabled = false;
    }
    IEnumerator swingShovelDown(float swingTime)
    {
        shovelSwingDown.enabled = true;
        yield return new WaitForSeconds(swingTime);
        shovelSwingDown.enabled = false;
    }
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}