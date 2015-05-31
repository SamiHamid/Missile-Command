using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyMissileLauncher : MonoBehaviour {
 
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _missileContainer;
    [SerializeField] private float _shootingPeriod;

    [SerializeField] private ProjectilePhysics _projectilePhysics;
    [SerializeField] private CannonPlacement _cannon;
    [SerializeField] private TargetPlacement _target;

    private float _shootingTime;

    void Start()
    {
		
    }
 

    void FixedUpdate()
    {
        if (gridMakerScript.GameStarted && Time.time >= _shootingTime) 
        {
            //StartCoroutine(SimulateProjectile());
            LaunchMissile();

            _shootingTime = Time.time + _shootingPeriod;
            
        }
    }

    // Following function calculates the initial velocity required
    // for a trajectory motion. Inputs: Angle, position and target.
    // Given the inputs, script calculates the initial velocity
    // and adds the velocity to the rigidbody
    void LaunchMissile()
    {
        // place the cannon into a random space in determined area
        Vector3 _cannonPosition = new Vector3(Random.Range(_cannon.XMin, _cannon.XMax), _cannon.Y, Random.Range(_cannon.ZMin, _cannon.ZMax));
        transform.position = _cannonPosition;

        // Determine a random target in the game field
        Vector3 _targetPosition = new Vector3(Random.Range(_target.XMin, _target.XMax), _target.Y, Random.Range(_target.ZMin, _target.ZMax));
        _target.BullsEyeTf.position = _targetPosition + new Vector3(0f, .1f, 0f);

        // Calculate the distance between the cannon and the target
        float dist = Vector3.Distance(_targetPosition, _cannonPosition);

        // find the plane angle
        float planeAngle;
        Vector3 from = _targetPosition - _cannonPosition;
        Vector3 to = transform.right;

        // angle returns the absolute value only [0-180]
        planeAngle = Vector3.Angle(from, to);

        // use cross product to find real value [-180 - +180]
        Vector3 cross = Vector3.Cross(from, to);
        if (cross.y < 0) planeAngle = -planeAngle;

        // Instantiate a projectile (missile) TODO: FIX ROTATION
        float _firingAngle = Random.Range(_projectilePhysics.FiringAngleMin, _projectilePhysics.FiringAngleMax);
        GameObject missile = Instantiate(_projectile, transform.position, Quaternion.Euler(new Vector3(-_firingAngle, 0f, planeAngle))) as GameObject;
        missile.transform.parent = _missileContainer;   // stack missiles under launcher object

       

        // calculate initival velocity required to land the missile on target
        float Vi;
        Vi = Mathf.Sqrt(dist * 9.8f / (Mathf.Sin(Mathf.Deg2Rad * _firingAngle * 2)));
        float Vx, Vy, Vz;   // x,y,z components of the initial velocity

        Vx = Vi * Mathf.Cos(Mathf.Deg2Rad * _firingAngle) * Mathf.Cos(planeAngle * Mathf.Deg2Rad);
        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _firingAngle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _firingAngle) * Mathf.Sin(planeAngle * Mathf.Deg2Rad);

        missile.GetComponent<Rigidbody>().velocity = new Vector3(Vx, Vy, Vz);
    }

    // TODO: remove this segment if the physics based solution above works
    IEnumerator SimulateProjectile()
    {
        // place the cannon into a random space in determined area
        Vector3 _cannonPosition = new Vector3(Random.Range(_cannon.XMin, _cannon.XMax), _cannon.Y, Random.Range(_cannon.ZMin, _cannon.ZMax));
        transform.position = _cannonPosition;

        // Determine a random target in the game field
        Vector3 _targetPosition = new Vector3(Random.Range(_target.XMin, _target.XMax), _target.Y, Random.Range(_target.ZMin, _target.ZMax));
        _target.BullsEyeTf.position = _targetPosition + new Vector3(0f, .1f, 0f);

        // Instantiate a projectile (missile)
        GameObject missile = Instantiate(_projectile, transform.position, Quaternion.identity) as GameObject;
        missile.transform.parent = _missileContainer;   // stack missiles under launcher object
        float _firingAngle = Random.Range(_projectilePhysics.FiringAngleMin, _projectilePhysics.FiringAngleMax);

        //Debug.Log("Angle: " + _firingAngle);


        // Calculate distance to target
        float target_Distance = Vector3.Distance(missile.transform.position, _targetPosition);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * _firingAngle * Mathf.Deg2Rad) / _projectilePhysics.Gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        missile.transform.rotation = Quaternion.LookRotation(_targetPosition - missile.transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            if (missile != null)
            {
                missile.transform.Translate(0, (Vy - (_projectilePhysics.Gravity*elapse_time))*Time.deltaTime,
                    Vx*Time.deltaTime);

            }
            elapse_time += Time.deltaTime;

            yield return null;
        }
    }  

}

