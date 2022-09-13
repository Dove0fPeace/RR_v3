using UnityEngine;

public class Way : MonoBehaviour
{
    [SerializeField] private Transform[] _Points;

    private Vector2 _gizmosPosition;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(float t = 0; t <= 1; t += 0.05f)
        {
            _gizmosPosition = Mathf.Pow(1 - t, 3) * _Points[0].position +
                             3 * Mathf.Pow(1 - t, 2) * t * _Points[1].position +
                             3 * (1 - t) * Mathf.Pow(t, 2) * _Points[2].position +
                             Mathf.Pow(t, 3) * _Points[3].position;

            Gizmos.DrawSphere(_gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(_Points[0].position.x, _Points[0].position.y), new Vector2(_Points[1].position.x, _Points[1].position.y));

        Gizmos.DrawLine(new Vector2(_Points[2].position.x, _Points[2].position.y), new Vector2(_Points[3].position.x, _Points[3].position.y));
    }
#endif
}
