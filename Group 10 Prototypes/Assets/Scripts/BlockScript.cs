using UnityEngine;

public class BlockScript : MonoBehaviour
{
    AudioSource sound;
    public AudioClip breakSound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    void Update() { }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "shovel")
        {
            breakBlock();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "explosive")
        {
            breakBlock();
        }
    }
    void breakBlock()
    {
        sound.PlayOneShot(breakSound);
        gameObject.SetActive(false);
    }
}