using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryProjectile : Projectile
{
   
  
    public override void Shoot(Vector2 projectileRange, int damage, Vector3 targetPos)
    {
        _projectileRange = projectileRange;
        _damage = damage;
        _targetPos = targetPos;
        var distance = Vector3.Distance(_targetPos, transform.position);
        //set the initial velocity based on distance to target
        float initVelocity = Mathf.Lerp(_velicity*0.3f, _velicity, distance / 20f);
        var velocity = ThrowBallAtTargetLocation(_targetPos, initVelocity, distance);
        _rb.AddForce(velocity, ForceMode.Impulse);
    }


}
