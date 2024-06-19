using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool isLtr = true;

    public bool IsLtr { get => isLtr; set => isLtr = value; }
    private float screenWidth;
    private float verticalToDestroy = 6;
    void Start()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        verticalToDestroy += screenWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLtr)
        {
            // move the transform
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            if (transform.position.x >= verticalToDestroy)
                Destroy(gameObject);

        }
        else
        {
            transform.Translate(_speed * Time.deltaTime * -1, 0, 0);
            if (transform.position.x <= -verticalToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
