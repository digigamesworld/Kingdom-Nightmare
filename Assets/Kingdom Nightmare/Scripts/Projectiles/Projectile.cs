using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Projectile : MonoBehaviour
{

    public event Action ProjectileDestroyed;

    //inspector variables
    [SerializeField] protected float            _velicity        = 15.0f;
    [SerializeField] protected Rigidbody        _rb              = null;
    [SerializeField] protected ProjectilePool   _pool            = null;
    [SerializeField] protected int              _poolIndex       = 0;
  

    //privates
    protected Vector3    _startPos;
    protected Vector2    _projectileRange;
    protected int        _damage = 20;// damage power is taken from its tower
    protected Vector3    _targetPos;
    protected virtual void OnEnable()
    {
        _startPos = transform.position;
    }

    //abstract methods
    public abstract void Shoot(Vector2 projectileRange, int damage, Vector3 targetPos);



    // Throws ball at location with regards to gravity (assuming no obstacles in path) and initialVelocity (how hard to throw the ball)
    public Vector3 ThrowBallAtTargetLocation(Vector3 targetLocation, float initialVelocity, float distance)
    {
        Vector3 direction = (targetLocation - transform.position).normalized;
        distance = Vector3.Distance(targetLocation, transform.position);

       var  _firingElevationAngle = FiringElevationAngle(Physics.gravity.magnitude, distance, initialVelocity);
        Vector3 elevation = Quaternion.AngleAxis(_firingElevationAngle, transform.right) * transform.up;
        float directionAngle = AngleBetweenAboutAxis(transform.forward, direction, transform.up);
        Vector3 velocity = Quaternion.AngleAxis(directionAngle, transform.up) * elevation * initialVelocity;
        return velocity;



        // transform.rotation = Quaternion.AngleAxis(_firingElevationAngle,Vector3.forward);
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
    public static float FiringElevationAngle(float gravity, float distance, float initialVelocity)
    {
        float angle = 0.5f * Mathf.Asin((gravity * distance) / (initialVelocity * initialVelocity)) * Mathf.Rad2Deg;
        return angle;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(Vector3.Distance(_startPos, transform.position) > _projectileRange.y)
        {
            _pool.BackObjectToPool(this, _poolIndex);
        }
        if(collision.collider.tag == "Ground")
        {
            _pool.BackObjectToPool(this, _poolIndex);
            ProjectileDestroyed?.Invoke();
        }
        else if(collision.collider.tag == "Enemy")
        {
            var enemy = GameSceneManager.Instance.GetEnemy(collision.collider.GetInstanceID());
            if(enemy != null)
            {
                enemy._enemyHealth.TakeDamage(_damage);
               _pool.BackObjectToPool(this, _poolIndex);
                ProjectileDestroyed?.Invoke();
            }
        }
    }
}
