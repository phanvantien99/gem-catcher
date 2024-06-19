using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public AudioClip[] musics;
    public AudioClip loseSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private bool _isGameOver = false;

    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

    private void Update()
    {
        // check if audio is not playing then play random background
        if (!_audioSource.isPlaying && !_isGameOver)
        {
            _audioSource.clip = musics[Random.Range(0, musics.Length)];
            _audioSource.Play();
        }
    }

    public void GameOver()
    {

        gameOverText.text = "Game Over!\nScore: " + ScoreManager.playerScore;
        // play game over sound
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
            _audioSource.clip = loseSound;
            _audioSource.volume = .5f;
            _audioSource.Play();
        }
        gameOverPanel.SetActive(true);
        IsGameOver = true;
    }
}
