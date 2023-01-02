using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularProjectile : Projectile
{
    private Vector3 _direction;
    private Vector3 _target;
    public override void Shoot(Vector2 projectileRange, int damage, Transform startTrans, Vector3 targetPos)
    {
        _projectileRange = projectileRange;
        _damage = damage;
        _startTrans = startTrans ;
        _target = targetPos;
        _direction = targetPos - _startTrans.position;
        transform.rotation = Quaternion.LookRotation(_direction);
    }
    protected  override void Update()
    {
        base.Update();
        transform.position += _direction.normalized* _velocity * Time.deltaTime ;//adding force to projectile
    }




}
