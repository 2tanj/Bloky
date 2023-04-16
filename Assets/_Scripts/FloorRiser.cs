using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRiser : MonoBehaviour
{
    public static FloorRiser Instance;
    
    // https://docs.unity3d.com/Manual/EditingCurves.html
    [SerializeField]
    private AnimationCurve _testing;

    public bool IsRising { get; private set; } = false;


    void Awake() => Instance = this;

    void Update()
    {
        //Debug.Log("value: " + _testing.Evaluate(Time.fixedTime).ToString("F3") + " time: " + Time.fixedTime);


        if (IsRising)
            RaiseFloor();
    }

    void Start()
    {
        Debug.Log(_testing.Evaluate(2f));
    }

    private void RaiseFloor()
    {
        var newPos = transform.position;
        newPos.y += _testing.Evaluate(Time.fixedTime);
        //newPos.y += .25f;

        transform.position = newPos;
    }

    public void ToggleRising()
    {
        IsRising = !IsRising;

        if (IsRising)
            GetComponent<SpriteRenderer>().color = Color.red;
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    public bool IsFloorOnScreen() => 
        GeometryUtility.TestPlanesAABB(
            GeometryUtility.CalculateFrustumPlanes(
                Camera.main), GetComponent<SpriteRenderer>().bounds);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IObstacle>(out var obstacle))
        {
            Destroy(obstacle.GetGameObject());
        }
    }
}
