using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen : MonoBehaviour
{

    public GameObject gemPrefabs;

    [SerializeField] private float _timer;
    [SerializeField] private float _spawnInterval = 4f;
    public GameObject objectManager;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = objectManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameOver) return;
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnGem();
            _timer = 0;
        }
    }

    void SpawnGem()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float randomX = Random.Range(-screenWidth, screenWidth);

        Instantiate(gemPrefabs, new Vector3(randomX, 6f, 0), Quaternion.identity);
    }
}
