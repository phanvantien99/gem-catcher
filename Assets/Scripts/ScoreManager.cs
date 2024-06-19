using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject gameManager;
    public static int playerScore = 0;
    public static float timeLeft = 10;
    public TextMeshProUGUI scoreText;

    public Mover player;

    public static int currentBoost = 0;
    public static int boostRemainTime = 0;

    public static bool _isCountDown = false;

    private static GameManager _gameManagerObj;

    private bool _isCountDownBoost = false;

    private int boostStatRemainTime = 0;
    private float _currentSpeed;

    public bool IsCountDownBoost { get => _isCountDownBoost; set => _isCountDownBoost = value; }
    public int BoostStatRemainTime { get => boostStatRemainTime; set => boostStatRemainTime = value; }

    private void Start()
    {
        _gameManagerObj = gameManager.GetComponent<GameManager>();
        _currentSpeed = player._speed;
    }

    public void addScore(int amount)
    {
        // if game is playing  then + amount
        if (!_gameManagerObj.IsGameOver)
        {
            playerScore += amount;
        }
    }

    public void minusScore(int amount)
    {
        if (!_gameManagerObj.IsGameOver)
        {
            playerScore -= amount;
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + playerScore + " | Time boost: " + boostRemainTime + " | Current boost: x" + currentBoost + "| Boost stat time: " + BoostStatRemainTime;
    }

    public void triggerCountBoost()
    {
        _isCountDown = true;
        StartCoroutine(CountBoostTime());
    }

    private IEnumerator CountBoostTime()
    {
        while (boostRemainTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            boostRemainTime--;
            yield return null;
        }
        currentBoost = 0;
        _isCountDown = false;
    }

    // trigger count boost speed time
    public void triggerCountBoostStat()
    {
        IsCountDownBoost = true;
        player._speed += 5f;
        StartCoroutine(CountBoostStatTime());
    }

    // count boost speed time
    private IEnumerator CountBoostStatTime()
    {
        while (BoostStatRemainTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            BoostStatRemainTime--;
            yield return null;
        }
        IsCountDownBoost = false;
        player._speed = _currentSpeed;
    }
}
