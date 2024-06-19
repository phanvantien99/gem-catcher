using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomSpawner : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _boomPrefab;

    [SerializeField] private int _timeToSpawn;

    private float _timer;

    public GameManager GameManager { get => _gameManager; set => _gameManager = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToSpawn && !GameManager.IsGameOver)
        {
            SpawnBoom();
            _timer = 0;
        }
    }


    private void SpawnBoom()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float positionX = Random.Range(-screenWidth, screenWidth);
        GameObject generateBoom = Instantiate(_boomPrefab, new Vector3(positionX, transform.position.y + 0.65f, 0), Quaternion.identity);
        generateBoom.GetComponent<Boom>().IsReady = false;
        generateBoom.transform.parent = transform;
        generateBoom.SetActive(true);
    }
}
