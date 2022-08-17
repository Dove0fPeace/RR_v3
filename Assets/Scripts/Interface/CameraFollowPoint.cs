using System;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour
{
    [SerializeField] private float _MovingSpeed;

    [SerializeField] private bool _StopMoving;
    
    private void Start()
    {
        Player.Instance.EnemyKilled += MoveToPLayer;
        MoveToPLayer();
    }

    private void Update()
    {
        if(_StopMoving) return;
        ForwardMoving();
    }

    private void OnDestroy()
    {
        Player.Instance.EnemyKilled -= MoveToPLayer;
    }

    private void MoveToPLayer()
    {
        transform.position = Player.Instance.transform.position;
    }

    private void ForwardMoving()
    {
        transform.Translate(Vector2.up * _MovingSpeed * Time.deltaTime);
    }
}
