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

    [SerializeField] private Animator _Animator;   
    
    [SerializeField] private UnityEvent _deathEvent;
    
    [SerializeField] private float _Speed;

    private Rigidbody2D _rb;

    private bool _isMoving = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Time.timeScale = 0.3f;
    }

    private void Update()
    {
        if (_isMoving)
        {
            //_rb.AddForce(Vector2.up * _Speed, ForceMode2D.Impulse);
            
            transform.Translate(Vector2.up * _Speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        _Animator.SetBool("IsAttack", _isMoving);
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
        _Animator.SetBool("IsHited",  false);
        _isMoving = false;
        _rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var enemy = other.transform.root.GetComponent<Enemy>();
        if (enemy != null)
        {
            _Animator.SetBool("IsHited",  true);
            //Debug.Log(Animator.IsHited);
            enemy.Death();
            EnemyKilled?.Invoke();
        }
        Stop();
    }

}
