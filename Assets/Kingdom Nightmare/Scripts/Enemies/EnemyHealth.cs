using UnityEngine;
using System;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    public static event Action<Enemy> EnemyDied;
    //privates
    private int _hp = 100;
    private Enemy _enemy;
    public void InitiateHp(int hp)
    {
        _hp = hp;
    }
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    public void TakeDamage(int damageAmount)
    {
        _hp -= damageAmount;
        if(_hp <= 0)
        {
            EnemyDied?.Invoke(_enemy);
        }
    }
}
