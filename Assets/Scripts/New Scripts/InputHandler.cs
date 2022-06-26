using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public void Input()
    {
        Player.Instance.Move();
    }
}
