using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    //privates
    private int _hp = 100;

  
    public void InitiateHp(int hp)
    {
        _hp = hp;
    }

    public void TakeDamage(int damageAmount)
    {
        _hp -= damageAmount;
        if(_hp <= 0)
        {
            //do some
        }
    }
}
