using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclesPrefabs;

    [SerializeField] private float _timer;

    [SerializeField] private int _spawnInterval;

    private float previosYLocate;

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
        if (previosYLocate == 0)
        {
            previosYLocate = positionY;
        }
        else if (previosYLocate > 0 && positionY > 0 || previosYLocate < 0 && positionY < 0)
        {
            // if the previous wood is same side with current => change side Y
            positionY *= -1;
            previosYLocate = positionY;
        }
        // change position Y to prevent Z-index
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
