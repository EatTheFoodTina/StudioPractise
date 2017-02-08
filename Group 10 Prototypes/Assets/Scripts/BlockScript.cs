using UnityEngine;

public class BlockScript : MonoBehaviour
{
    void Start() { }
    void Update() { }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "shovel")
        {
            gameObject.SetActive(false);
        }
    }
}
