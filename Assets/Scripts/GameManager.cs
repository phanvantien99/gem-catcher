using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    [SerializeField] private bool _isGameOver = false;

    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.timeLeft <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!\nScore: " + ScoreManager.playerScore;
        gameOverPanel.SetActive(true);
        IsGameOver = true;
    }
}
