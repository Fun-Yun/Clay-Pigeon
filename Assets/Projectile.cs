using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vec3 v3CurrentVelocity = new Vec3(0f, 0f, 0f); //launch vel as a vector
    private Vec3 v3Acceleration = new Vec3(0f, -9.8f, 0f); //Acceleration vector quantity, -9.8f = gravity

    private float m_Lifespan = 0f;

    public void SetVelocity(Vec3 a_vel)
    {
        v3CurrentVelocity = a_vel;
    }

    public void SetAcceleration(Vec3 a_acceleration)
    {
        v3Acceleration = a_acceleration;
    }

    public void SetLifeTime(float a_lifespan)
    {
        m_Lifespan = a_lifespan;
    }

    private void FixedUpdate()
    {
        m_Lifespan -= Time.deltaTime;

        Vec3 currentPos = new Vec3(transform.position);
        //work out current velocity
        v3CurrentVelocity += v3Acceleration * Time.deltaTime;
        //work out displacement
        Vec3 displacement = v3CurrentVelocity * Time.deltaTime;
        currentPos += displacement;
        transform.position = currentPos.ToVector3();

        if(m_Lifespan < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Debug.Log(v3CurrentVelocity.z);
        
    }

}
