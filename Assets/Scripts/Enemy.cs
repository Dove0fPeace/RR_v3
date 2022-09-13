using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _DeathBody;

    [SerializeField] private GameObject _WayPrefab;

    [SerializeField] private float _StartDelay;

    [SerializeField] private float m_RetreatSpeed;

    private List<Transform> _routes;

    private float tParam;
    private float speedModifier;

    private int routeToGo;

    private bool IsOnTheLeftSide;
    private bool _start;
    private bool coroutineAllowed;
    private bool _goMove = false;

    private Vector2 position;

    private Vector2 _startPosition;

    private GameObject _way;
    public RectTransform MovingLine;


    private static HashSet<Enemy> m_Enemies;
    public static IReadOnlyCollection<Enemy> Enemies => m_Enemies;

    private void OnEnable()
    {
        if (m_Enemies == null)
        {
            m_Enemies = new HashSet<Enemy>();
        }
        m_Enemies.Add(this);
    }


    private void Start()
    {
        _routes = new List<Transform>();

        _way = Instantiate(_WayPrefab, new Vector3(0, Camera.main.ScreenToWorldPoint(MovingLine.transform.position).y, 0), Quaternion.Euler(0, 0, 0));

        for (int i = 0; i < _way.transform.childCount; i++)
        {
            _routes.Add(_way.transform.GetChild(i));
        }

        speedModifier = Random.Range(0.65f, 1.1f);

        tParam = 0f;

        PrepareToMove(false);

        Player.Instance.EnemyKilled += RetreatInvoke;

        StartCoroutine(StartMovementDelay());
    }

    private void Update()
    {
        if (_goMove == false) return;

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

    private void OnDestroy()
    {
        m_Enemies.Remove(this);
        Player.Instance.EnemyKilled -= RetreatInvoke;
    }

    public void SetMovingLine(RectTransform line)
    {
        MovingLine = line;
    }

    public void RetreatInvoke()
    {
        StartCoroutine(Retreat());
    }

    public void Death()
    {
        Instantiate(_DeathBody, transform.position, Quaternion.identity);
        Destroy(_way);
        Destroy(gameObject);
    }

    private void PrepareToMove(bool retreat)
    {
        

        coroutineAllowed = false;

        IsOnTheLeftSide = transform.position.x < 0;
        
        switch(retreat)
        {
            case true:
                routeToGo = IsOnTheLeftSide ? 0 : 1;
                break;
            case false:
                routeToGo = IsOnTheLeftSide ? 0 : 1;
                break;
        }

        _startPosition = transform.position;

        _start = true;
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

        tParam = 0f;

        coroutineAllowed = true;

    }

    private IEnumerator StartMovementDelay()
    {
        yield return new WaitForSeconds(_StartDelay);

        _way.transform.position = new Vector3(0, Camera.main.ScreenToWorldPoint(MovingLine.transform.position).y, 0);

        _goMove = true;
    }

    private IEnumerator Retreat()
    {
        yield return new WaitForSeconds(0.6f);

        coroutineAllowed = false;
        StopAllCoroutines();
        tParam = 0f;
        _way.transform.position = new Vector3(0, Camera.main.ScreenToWorldPoint(MovingLine.transform.position).y, 0);
        PrepareToMove(true);
    }
}
