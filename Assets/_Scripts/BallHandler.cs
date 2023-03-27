using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _ballDelay = .5f;

    [SerializeField] private Rigidbody2D _ball;
    [SerializeField] private Rigidbody2D _pivot;

    private SpringJoint2D _springJoint;

    private bool _isDragging;
    private bool _isPivotSet;

    private void Start()
    {
        _camera = Camera.main;
        _springJoint = _ball.GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        if (_ball == null)
            return;

        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isDragging)
                LaunchBall();

            _isDragging = false;
            return;
        }

        var touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
        var worldPos = _camera.ScreenToWorldPoint(touchPos);

        if (!_isPivotSet)
        {
            _pivot.transform.position = worldPos;

            //_pivot.position = worldPos;
            _isPivotSet     = true;
        }

        _ball.isKinematic = true;
        _ball.position    = worldPos;

        _isDragging = true;
    }

    private void LaunchBall()
    {
        _ball.isKinematic = false;
        _ball = null;

        Invoke(nameof(DetachBall), _ballDelay);
    }

    private void DetachBall()
    {
        _springJoint.enabled = false;
        _springJoint = null;
    }
}
