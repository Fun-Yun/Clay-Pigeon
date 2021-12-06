using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{

    public float launchVelocity = 10f; //projectile launch velocity
    public float launchAngle = 30f;
    public float Gravity = -9.8f; //gravity that effects projectiles

    public GameObject projectile; //the object that is fired
    public GameObject launchPoint; //where the projectile is shot from

    private Vec3 v3InitialVelocity = new Vec3(); //Launch Velocity as Vector
    private Vec3 v3Acceleration; //Vector quantity for acceleration

    private float airTime = 0f; //how long the projectile is airborne
    private float horizontalDisplacement = 0f; //how far the projectile will travel in the horizontal plane

    private bool simulate = false;

    //Variables for drawing projectile path
    private List<Vec3> pathPoints;
    private int simulationSteps = 30; //# of points on the path, more points = smoother curve


    void Start()
    {
        pathPoints = new List<Vec3>();
        calculateProjectile();
        calculatePath();
    }


    private void calculateProjectile()
    {
        launchAngle = transform.parent.eulerAngles.x;
        float launchHeight = launchPoint.transform.position.y; //find vertical offset

       //velocity as vector quantity     
        v3InitialVelocity.x = 0f; 
        v3InitialVelocity.z = launchVelocity * Mathf.Cos(launchAngle * Mathf.Deg2Rad);
        v3InitialVelocity.y = launchVelocity * Mathf.Sin(launchAngle * Mathf.Deg2Rad);

        //transform v3velocity into world space direction else projectile will move down worlds z axis
        Vector3 txDirection = launchPoint.transform.TransformDirection(v3InitialVelocity.ToVector3());
        v3InitialVelocity = new Vec3(txDirection);

        v3Acceleration = new Vec3(0f, Gravity, 0f);

        airTime = Quadratic(v3Acceleration.y, v3InitialVelocity.y * 2f, launchHeight * 2f); //use quadratic formula to calculate total time in air

        horizontalDisplacement = airTime * v3InitialVelocity.z; //calculate total horizontal distance travelled before ground contact
       
    }

    private float Quadratic(float a, float b, float c)
    {
        if(0.0001 > Mathf.Abs(a)) //if a is near 0 then formula != true
        {
            return 0f;
        }
        float bb = b * b;
        float ac = a * c;
        float b4ac = Mathf.Sqrt(bb - 4f * ac);
        float t1 = (-b + b4ac) / (2f * a);
        float t2 = (-b - b4ac) / (2f * a);
        float t = Mathf.Max(t1, t2); //return highest in case of negative
        return t;
    }


    private void calculatePath()
    {
        Vec3 launchPos = new Vec3(launchPoint.transform.position);
        pathPoints.Add(launchPos);

        for (int i = 0; i <= simulationSteps; ++i)
        {
            float simTime = (i / (float)simulationSteps) * airTime;
            //suvat formula for displacement s = ut + 1/2at^2         s=displacement u=initalVel v=finalVel a=accel t=time
            Vec3 displacement = (v3InitialVelocity * simTime) + (0.5f * v3Acceleration * simTime * simTime);
            Vec3 drawPoint = launchPos + displacement;
            pathPoints.Add(drawPoint);
        }
    }

    void drawPath()
    {
        for (int i = 0; i < pathPoints.Count - 1; ++i)
        {
            Debug.DrawLine(pathPoints[i].ToVector3(), pathPoints[i + 1].ToVector3(), Color.green);
        }
    }

    void Update()
    {
        pathPoints = new List<Vec3>();
        calculateProjectile();
        calculatePath();
        drawPath();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulate = true;
            
            GameObject proj = Instantiate(projectile, launchPoint.transform.position, launchPoint.transform.rotation);
            proj.GetComponent<Projectile>().SetVelocity(v3InitialVelocity);
            proj.GetComponent<Projectile>().SetAcceleration(v3Acceleration);
            proj.GetComponent<Projectile>().SetLifeTime(airTime);
        }

        

    }
    
   

}
