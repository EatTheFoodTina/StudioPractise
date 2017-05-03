using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class timer : MonoBehaviour
{
    int secondsRemaining = 0;
    int minsRemaining = 3;
    public Text timerText;
    public GameObject player1;
    public PlayerScript player1Script;
    public GameObject player2;
    public PlayerScript player2Script;
    public GameObject p1Win;
    public GameObject p2Win;
    public GameObject fightText;
    void Start()
    {
        StartCoroutine(Fight(2));
        timerText.color = Color.red;
        InvokeRepeating("timerDecrememnt", 0, 1.0f);
        player1Script = player1.GetComponent<PlayerScript>();
        player2Script = player2.GetComponent<PlayerScript>();
    }
    void timerDecrememnt()
    {
        secondsRemaining--;
        if (secondsRemaining <= 0)
        {
            secondsRemaining = 59;
            minsRemaining--;
            if (minsRemaining == 0)
            {
                CancelInvoke();
                EndGame();
            }
        }
    }
    void Update()
    {
        if (minsRemaining == 0 && secondsRemaining <= 30) { timerText.color = Color.red; }
        if (secondsRemaining < 10) { timerText.text = minsRemaining + ":0" + secondsRemaining; }
        else { timerText.text = minsRemaining + ":" + secondsRemaining; }
    }
    void EndGame()
    {
        if (player1Script.score>player2Script.score)
        {
            p1Win.SetActive(true);
        }
        else if (player2Script.score > player1Script.score)
        {
            p2Win.SetActive(true);
        }
        StartCoroutine(ReturnToMenu(5));
    }
    IEnumerator ReturnToMenu(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
    IEnumerator Fight(float time)
    {
        yield return new WaitForSeconds(time);
        fightText.SetActive(false);
    }
}