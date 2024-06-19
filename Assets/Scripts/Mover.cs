using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float _speed = 0.5f;
    [SerializeField] private float jumpPower = 7.0f;
    [SerializeField] private GameObject _groundDetecter;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _timeToJump;
    [SerializeField] private int _jumpRemaining;

    private Animator _animator;

    public GameObject objectManager;

    private GameManager _gameManager;

    private bool _isGrounded;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _gameManager = objectManager.GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _jumpRemaining = _timeToJump;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsGameOver) return;
        _moveHorizontal();
        _jump();
    }

    private void _moveHorizontal()
    {
        // calculate
        float horizontal = Input.GetAxis("Horizontal");
        float moveHorizontal = horizontal * _speed * Time.deltaTime;
        bool isMoving = moveHorizontal != 0;

        // handle animation
        _animator.SetBool("isMoving", isMoving);
        // move
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x + moveHorizontal, transform.position.y);
        }
    }

    private void _jump()
    {
        // OverlapCircle return boolean if some thing with layer mask is fall in the radius (.2f) of circle of _ground detecter
        _isGrounded = Physics2D.OverlapCircle(_groundDetecter.transform.position, .2f, _layerMask);
        // float isJump = Input.GetAxis("Jump");
        // float jumpVelocity = isJump * jumpPower;

        if (Input.GetKeyDown(KeyCode.Space) && (_isGrounded || _jumpRemaining > 0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpPower);
            _jumpRemaining--;
        }
        else if (_isGrounded)
        {
            _jumpRemaining = _timeToJump;
        }
    }
}
