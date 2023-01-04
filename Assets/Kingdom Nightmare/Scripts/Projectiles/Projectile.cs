using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Projectile : MonoBehaviour
{


    //inspector variables
    [SerializeField] protected float _velocity = 15.0f;
    [SerializeField] protected Rigidbody _rb = null;
    [SerializeField] protected int _poolIndex = 0;


    //privates
    private ProjectilePool _pool = null;
    protected Vector2 _projectileRange;
    protected int _damage = 20;// damage power is taken from its tower
    protected Transform _startTrans;
 
    //properties 
    public int Damage => _damage;

    protected virtual void Awake()
    {
        _pool = ProjectilePool.Instance.GetComponent<ProjectilePool>();

    }

    protected virtual void Update() 
    {
        if(_startTrans == null)
        {
     
            _pool.BackObjectToPool(this, _poolIndex);
            return;
        }
        if (Vector3.Distance(transform.position, _startTrans.position) > _projectileRange.y)
        {
            _pool.BackObjectToPool(this, _poolIndex);
        }
    }

    //abstract methods
    public abstract void Shoot(Vector2 projectileRange, int damage,Transform startTrans, Vector3 targetPos);


    // Throws ball at location with regards to gravity (assuming no obstacles in path) and initialVelocity (how hard to throw the ball)
    public Vector3 ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity ,float _distance)
    {
       // if (_startTrans == null) return Vector3.zero;
        Vector3 direction = (targetLocation - _startTrans.position).normalized;
        var distance = _distance;

        var firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        Vector3 elevation = Quaternion.AngleAxis(firingElevationAngle, _startTrans.right) * _startTrans.up;
        float directionAngle = AngleBetweenAboutAxis(_startTrans.forward, direction, _startTrans.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, _startTrans.up) * elevation * initialVelocity;
        transform.rotation = Quaternion.AngleAxis(firingElevationAngle, Vector3.forward);
        return velocity;



       // transform.rotation = Quaternion.AngleAxis(firingElevationAngle, Vector3.forward);
    }

    // Helper method to find angle between two points (v1 & v2) with respect to axis n
    public static float AngleBetweenAboutAxis(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(v1, v2)),
            Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

    // Helper method to find angle of elevation (ballistic trajectory) required to reach distance with initialVelocity
    // Does not take wind resistance into consideration.
    private float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }


    protected virtual void OnTriggerEnter(Collider collider)
    {
 
        if(collider.CompareTag("Ground"))
        {
            _pool.BackObjectToPool(this, _poolIndex);
         
        }
        else if(collider.CompareTag("Enemy"))
        {
            var enemy = GameSceneManager.Instance.GetEnemy(collider.GetInstanceID());
            if(enemy != null)
            {
                enemy._enemyHealth.TakeDamage(_damage);
               _pool.BackObjectToPool(this, _poolIndex);
  
            }
        }
    }
}
