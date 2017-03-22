using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    int health = 10;
    public GameObject healthBar;
    SpriteRenderer sprite;
    public Sprite[] hearts;
    void Start()
    {
        sprite = healthBar.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (health <= 0)
        {
            // end game
        }
    }
    public void damage(int damage)
    {
        if (health - damage <= 0)
        {
            health = 0;
        }
        else if (health - damage >= 10)
        {
            health = 10;
        }
        else
        {
            health -= damage;
        }
        sprite.sprite = hearts[health];
    }
}