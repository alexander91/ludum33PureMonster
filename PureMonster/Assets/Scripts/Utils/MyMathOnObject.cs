using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class MyMathOnObject
{
    public static void setupRotation(Transform transform, Vector3 from, Vector3 to, float adding = 0f)
    {
        Vector2 vectorL = to - from;
        float angleZ = Mathf.Rad2Deg * (Mathf.Atan2(vectorL.y, vectorL.x));
        transform.eulerAngles = new Vector3(0, 0, angleZ + adding);
    }
}

