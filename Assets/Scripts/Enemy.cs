using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _DeathBody;

    [SerializeField] private GameObject _Route;
    
    private List<Transform> _routes;
    
    private float tParam;
    private float speedModifier;

    private int routeToGo;
    
    private bool SpawnInLeftSide;
    private bool _start;
    private bool coroutineAllowed;
    
    private Vector2 position;

    private Vector2 _startPosition;

    private GameObject _route;


    private void Start()
    {
        _routes = new List<Transform>();
        _route = Instantiate(_Route, new Vector3(0, EnemyMovingLines.Instance.GetRandomMovingLine().y, 0), Quaternion.Euler(0,0,0));

        for (int i = 0; i < _route.transform.childCount; i++)
        {
            _routes.Add(_route.transform.GetChild(i));
        }

        SpawnInLeftSide = transform.position.x < 0;
        
        routeToGo = SpawnInLeftSide ? 0 : 1;

        speedModifier = Random.Range(0.25f,1.2f);
        tParam = 0f;
        coroutineAllowed = false;
        _start = true;

        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_start)
        {
            StartCoroutine(StartMove());
        }
        
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player.Instance.Death();
        }
    }

    public void Death()
    {
        Instantiate(_DeathBody, transform.position, Quaternion.identity);
        Destroy(_route);
        Destroy(gameObject);
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = _routes[routeNumber].GetChild(0).position;
        Vector2 p1 = _routes[routeNumber].GetChild(1).position;
        Vector2 p2 = _routes[routeNumber].GetChild(2).position;
        Vector2 p3 = _routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            
            position = Mathf.Pow(1 - tParam, 3) * p0 +
                       3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                       3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                       Mathf.Pow(tParam, 3) * p3;

            transform.position = position;
            
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > _routes.Count - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }

    private IEnumerator StartMove()
    {
        _start = false;
        
        Vector2 p0 = _startPosition;
        Vector2 p1 = _routes[routeToGo].GetChild(3).position;
        Vector2 p2 = _routes[routeToGo].GetChild(1).position;
        Vector2 p3 = _routes[routeToGo].GetChild(0).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            
            position = Mathf.Pow(1 - tParam, 3) * p0 +
                       3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                       3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                       Mathf.Pow(tParam, 3) * p3;

            transform.position = position;
            
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;

        coroutineAllowed = true;
        
    }
}
