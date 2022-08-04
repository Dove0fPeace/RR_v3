using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour
{
    private Vector2 _topPosition;
    public Vector2 TopPosition => _topPosition;

    private float _halfYSize;
    public float HalfYSize => _halfYSize;

    private BoxCollider2D _col;

    void Start()
    {
        _col = GetComponent<BoxCollider2D>();
        _halfYSize = _col.size.y / 2;
        _topPosition = new Vector2(transform.position.x ,transform.position.y + _halfYSize);
    }

}
