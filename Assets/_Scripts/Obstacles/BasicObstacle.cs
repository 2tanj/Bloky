using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour, IObstacle
{
    public virtual void       OnPlayerTouch() => PlayerController.Instance.ResetJumps();
    public virtual GameObject GetGameObject() => gameObject;
}
