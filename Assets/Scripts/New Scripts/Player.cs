using System;
using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SingletonBase<Player>
{
    public event Action EnemyKilled;
    
    [SerializeField] private UnityEvent _deathEvent;
    
    [SerializeField] private float _Speed;

    private Rigidbody2D _rb;

    private bool _isMoving = false;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.AddForce(Vector2.up * _Speed, ForceMode2D.Impulse);
        }
    }

    public void Move()
    {
        if(_isMoving) return;
        
        _isMoving = true;
    }

    public void Death()
    {
        Stop();
        _deathEvent?.Invoke();
    }

    private void Stop()
    {
        _isMoving = false;
        _rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        print("collision");
        var enemy = other.transform.root.GetComponent<Enemy>();
        if (enemy != null)
        {
            print("enemy != null");
            enemy.Death();
            EnemyKilled?.Invoke();
        }
        Stop();
    }

}
