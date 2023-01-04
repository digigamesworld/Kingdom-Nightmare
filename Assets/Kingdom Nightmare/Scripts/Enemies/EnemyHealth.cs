using UnityEngine;
using System;
using UnityEngine.UI;
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    //public static event Action<Enemy> EnemyDied;
    //privates
    private int _hp = 100;
    private Enemy _enemy;
    //inspector
    [Tooltip("which item this enemy should drop on death")]
    [SerializeField] int _collectablePoolIndexItem;
    [SerializeField] int _particlePoolIndexItem;
    [SerializeField] Slider _slider;
    private int _maxHealth;
    public void InitiateHp(int hp)
    {
        _hp = _maxHealth =hp;
        _slider.value = 1.0f;
    }
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
  
    public void TakeDamage(int damageAmount)
    {
        _hp -= damageAmount;
        _slider.value = (float)_hp / _maxHealth;
        if(_hp <= 0)
        {
            //EnemyDied?.Invoke(_enemy);
            EnemyPool.Instance.BackObjectToPool(_enemy, _enemy.PooledIndex);
            var pooleditem = CollectablePools.Instance.GetObjectFromPool(_collectablePoolIndexItem);
            pooleditem.transform.position = transform.position + Vector3.up;
            var particlePooleditem = ParticlePool.Instance.GetObjectFromPool(_particlePoolIndexItem);
            particlePooleditem.transform.position = transform.position + Vector3.up;
        }
    }
}
