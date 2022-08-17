using System.Collections;
using System.Collections.Generic;
using _Imported;
using UnityEngine;

public class EnemyMovingLines : SingletonBase<EnemyMovingLines>
{
    
    [SerializeField] private RectTransform[] _lines;

    public RectTransform[] GetRandomMovingLines(int size)
    {
        List<RectTransform> list = new List<RectTransform>();

        int tries = 0;
        int maxTries = _lines.Length;

        while (tries < maxTries && list.Count < size)
        {
            var element = _lines[Random.Range(0, _lines.Length)];

            if(!list.Contains(element))
            {
                list.Add(element);
            }
            else
            {
                tries++;
            }
        }

        if(list.Count > 0)
        {
            return list.ToArray();
        }
        else
        {
            return null;
        }
    }
}
