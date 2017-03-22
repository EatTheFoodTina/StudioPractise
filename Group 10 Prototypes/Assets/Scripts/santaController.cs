using UnityEngine;

public class santaController : MonoBehaviour
{
    public GameObject santa;
    public int time;
    public int maxTime;
    public int minTime;
    Rigidbody2D santaRb;
    void Start()
    {
        time = Random.Range(minTime, maxTime);
        InvokeRepeating("timerDecrement", 0, 1);
        santaRb = santa.GetComponent<Rigidbody2D>();
    }
    void timerDecrement()
    {
        if (time > 0)
        {
            time--;
            return;
        }
        else
        {
            santaRb.velocity = Vector2.zero;
            santa.SetActive(true);
            if (Random.Range(0, 1) == 0)
            {
                santa.transform.position = new Vector2(-8, 5);
                santaRb.AddForce(Vector2.right * 500);
                santaRb.AddTorque(500);
            }
            else
            {
                santa.transform.position = new Vector2(8, 5);
                santaRb.AddForce(Vector2.left * 500);
                santaRb.AddTorque(-500);
            }
            time = Random.Range(minTime, maxTime);
        }
    }
}