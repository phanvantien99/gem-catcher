using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boom : MonoBehaviour
{

    [SerializeField] private bool _isReady;
    [SerializeField] private GameObject _icon;
    [SerializeField] private ParticleSystem _vfxExplosion;

    [SerializeField] private AudioSource _soundExplosion;
    private Animator _animator;
    [SerializeField] private int _timeToReady;
    public bool IsReady { get => _isReady; set => _isReady = value; }

    // Start is called before the first frame update
    void Start()
    {
        _animator = _icon.GetComponent<Animator>();
        StartCoroutine(countForReady());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (this._isReady)
            {
                GameManager manager = transform.parent.GetComponent<BoomSpawner>().GameManager;
                if (manager)
                {
                    _icon.SetActive(false);
                    _vfxExplosion.Play();
                    _soundExplosion.Play();
                    manager.GameOver();
                }
            }
        }
    }

    private IEnumerator countForReady()
    {
        while (_timeToReady > 0)
        {
            yield return new WaitForSeconds(1.0f);
            _timeToReady--;
            yield return null;
        }
        _isReady = true;
        _animator.SetBool("isReady", true);
    }
}