[Serializable]
public class ProjectilePhysics
{
    // a class for grouping the related variables under one dropdown in the editor
    [SerializeField] private float _firingAngleMin;
    [SerializeField] private float _firingAngleMax;
    [SerializeField] private float _gravity;

    public float FiringAngleMax
    {
        get { return _firingAngleMax; }
    }

    public float Gravity
    {
        get { return _gravity; }
    }

    public float FiringAngleMin
    {
        get { return _firingAngleMin; }
    }
}

[Serializable]
public class CannonPlacement
{
    // drag and drop a plane, so that the coordinates (XMin/Max, ZMin/Max)
    // will be returned back as a point on that plane
    [SerializeField] private Transform _cannonPlaneTf;

    // if the scale of an axis of a plane is 1, then the plane's length of the corresponding
    // axis is 10. To get the half of it from its middle point (plane.position.x)
    // we need to multiply its scale of that dimension by 5 to get the edge of that axis
    // hence 5 * plane.localScale.x

    //           PLANE : Scale=1
    //      =====================
    //      |                   |
    //      |          <---5--->|   _cannonPlaneTf.position.x + 5*_cannonPlaneTf.localScale.x;
    //      |         x         |   _cannonPlaneTf.position.x
    //      |<---5--->          |   _cannonPlaneTf.position.x - 5*_cannonPlaneTf.localScale.x;
    //      |                   |
    //      =====================
    //      <-------- 10 ------->
    public float XMin
    {
        get
        {   
            return _cannonPlaneTf.position.x - 5*_cannonPlaneTf.localScale.x;
        }
    }

    public float XMax
    {
        get
        {
            return _cannonPlaneTf.position.x + 5 * _cannonPlaneTf.localScale.x;
        }
    }

    public float ZMin
    {
        get
        {
            return _cannonPlaneTf.position.z - 5 * _cannonPlaneTf.localScale.z;
        }
    }

    public float ZMax
    {
        get
        {
            return _cannonPlaneTf.position.z + 5 * _cannonPlaneTf.localScale.z;
        }
    }

    public float Y
    {
        get
        {
            return _cannonPlaneTf.position.y;
        }
    }
}

[Serializable]
public class TargetPlacement
{
    // see the comments on CannonPlacement class for explanation
    [SerializeField] private Transform _gamePlaneTf;

    [SerializeField] private Transform _bullsEyeTf;

    public float XMin
    {
        get
        {
            return _gamePlaneTf.position.x - 5f * _gamePlaneTf.localScale.x;
        }
    }

    public float XMax
    {
        get
        {
            return _gamePlaneTf.position.x + 5f * _gamePlaneTf.localScale.x;
        }
    }

    public float ZMin
    {
        get
        {
            return _gamePlaneTf.position.z - 5f * _gamePlaneTf.localScale.z;
        }
    }

    public float ZMax
    {
        get
        {
            return _gamePlaneTf.position.z + 5 * _gamePlaneTf.localScale.z;
        }
    }

    public float Y
    {
        get
        {
            return _gamePlaneTf.position.y;
        }
    }


    public Transform BullsEyeTf
    {
        get { return _bullsEyeTf; }
    }
}
