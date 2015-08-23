using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class RandomUtils
{
    public static Vector3 getRandomVector(float l)
    {
        return l*UnityEngine.Random.insideUnitSphere;
    }
}

