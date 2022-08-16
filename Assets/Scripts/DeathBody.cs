using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBody : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 15);
    }
}
