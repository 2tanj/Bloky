using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJumpObstacle : MonoBehaviour, IObstacle
{
    public void OnPlayerTouch()
    {
        PlayerController.Instance.AddJump();
        Destroy(gameObject);
    }

    public GameObject GetGameObject() => gameObject;
}
