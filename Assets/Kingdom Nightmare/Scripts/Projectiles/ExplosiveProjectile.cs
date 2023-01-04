using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    [SerializeField] private float _explosionAreaRange;
    [Range(0.1f, 1.0f)]
    [Tooltip("Base Projectile damage multiples with this value and takes damage to an area")]
    [SerializeField] private float _damageRatio = 0.3f;
    private Projectile _projectile;
    private void Awake()
    {
        _projectile = GetComponent<Projectile>();
    }

    private void OnDisable()
    {
      
        var listOfEnemies = new List<Enemy>();
        var registerdEnemies = GameSceneManager.Instance.RegisterdEnemis;
        foreach (Enemy Ene in registerdEnemies.Values)
        {
            if (!Ene.gameObject.activeInHierarchy) continue;
            if (Vector3.Distance(transform.position, Ene.transform.position) < _explosionAreaRange)
            {
                listOfEnemies.Add(Ene);

            }
        }
        for (int i = 0; i < listOfEnemies.Count; i++)
        {
            int calculateDamage = (int)(_projectile.Damage * _damageRatio);
            listOfEnemies[i]._enemyHealth.TakeDamage(calculateDamage);
        }
    }
}
