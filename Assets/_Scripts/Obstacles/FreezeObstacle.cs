using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeObstacle : MonoBehaviour, IObstacle
{
    public void OnPlayerTouch()
    {
        PlayerController.Instance.AddJump();
        PlayerController.Instance.Freeze();

        Destroy(gameObject);
    }

    public GameObject GetGameObject() => gameObject;
}
