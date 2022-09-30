using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _DeathBody;

    [SerializeField] private GameObject _WayPrefab;


    [SerializeField] private PathType _PathType = PathType.CatmullRom;
    [SerializeField] private float _MoveSpeed;
    [SerializeField] private float _MovementStartDelay;
    private GameObject _way;
    public RectTransform MovingLine;
    public Transform SpawnPoint;

    private Rigidbody2D _targetRigidbody;
    public Vector2[] _waypoints = new Vector2[4];

    private List<Transform> _fixedPoints = new List<Transform>();

    private Tween t;



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
        _targetRigidbody = GetComponent<Rigidbody2D>();
        _way = Instantiate(_WayPrefab);

        for (int i = 0; i < _way.transform.childCount; i++)
        {
            _fixedPoints.Add(_way.transform.GetChild(i));
        }

        Player.Instance.EnemyKilled += RetreatInvoke;

        _MoveSpeed = Random.Range(3.5f, 5);

        Invoke("StartMovement", _MovementStartDelay);
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
        t.Kill();
        Player.Instance.EnemyKilled -= RetreatInvoke;
    }

    public void Set(RectTransform line)
    {
        MovingLine = line;
    }


    public void Death()
    {
        Instantiate(_DeathBody, transform.position, Quaternion.identity);
        Destroy(_way);
        Destroy(gameObject);
    }

    #region Movement
    private void StartMovement()
    {
        transform.position = SpawnPoint.position;
        StartCoroutine(CurveMove(false));
    }

    private void RetreatInvoke()
    {
        StopAllCoroutines();
        StartCoroutine(CurveMove(true));
    }
    private IEnumerator CurveMove(bool retreat)
    {
        yield return new WaitForSeconds(_MovementStartDelay);

        _way.transform.position = new Vector3(0, Camera.main.ScreenToWorldPoint(MovingLine.transform.position).y, 0);

        Vector2 currentPosition = transform.position;

        bool IsOnTheLeftSide = transform.position.x < 0;

        switch (IsOnTheLeftSide)
        {
            case true:
                _waypoints[0] = currentPosition;
                _waypoints[1] = _fixedPoints[0].position;
                _waypoints[2] = _fixedPoints[1].position;
                break;
            case false:
                _waypoints[0] = currentPosition;
                _waypoints[1] = _fixedPoints[3].position;
                _waypoints[2] = _fixedPoints[2].position;
                break;
        }

        t.Kill();
        t = _targetRigidbody.DOPath(_waypoints, retreat ? _MoveSpeed * 1.6f : _MoveSpeed*1.6f, _PathType, PathMode.TopDown2D,5).SetSpeedBased().OnComplete(Move);
        t.SetEase(retreat ? Ease.OutSine : Ease.OutSine);
    }
    private void Move()
    {
        Vector2 waypoint;

        bool IsOnTheLeftSide = transform.position.x < 0;
        waypoint = IsOnTheLeftSide ? _fixedPoints[1].position : _fixedPoints[2].position;
        t.Kill();
        t = _targetRigidbody.DOMove(waypoint, _MoveSpeed).SetSpeedBased().SetEase(Ease.InOutSine);
        t.SetLoops(-1, LoopType.Yoyo);
    }

    #endregion
}
