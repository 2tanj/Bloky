using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour, IObstacle
{
    public void OnPlayerTouch()
    {
        PlayerController.Instance.ResetJumps();
    }
}
