using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Camera _camera;

    private Rigidbody2D   _player;
    private TrailRenderer _trail;
    private LineRenderer  _trajectory;
    
    [Range(0, 10)]
    [SerializeField] private float _jumpPower = 1f, _maxDrag = 5f;

    private Color _currentColor => _canJump ? _canJumpColor : _cantJumpColor;
    [SerializeField] private Color _canJumpColor;
    [SerializeField] private Color _cantJumpColor;


    private Vector2 _startPoint, _endPoint;
    private bool    _isDragging = false;

    [SerializeField]
    private uint _maxJumps = 2;
    private uint _availableJumps;
    private bool _canJump => _availableJumps > 0;


    private int _maxHeight = 0;

    private void Awake() => Instance = this;

    private void Start()
    {
        _trajectory = GetComponent<LineRenderer>();
        _player     = GetComponent<Rigidbody2D>();
        _trail      = GetComponent<TrailRenderer>();

        _camera     = Camera.main;
    }

    private void Update()
    {
        HandleMaxHeight();
        HandleInput();
    }

    private void HandleMaxHeight()
    {
        if ((int)transform.position.y > _maxHeight)
        {
            _maxHeight = (int)transform.position.y;
            UIManager.Instance.UpdateMaxHeightText(_maxHeight.ToString());
        }
    }

    private void HandleInput()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            // on released
            if (_isDragging && _canJump)
                Jump();

            _isDragging = false;
            return;
        }

        // on pressed
        if (!_isDragging)
            _startPoint = GetWorldPosFromTouch();

        _isDragging = true;

        if (_canJump)
            DrawTrajectory();
    }

    private void Jump()
    {
        _endPoint = GetWorldPosFromTouch();

        var force = _startPoint - _endPoint;
        force     = Vector2.ClampMagnitude(force, _maxDrag) * _jumpPower;

        _player.velocity = Vector2.zero;
        _player.AddForce(force, ForceMode2D.Impulse);
        _availableJumps--;

        ChangeColor();
        _trajectory.positionCount = 0;

        if (!FloorRiser.Instance.IsRising)
            FloorRiser.Instance.ToggleRising();
    }

    private Vector3[] GetTrajectory()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        var pos = (Vector2)transform.position;

        float timestep   = Time.fixedDeltaTime / Physics2D.velocityIterations;
        var gravityAccel = Physics2D.gravity * rigidBody.gravityScale * timestep * timestep;

        var force = _startPoint - GetWorldPosFromTouch();
        force = Vector2.ClampMagnitude(force, _maxDrag) * (_jumpPower * 2);

        float drag = 1f - timestep * rigidBody.drag;
        var moveStep = force * timestep;

        var steps = force.magnitude * 50;
        var results = new Vector3[(int)steps];

        for (int i = 0; i < (int)steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;

            results[i] = pos;
        }

        return results;
    }

    private void DrawTrajectory()
    {
        var trajectory = GetTrajectory();
        _trajectory.positionCount = trajectory.Length;

        // set trajectory color based on trajectory.len(steps)
        _trajectory.SetPositions(trajectory);
    }

    private void ChangeColor()
    {
        _player.GetComponent<SpriteRenderer>().color = _currentColor;
        _trail.startColor                            = _currentColor;
    }

    private Vector2 GetWorldPosFromTouch() => 
        _camera.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag   == "Ground" && 
            GameManager.Instance.State == GameState.PLAYING)
        {
            Debug.Log("GameOver");
            GameManager.Instance.GameOver();
        }

        if (collision.gameObject.TryGetComponent<IObstacle>(out var obstacle))
            obstacle.OnPlayerTouch();
    }

    public void ResetJumps() 
    { 
        _availableJumps = _maxJumps;
        ChangeColor();
    }
}
