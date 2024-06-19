using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Gen : MonoBehaviour
{

    public List<GameObject> gemPrefabs;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _timer;
    [SerializeField] private float _spawnInterval = 4f;
    [SerializeField] private ScoreManager _scoreManager;
    public GameObject objectManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if (_gameManager.IsGameOver) return;
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval && !_gameManager.IsGameOver)
        {
            SpawnGem();
            _timer = 0;
        }
    }

    void SpawnGem()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float randomX = Random.Range(-screenWidth, screenWidth);

        System.Random rnd = new System.Random();
        // int randomInt = rnd.Next(0, 1);
        int randomInt = rnd.Next(0, gemPrefabs.Count);
        if (randomInt == gemPrefabs.Count) randomInt = gemPrefabs.Count - 1;

        GameObject gemSpawn = gemPrefabs[randomInt];

        GameObject gemGenerated = Instantiate(gemSpawn, new Vector3(randomX, 6f, 0), Quaternion.identity);
        gemGenerated.transform.parent = transform;
        // set action for gem
        gemGenerated.GetComponent<GemMover>().InitData(addScore, minusScore, boostingStat);
        gemGenerated.SetActive(true);
    }


    /*
    * Add Score
    */
    private void addScore(GemMover gem)
    {
        int number = 1;
        // check if item have boost or nto
        if (gem.BoostPoint > 0)
        {
            ScoreManager.currentBoost = gem.BoostPoint;
            ScoreManager.boostRemainTime = gem.BoostTime;
            if (!ScoreManager._isCountDown)
            {
                _scoreManager.triggerCountBoost();
            }
        }

        if (ScoreManager.boostRemainTime > 0 && ScoreManager.currentBoost > 0)
        {
            number *= ScoreManager.currentBoost;
        }
        _scoreManager.addScore(number);
    }

    // substract score
    private void minusScore(GemMover gem)
    {
        int number = 1;
        _scoreManager.minusScore(number);
    }

    // boost stat
    private void boostingStat(GemMover gem)
    {
        if (gem.IsBoostedItem)
        {
            _scoreManager.BoostStatRemainTime = gem.BoostStatTime;
        }
        if (!_scoreManager.IsCountDownBoost)
        {
            _scoreManager.triggerCountBoostStat();
        }
    }

}
