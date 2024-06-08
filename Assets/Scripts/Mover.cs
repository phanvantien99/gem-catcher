using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float _speed = 0.5f;

    private Animator _animator;

    public GameObject objectManager;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = objectManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameOver) return;
        float horizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = horizontal * _speed * Time.deltaTime;
        bool isMoving = moveHorizontal != 0;

        _animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y);
        }
    }
}
