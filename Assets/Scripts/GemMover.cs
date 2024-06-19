using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMover : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private int boostPoint = 0;

    [SerializeField] private int _boostTime;

    [SerializeField] private bool _isBoostedItem;

    [SerializeField] private int _boostStatTime;

    [SerializeField] private bool _isToxicItem;

    Action<GemMover> _onAddPoint;
    Action<GemMover> _onSubstractPoint;

    Action<GemMover> _onBoostingStat;
    public int BoostPoint { get => boostPoint; set => boostPoint = value; }
    public int BoostTime { get => _boostTime; set => _boostTime = value; }
    public bool IsBoostedItem { get => _isBoostedItem; set => _isBoostedItem = value; }
    public bool IsToxicItem { get => _isToxicItem; set => _isToxicItem = value; }
    public int BoostStatTime { get => _boostStatTime; set => _boostStatTime = value; }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            // invoke action substract
            if (_isToxicItem)
            { 
                _onSubstractPoint.Invoke(this);
            }
            // invoke action boost speed
            else if (_isBoostedItem)
            {
                _onBoostingStat.Invoke(this);
            }
            else
            {
                _onAddPoint.Invoke(this);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    // initialize data to assign action
    public void InitData(Action<GemMover> addPointAction, Action<GemMover> minusPointAction, Action<GemMover> boostStatAction)
    {
        _onAddPoint = addPointAction;
        _onSubstractPoint = minusPointAction;
        _onBoostingStat = boostStatAction;
    }
}
