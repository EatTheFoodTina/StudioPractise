using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    AudioSource sound;
    public AudioClip explosionSound;
    public float explosionVolume;
    public float shovelForce = 100;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //explode
        sound.PlayOneShot(explosionSound, explosionVolume);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "shovel")
        {
            float xDif = transform.position.x - col.transform.position.x;
            float yDif = transform.position.y - col.transform.position.y;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(xDif, yDif).normalized * shovelForce);
        }
    }
}