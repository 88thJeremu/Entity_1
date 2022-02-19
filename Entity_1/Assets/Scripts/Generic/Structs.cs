using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This file contains different "structs" that are generally useful in any Unity3D project.
// It seems as though Coulombic does not use any of these structs. Perhaps it did at some point...
// Anyway, this file could be removed from the project. It was probably copied here awhile ago from
// another "Generic" folder. That's the whole point of the "Generic" folder - to re-use code between projects.

/// <summary>
/// A "Vector2" type in Unity3D uses floats (real numbers). This means that you can have a 2D vector like (1.5,1).
/// So this struct is much like Vector2, except instead 
/// of floats (real numbers) it uses whole integers.
/// </summary>
public struct IntVector2 {
    public int x, y;
    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public override string ToString()
    {
        return "IntVector2 ("+x+", "+y+")";
    }

    public static IntVector2 GetReversed(IntVector2 vector)
    {
        return new IntVector2(-vector.x, -vector.y);
    }

    public float GetMagnitude()
    {
        return new Vector2(x, y).magnitude;
    }
}

public struct RectangularPrism
{
    public float x, y, z;
    public float width, height, depth;
    public RectangularPrism(float x, float y, float z, float width, float height, float depth)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.width = width;
        this.height = height;
        this.depth = depth;
    }

    public override string ToString()
    {
        return "RectangularPrism coord(" + x + ", " + y + "," + z + ") size(" + width + "," + height + "," + depth + ")";
    }

    public Vector3 GetSizeVector()
    {
        return new Vector3(width, height, depth);
    }

    public Vector3 GetPositionVector()
    {
        return new Vector3(x, y, z);
    }
}

/// <summary>
/// See IntVector2, except this is three dimensional.
/// </summary>
public struct IntVector3
{
    public int x, y,z;
    public IntVector3(int x, int y,int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public IntVector3(float x, float y, float z)
    {
        this.x = Mathf.FloorToInt(x);
        this.y = Mathf.FloorToInt(y);
        this.z = Mathf.FloorToInt(z);
    }
    public override string ToString()
    {
        return "IntVector3 (" + x + ", " + y + ","+z+")";
    }
}
