using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private variables
  
    private int            _currentLevel = 0;
    private float          _timer = 0.0f;

    //inspectore variables
    [SerializeField] TowersSpecs    _towersSpecs;
    [SerializeField] int            _Index_projectilePooledItem;
    [SerializeField] float          _retargetingTime = 0.5f;
    [SerializeField] Transform      _startFireTransform;
    //properties
    public TowersSpecs TowerSpecs => _towersSpecs;
    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0.0f, _retargetingTime);
    }

    private Enemy  UpdateTarget()
    {
       
        float nearestTargetDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;
        var registerdEnemies = GameSceneManager.Instance.AllEnemies();
        foreach(Enemy Ene in registerdEnemies.Values)
        {
            if (!Ene.gameObject.activeInHierarchy) continue;
            if(Vector3.Distance(transform.position, Ene.transform.position) < nearestTargetDistance)
            {
                nearestTargetDistance = Vector3.Distance(transform.position, Ene.transform.position);
                nearestEnemy = Ene;

            }
        }

        if (nearestEnemy != null && nearestTargetDistance > _towersSpecs.Range.x
                                    && _towersSpecs.Range.y > nearestTargetDistance)
        {
   
            return nearestEnemy;
        }
        else
            return null;
    }

    private void Update()
    {
        ShootToTarget();

    }
    private void ShootToTarget()
    {
        if (UpdateTarget() == null) return;
  
        _timer += Time.deltaTime;
        if (_timer > _towersSpecs.FireRate)
        {
            var projectile = ProjectilePool.Instance.GetObjectFromPool(_Index_projectilePooledItem);
            projectile.transform.position = _startFireTransform.position;
            if (projectile != null) projectile.Shoot(_towersSpecs.Range, _towersSpecs.Damage, _startFireTransform,UpdateTarget().transform.position);
            _timer = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _towersSpecs.Range.y);
    }

}
