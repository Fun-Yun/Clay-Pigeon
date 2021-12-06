using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLauncher : MonoBehaviour
{
    
    public float launchVelocity = 10f; //projectile launch velocity
    public float launchAngle = 30f; //angle projectile is fired at
    public float Gravity = -9.8f; //gravity that effects projectiles

    public Vec3 v3InitialVelocity = new Vec3(); //Launch Velocity as Vector
    public Vec3 v3CurrentVelocity = new Vec3(); //The projectiles current velocity
    private Vec3 v3Acceleration; //Vector quantity for acceleration

    private float airTime = 0f; //how long the projectile is airborne
    private float xDisplacement = 0f; //how far the projectile will travel in the horizontal plane

    private bool simulate = false;

    //Variables for drawing projectile path
    private List<Vec3> pathPoints;
    private int simulationSteps = 30; 


    void Start()
    {
        pathPoints = new List<Vec3>();
        calculateProjectile();
        calculatePath();
    }

    // Update is called once per frame
    void Update()
    {
        if(simulate == false)
        {
            pathPoints = new List<Vec3>();
            calculateProjectile();
            calculatePath();
        }
        drawPath();

        if (Input.GetKeyDown(KeyCode.Space) && simulate == false)
        {
            simulate = true;
            v3CurrentVelocity = v3InitialVelocity;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            simulate = false;
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    private void calculateProjectile()
    {
        //work out velocity as vector quantity

        v3InitialVelocity.x = launchVelocity * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        v3InitialVelocity.y = launchVelocity * Mathf.Sin(launchAngle * Mathf.Deg2Rad);

        //gravity as vec3
        v3Acceleration = new Vec3(0f, Gravity, 0f);

        //calculate total airtime
        float finalYVelocity = 0f;
        airTime = 2f * (finalYVelocity - v3InitialVelocity.y) / v3Acceleration.y;
    }


    private void calculatePath()
    {
        Vec3 launchPos = new Vec3(transform.position);
        pathPoints.Add(launchPos);

        for(int i = 0; i <= simulationSteps; ++i)
        {
            float simTime = (i / (float)simulationSteps) * airTime;
            //suvat formula for displacement s = ut + 1/2at^2                        s=displacement u=initalVel v=finalVel a=accel t=time
            Vec3 displacement = v3InitialVelocity * simTime + v3Acceleration * simTime * simTime * 0.5f;
            Vec3 drawPoint = launchPos + displacement;
            pathPoints.Add(drawPoint);
        }
    }

    void drawPath()
    {
        for(int i = 0; i < pathPoints.Count-1; ++i)
        {
            Debug.DrawLine(pathPoints[i].ToVector3(), pathPoints[i + 1].ToVector3(), Color.green);
        }
    }



    private void FixedUpdate()
    {
        if (simulate)
        {
            Vec3 currentPos = new Vec3(transform.position);

            //work out current vel
            v3CurrentVelocity += v3Acceleration * Time.deltaTime;

            //work out displacement
            Vec3 displacement = v3CurrentVelocity * Time.deltaTime;
            currentPos += displacement;
            transform.position = currentPos.ToVector3();


        }
    }

}
