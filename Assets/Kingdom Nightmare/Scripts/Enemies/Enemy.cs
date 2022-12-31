using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{


    //inspector variables
    [SerializeField] EnemySpecs _enemySpecs;

    //private variables
    private Rigidbody    _rb;
    private int          _health;
    private int          _currentWave;
    //properties
    private EnemyHealth  _enemyHealth => GetComponent<EnemyHealth>();
    public EnemyHealth EnemyHp => _enemyHealth;
  
    private void Awake()
    {
        _currentWave = GameSceneManager.Instance.CurrentWave;
        _rb = GetComponent<Rigidbody>();
        _health = _enemySpecs[_currentWave]._HP;
    }
    private void Start()
    {
        _enemyHealth.InitiateHp(_health);
        GameSceneManager.Instance.RegisterEnemies(_rb.GetInstanceID(), this);
    }
}
