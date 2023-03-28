using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRiser : MonoBehaviour
{
    // https://docs.unity3d.com/Manual/EditingCurves.html
    [SerializeField]
    private AnimationCurve _testing;

    private void Update()
    {
        Debug.Log("value: " + _testing.Evaluate(Time.fixedTime).ToString("F3") + " time: " + Time.fixedTime);

        RaiseFloor();
    }

    private void Start()
    {
        Debug.Log(_testing.Evaluate(2f));
    }

    private void RaiseFloor()
    {
        var newPos = transform.position;
        newPos.y += _testing.Evaluate(Time.fixedTime);
        transform.position = newPos;
    }
}
