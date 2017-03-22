using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    int secondsRemaining = 0;
    int minsRemaining = 3;
    public Text timerText;
    void Start()
    {
        timerText.color = Color.red;
        InvokeRepeating("timerDecrememnt", 0, 1.0f);
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
                // end game
            }
        }
    }
    void Update()
    {
        if (minsRemaining == 0 && secondsRemaining <= 30) { timerText.color = Color.red; }
        if (secondsRemaining < 10) { timerText.text = minsRemaining + ":0" + secondsRemaining; }
        else { timerText.text = minsRemaining + ":" + secondsRemaining; }
    }
}