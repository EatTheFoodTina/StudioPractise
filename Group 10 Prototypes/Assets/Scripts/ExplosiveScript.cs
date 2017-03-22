using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    AudioSource audio;
    public AudioClip explosionSound;
    public float explosionVolume;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //explode
        audio.PlayOneShot(explosionSound, explosionVolume);
        gameObject.SetActive(false);
    }
}