using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class EnemyMovingLines : SingletonBase<EnemyMovingLines>
{
    
    [SerializeField] private RectTransform[] _lines;
    
    public Vector2 GetRandomMovingLine()
    {
        var line = _lines[Random.Range(0, _lines.Length)];

        Vector2 movingLine = Camera.main.ScreenToWorldPoint(line.transform.position);

        return movingLine;
    }
}
