using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : BasicObstacle
{
    [SerializeField, Range(0, 100)]
    private float _rotationSpeed = 10f;

    void Update() =>
        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * _rotationSpeed);
}
