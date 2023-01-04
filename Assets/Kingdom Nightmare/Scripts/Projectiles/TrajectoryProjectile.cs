using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryProjectile : Projectile
{

    [SerializeField]private int _particlePoolIndexItem;

    public override void Shoot(Vector2 projectileRange, int damage,Transform startTrans ,Vector3 targetPos)
    {
      
        _projectileRange = projectileRange;
        _damage = damage;
        _startTrans = startTrans;
        var distance = Vector3.Distance(targetPos, startTrans.position);
        //set the initial velocity based on distance to target
        //float initVelocity = Mathf.Lerp(_velocity*0.3f, _velocity, distance / 20f);
        var velocity = ThrowBallAtTargetLocation(targetPos, _velocity, distance);
        Debug.Log(velocity.magnitude);
        if(velocity.magnitude > 0)
        _rb.AddForce(velocity, ForceMode.Impulse);
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if(collider.CompareTag("Enemy") || collider.CompareTag("Ground"))
            AudioManager.Instance.Play("Explosion");
        var particlePooleditem = ParticlePool.Instance.GetObjectFromPool(_particlePoolIndexItem);
        particlePooleditem.transform.position = transform.position + Vector3.up;
    }
}
