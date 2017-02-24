using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{
    void Start() { }
    void Update() { }
    void OnCollisionEnter2D(Collision2D col)
    {
        //explode
        gameObject.SetActive(false);
    }
}
