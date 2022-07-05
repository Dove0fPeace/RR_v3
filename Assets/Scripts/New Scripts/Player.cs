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
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (_isMoving)
        {
            //_rb.AddForce(Vector2.up * _Speed, ForceMode2D.Impulse);
            
            transform.Translate(Vector2.up * _Speed * Time.deltaTime);
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
        Time.timeScale = 0;
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
