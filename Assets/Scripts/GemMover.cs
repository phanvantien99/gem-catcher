using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMover : MonoBehaviour
{
    public float speed = 5f;

    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger collision here");
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.Play();
                ScoreManager.addScore(1);
            }
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

}
