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

    void FixedUpdate()
    {
        if (gridMakerScript.GameStarted && Time.time >= _shootingTime) 
        {
            LaunchMissile();

            // determine the next point in time to shoot missile
            _shootingTime = Time.time + _shootingPeriod;
        }
    }

    // Following function calculates the initial velocity required
    // for a trajectory motion. Inputs: Angle, position and target.
    // Given the inputs, script calculates the initial velocity and
    // adds the velocity to the rigidbody. Assumes Y_source = Y_target
    void LaunchMissile()
    {
        // place the cannon into a random space in determined area
        Vector3 _cannonPosition = new Vector3(Random.Range(_cannon.XMin, _cannon.XMax), _cannon.Y, Random.Range(_cannon.ZMin, _cannon.ZMax));
        transform.position = _cannonPosition;

        // Determine a random target in the game field
        Vector3 _targetPosition = new Vector3(Random.Range(_target.XMin, _target.XMax), _target.Y, Random.Range(_target.ZMin, _target.ZMax));
        _target.BullsEyeTf.position = _targetPosition + new Vector3(0f, .1f, 0f);   // place bullzeye to the acquiared position

        // Calculate the distance between the cannon and the target
        float dist = Vector3.Distance(_targetPosition, _cannonPosition);

        // Instantiate a projectile (missile) 
        float _firingAngle = Random.Range(_projectilePhysics.FiringAngleMin, _projectilePhysics.FiringAngleMax);
        GameObject missile = Instantiate(_projectile, transform.position, Quaternion.identity) as GameObject;
        //missile.transform.parent = _missileContainer;   // stack missiles under launcher object

        // calculate initival velocity required to land the missile on target
        float Vi = Mathf.Sqrt(dist * Physics.gravity.magnitude / (Mathf.Sin(Mathf.Deg2Rad * _firingAngle * 2)));

        // Calculate the local velocity vector: Y=Up | Z=Forward, thus: Vi x Sin(angl) = Vy  &  Vi x Cos(angl) = Vz
        Vector3 LocalVelocity = new Vector3(0f, Vi * Mathf.Sin(Mathf.Deg2Rad * _firingAngle), Vi * Mathf.Cos(Mathf.Deg2Rad * _firingAngle));

        // transform this local velocity vector to global space
        missile.transform.LookAt(_targetPosition);  
        Vector3 Local2GlobalVelocity = missile.transform.TransformVector(LocalVelocity)*
                                       (1f/missile.transform.localScale.x);
        // finally, set the initial velocity of the missile - LAUNCH THE BABY
        missile.GetComponent<Rigidbody>().velocity = Local2GlobalVelocity;
    }
}

[Serializable]
public class ProjectilePhysics
{
    // a class for grouping the related variables under one dropdown in the editor
    [SerializeField] private float _firingAngleMin;
    [SerializeField] private float _firingAngleMax;

    public float FiringAngleMax
    {
        get { return _firingAngleMax; }
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
    //      |          <---5--->|   _cannonPlaneTf.position.x + 5*_cannonPlaneTf.localScale.x;  : XMax
    //      |         x         |   _cannonPlaneTf.position.x                                   : XMid
    //      |<---5--->          |   _cannonPlaneTf.position.x - 5*_cannonPlaneTf.localScale.x;  : XMin
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
