using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec2
{
    public float x = 0f;
    public float y = 0f;

    public Vec2(){ //constructor
        x = 0f;
        y = 0f;
    }

    public Vec2(float a_x, float a_y) //arguments constructor (allows assignment of values into vec2)
    {
        x = a_x;
        y = a_y;
    }

    //functions to convert to the unity standard vector 2 and 3
    
    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
    
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, 0f);
    }


    //operator overloading

    public static Vec2 operator +(Vec2 a, Vec2 b) //addition overloading
    {
        return new Vec2(a.x + b.x, a.y + b.y);
    }

    public static Vec2 operator -(Vec2 a, Vec2 b) //subtraction overloading
    {
        return new Vec2(a.x - b.x, a.y - b.y);
    }
    public static Vec2 operator *(Vec2 a, Vec2 b) //multiplication overloading
    {
        return new Vec2(a.x * b.x, a.y * b.y);
    }

    public static Vec2 operator *(Vec2 a, float s) //Scalar Multiplication
    {
        return new Vec2(a.x * s, a.y * s);
    }

    public static Vec2 operator -(Vec2 a) //Negate operator e.g. Vec2 c = -a
    {
        return new Vec2(-a.x, -a.y);
    }

    public float Magnitude() //returns magnitude of vector
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public float MagnitudeSquared(){
        return x * x + y * y;
    }

    //Dot product - Projection of 1 vector onto another or the cos(theta) angle between unit length vectors

    public float DotProduct(Vec2 a_b) { //float dp = a.DotProduct(b);
        return x * a_b.x + y * a_b.y;
    }

    public static float DotProduct(Vec2 a, Vec2 b) //float dp = DotProduct(a, b);
    {
        return a.x * b.x + a.y * b.y;
    }

    //Normalize vec2 to just directional 

    public float Normalize()
    {
        float mag = Magnitude();
        float invMag = (mag != 0f) ? 1f / mag : 1.00e+12f;
        x *= invMag;
        y *= invMag;
        return mag;

    }

    //Get the perpendicular vector
    public Vec2 Perpendicular()
    {
        return new Vec2(-y, x);
    }

    //rotate 2d vector around imaginary z axis
    public void Rotate(float angle)
    {
        float fX = x;
        x = fX * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        y = fX * Mathf.Sin(angle) + y * Mathf.Cos(angle);
    }


}
