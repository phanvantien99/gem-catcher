using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclesPrefabs;

    [SerializeField] private float _timer;

    [SerializeField] private int _spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            SpawnObstacle();
            _timer = 0;
        }
    }

    private void SpawnObstacle()
    {
        // get size of camera
        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        // random from left to right or right to left
        bool coinIsHeads = Random.value < 0.5f;
        
        // it's 6 unit from the center to the end of the prefabs 
        float positionX = 6;
        float positionY = Random.Range(-screenHeight, screenHeight); 

        if (coinIsHeads)
        {
            positionX += screenWidth;
        }
        if (!coinIsHeads)
        {
            positionX = positionX * -1 - screenWidth;
        }

        GameObject generatedObstacle = Instantiate(_obstaclesPrefabs, new Vector3(positionX, positionY, 0), Quaternion.identity);
        if (generatedObstacle.transform.position.x > 0)
        {
            generatedObstacle.GetComponent<Obstacle>().IsLtr = false;
        }
        generatedObstacle.transform.parent = transform;
        generatedObstacle.SetActive(true);
    }
}
