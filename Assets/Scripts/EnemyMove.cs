using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D TargetRigidbody;
    public PathType pathType = PathType.CatmullRom;
    public GameObject WayPrefab;
    private Vector2[] waypoints;
    public float moveSpeed;

    private List<Transform> fixedPoints;

    public RectTransform MovingLine;
    public Enemy myself;

    private GameObject _way;
    private Tween t;

    private void Start()
    {
        //MovingLine = myself.MovingLine;
        _way = Instantiate(WayPrefab);

        for (int i = 0; i < _way.transform.childCount; i++)
        {
            fixedPoints.Add(_way.transform.GetChild(i));
        }

        Player.Instance.EnemyKilled += FirstMoveInvoke;

        StartCoroutine(FirstMove());
    }

    private void OnDestroy()
    {
        Player.Instance.EnemyKilled -= FirstMoveInvoke;
    }

    private void FirstMoveInvoke()
    {
        _way.transform.position = new Vector3(0, Camera.main.ScreenToWorldPoint(MovingLine.transform.position).y, 0);
        StartCoroutine(FirstMove());
    }
    private IEnumerator FirstMove()
    {
        yield return new WaitForSeconds(0.6f);

        Vector2 currentPosition = transform.position;

        bool IsOnTheLeftSide = transform.position.x < 0;

        switch(IsOnTheLeftSide)
        {
            case true:
                waypoints[0] = currentPosition;
                waypoints[1] = fixedPoints[1].position;
                waypoints[2] = fixedPoints[0].position;
                break;
            case false:
                waypoints[0] = currentPosition;
                waypoints[1] = fixedPoints[0].position;
                waypoints[2] = fixedPoints[1].position;
                break;
        }
        
        t.Kill();
        t = TargetRigidbody.DOPath(waypoints, moveSpeed, pathType).SetOptions(true).OnComplete(Move);
        t.SetEase(Ease.OutCubic).SetLoops(0);
    }

    private void Move()
    {
        waypoints = new Vector2[0];
        bool IsOnTheLeftSide = transform.position.x < 0;
        switch (IsOnTheLeftSide)
        {
            case true:
                waypoints[1] = fixedPoints[0].position;
                waypoints[2] = fixedPoints[1].position;
                break;
            case false:
                waypoints[1] = fixedPoints[1].position;
                waypoints[2] = fixedPoints[0].position;
                break;
        }
        t = TargetRigidbody.DOPath(waypoints, moveSpeed, pathType).SetOptions(true);
        t.SetEase(Ease.OutCubic).SetLoops(-1);
    }
}
