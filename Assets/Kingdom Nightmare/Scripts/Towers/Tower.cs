using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private variables
    private Transform      _target;
    private int            _currentLevel = 1;
    private float          _timer = 0.0f;

    //inspectore variables
    [SerializeField] TowersSpecs    _towersSpecs;
    [SerializeField] int            _Index_projectilePooledItem;
    [SerializeField] float          _retargetingTime = 0.5f;

    //properties
    public TowersSpecs TowerSpecs => _towersSpecs;
    private void Start()
    {
       // InvokeRepeating(nameof(UpdateTarget), 0.0f, _retargetingTime);
    }

    private Enemy  UpdateTarget()
    {
        float nearestTargetDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;
        var registerdEnemies = GameSceneManager.Instance.RegisterdEnemies;
        for(int i=0; i< GameSceneManager.Instance.RegisterdEnemyCount; i++)
        {
            if(Vector3.Distance(transform.position, registerdEnemies[i].transform.position) < nearestTargetDistance)
            {
                nearestTargetDistance = Vector3.Distance(transform.position, registerdEnemies[i].transform.position);
                nearestEnemy = registerdEnemies[i];
            }
        }
        if (nearestEnemy != null && _towersSpecs.Range.x > nearestTargetDistance
                                    && _towersSpecs.Range.y < nearestTargetDistance)
            return nearestEnemy;
        else
            return null;
    }

    private void Update()
    {
        ShootToTarget();

    }
    private void ShootToTarget()
    {
        if (_target == null) return;
        if (ProjectilePool.Instance._prefabs[_Index_projectilePooledItem] == null) return;
        _timer += Time.deltaTime;
        if (_timer > _towersSpecs.FireRate)
        {
            var projectile = ProjectilePool.Instance.GetObjectFromPool(_Index_projectilePooledItem);
            if (projectile != null) projectile.Shoot(_towersSpecs.Range, _towersSpecs.Damage, _target.transform.position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _towersSpecs.Range.y);
    }

}
