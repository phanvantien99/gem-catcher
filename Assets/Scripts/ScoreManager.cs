using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int playerScore = 0;
    public static float timeLeft = 10;
    public TextMeshProUGUI scoreText;

    public static void addScore(int amount)
    {
        playerScore += amount;
    }

    private void Start()
    {
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
        scoreText.text = "Score: " + playerScore + " | Time left: " + timeLeft;
    }

    private IEnumerator StartCountDown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
    }
}
