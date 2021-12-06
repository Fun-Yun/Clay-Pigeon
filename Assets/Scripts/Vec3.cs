using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec3
{
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;

    public Vec3()
    { //constructor
        x = 0f;
        y = 0f;
        z = 0f;
    }

    public Vec3(float a_x, float a_y, float a_z) //arguments constructor (allows assignment of values into Vec3)
    {
        x = a_x;
        y = a_y;
        z = a_z;
    }

    //functions to convert to the unity standard vector 3

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z); 
    }

    public Vec3(Vector3 a_pos) //pass in transform.position for example
    {
        x = a_pos.x;
        y = a_pos.y;
        z = a_pos.z;
        Debug.Log("here" + x + y + z);

    }

    //operator overloading

    public static Vec3 operator +(Vec3 a, Vec3 b) //addition overloading
    {
        return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vec3 operator -(Vec3 a, Vec3 b) //subtraction overloading
    {
        return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static Vec3 operator *(Vec3 a, Vec3 b) //multiplication overloading
    {
        return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
    }

    public static Vec3 operator *(Vec3 a, float s) //Scalar Multiplication
    {
        return new Vec3(a.x * s, a.y * s, a.z * s);
    }

    public static Vec3 operator *(float s, Vec3 a)
    {
        return new Vec3(a.x * s, a.y * s, a.z * s);
    }

    public static Vec3 operator -(Vec3 a) //Negate operator e.g. Vec3 c = -a
    {
        return new Vec3(-a.x, -a.y, -a.z);
    }

    public float Magnitude() //returns magnitude of vector
    {
        return Mathf.Sqrt(x * x + y * y + z * z);
    }

    public float MagnitudeSquared()
    {
        return x * x + y * y + z * z;
    }

    //Dot product - Projection of 1 vector onto another or the cos(theta) angle between unit length vectors

    public float DotProduct(Vec3 a_b)
    { //float dp = a.DotProduct(b);
        return x * a_b.x + y * a_b.y + z * a_b.z;
    }

    public static float DotProduct(Vec3 a, Vec3 b) //float dp = DotProduct(a, b);
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    //Normalize Vec3 to just directional 

    public float Normalize()
    {
        float mag = Magnitude();
        float invMag = (mag != 0f) ? 1f / mag : 1.00e+12f;
        x *= invMag;
        y *= invMag;
        z *= invMag;
        return mag;

    }

    //Get the crossproduct
    public static Vec3 CrossProduct(Vec3 a, Vec3 b)
    {
        return new Vec3(a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x);
    }

    //rotate 2d vector around z axis
    public void RotateZ(float angle)
    {
        float fX = x;
        x = fX * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        y = fX * Mathf.Sin(angle) + y * Mathf.Cos(angle);
    }
    
    //rotate 2d vector around y axis
    public void RotateY(float angle)
    {
        float fX = x;
        x = fX * Mathf.Cos(angle) - z * Mathf.Sin(angle);
        z = fX * Mathf.Sin(angle) + z * Mathf.Cos(angle);
    }
    
    //rotate 2d vector around x axis
    public void RotateX(float angle)
    {
        float fY = y;
        y = fY * Mathf.Cos(angle) - z * Mathf.Sin(angle);
        z = fY * Mathf.Sin(angle) + z * Mathf.Cos(angle);
    }

}
