using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 7;
    private Vector2 _lastDirection;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        StartCoroutine(ActivateBall(2));
    }

    void ServeBall() {
        float randomY = UnityEngine.Random.Range(-0.7f, 0.7f);
        Vector2 serveDirection = new Vector2(1, randomY).normalized;
        _rb.velocity = serveDirection * speed;
        _lastDirection = serveDirection;
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Goal")) {
            StartCoroutine(ActivateBall(2));
            return;
        }
        
        Vector2 collisionNormal = collision.GetContact(0).normal;
        Vector2 newDirection = Vector2.Reflect(_lastDirection, collisionNormal).normalized;
        _rb.velocity = newDirection * speed;
        _lastDirection = newDirection;
    }

    IEnumerator ActivateBall(int startDelay) {
        _rb.transform.position = Vector2.zero;
        _rb.velocity = Vector2.zero;
        yield return new WaitForSecondsRealtime(startDelay);
        ServeBall();
    }
}
