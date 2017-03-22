using UnityEngine;

public class FallingItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public int time = 0;
    public int minTime = 5;
    public int maxTime = 20;
    public int item;
    void Start()
    {
        time = Random.Range(minTime, maxTime);
        InvokeRepeating("timerDecrement", 0, 1);
    }
    void timerDecrement()
    {
        if (time > 0)
        {
            time--;
            return;
        }
        if (time == 0)
        {
            item = Random.Range(0, items.Length);
            spawnItem(item);
            time = Random.Range(minTime, maxTime);
            return;
        }
    }
    void spawnItem(int item)
    {
        items[item].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        items[item].SetActive(true);
        items[item].transform.position = new Vector2(Random.Range(-8, 8), 5);
        items[item].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1) * 100, 0));
    }
}