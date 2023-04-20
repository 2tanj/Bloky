using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static T RandomItem<T>(this T[] list) =>
        list[Random.Range(0, list.Length)];
}
