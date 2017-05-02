using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public KeyCode LeftKey;
    public KeyCode RightKey;
    public KeyCode JumpKey;
    public KeyCode AttackFrontKey;
    public KeyCode AttackDownKey;
    public GameObject opponent;
    public PlayerScript opponentScript;
    public GameObject opponentsShovel;
    public BoxCollider2D shovelSwingFront; // front shovel collider
    public float shovelSwingTime = 0.1f; // time in seconds that the shovel collider exists for
    public float moveSpeed = 5f;
    public float jumpForce = 5;
    public float shovelForce;
    public float exForce = 100;
    bool canJump = true;
    bool jump2 = true;
    public bool facingLeft;
    public Vector2 respawnLocation;
    public float spawnVertOffset = 1f;
    Rigidbody2D rb;
    public Text scoreDisplay;
    AudioSource sound;
    public AudioClip snowImpact;
    //public AudioClip snowCrunch;
    public AudioClip grunt;
    public int score;
    public int player;
    void Start()
    {
        opponentScript = opponent.GetComponent<PlayerScript>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        shovelSwingFront.enabled = false;
        respawn();
    }
    void Update()
    {
        scoreDisplay.text = "Player " + player + " Score: " + score.ToString();
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
                rb.velocity = (Vector2.up * jumpForce) + new Vector2(rb.velocity.x, 0);
                canJump = false;
            }
            else if (jump2)
            {
                rb.velocity = (Vector2.up * jumpForce) + new Vector2(rb.velocity.x, 0);
                jump2 = false;
            }
        }
        if (Input.GetKeyDown(AttackFrontKey)) // swing shovel
        {
            StartCoroutine(swingShovel(shovelSwingTime));
        }
        if (transform.position.y < -5)
        { // if the player falls off the platform
            opponentScript.score++;
            respawn();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "alcohol")
        {
            col.gameObject.SetActive(false);
            score++;
        }
        if (col.gameObject.tag == "ground" || col.gameObject.tag == "Player")
        {
            if (!canJump) { sound.PlayOneShot(snowImpact); }
            canJump = true; // can jump if touching ground
            jump2 = true; // enable double jump when the player lands
        }
        if (col.gameObject.tag == "spike")
        {
            respawn(); // respawn if player hits spike
            opponentScript.score++;
        }
        if (col.gameObject.tag == "explosive")
        {
            rb.AddForce(new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized * exForce);
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
        transform.position = respawnLocation + new Vector2(0, spawnVertOffset);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == opponentsShovel)
        {
            sound.PlayOneShot(grunt);
            float xDif = transform.position.x - opponent.transform.position.x;
            float yDif = transform.position.y - opponent.transform.position.y;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(xDif, yDif).normalized * shovelForce);
        }
    }
    IEnumerator swingShovel(float swingTime)
    {
        shovelSwingFront.enabled = true;
        yield return new WaitForSeconds(swingTime);
        shovelSwingFront.enabled = false;
    }
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}