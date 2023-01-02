using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    //inspector variables
    [SerializeField]private EnemySpecs _enemySpecs;
    [SerializeField]private Collider   _collider;
    [SerializeField] private float _nextNodeRange = 0.5f;
    //private variables
    private Rigidbody          _rb;
    private int                _health;
 
    private GameSceneManager   _gameManager;
    private int                _pathNodeIndex = 0;
    private Transform          _target;
    private int                _poolIndexNUmebr;
    //properties
    public EnemyHealth  _enemyHealth => GetComponent<EnemyHealth>();
    public EnemyHealth EnemyHp => _enemyHealth;
  
    private List<Transform> _pathNodes = new List<Transform>();

    public static event Action EnemyReachedEndNode;

    //properties

    private void Awake()
    {
        _gameManager = GameSceneManager.Instance;
        if(_gameManager != null)
        _gameManager.RegisterEnemies(_collider.GetInstanceID(), this);
        _rb = GetComponent<Rigidbody>();
        _pathNodes = _gameManager.DrawPath.PathNodes;

    }
    private void OnEnable()
    {
        if (_gameManager != null)
        _health = _enemySpecs[_gameManager.CurrentWave]._HP;
        _enemyHealth.InitiateHp(_health);
    }

    public void Initiate(int PoolIndex)
    {

        _pathNodeIndex = 0;
        _target = _pathNodes[_pathNodeIndex];
        _poolIndexNUmebr = PoolIndex;
    }


    private void Update()
    {
        if (_pathNodeIndex >= _pathNodes.Count)
        {
         
            EnemyPool.Instance.BackObjectToPool(this, _poolIndexNUmebr);
            EnemyReachedEndNode?.Invoke();
            return;

        }
       
        _target = _pathNodes[_pathNodeIndex];

        var dir = _target.position - transform.position;
        transform.position += dir.normalized * _enemySpecs[GameSceneManager.Instance.CurrentWave]._Speed * Time.deltaTime*6;
      
        Quaternion targetrotation = Quaternion.LookRotation(dir.normalized);

        transform.rotation = Quaternion.Slerp(transform.rotation,targetrotation,Time.deltaTime * 3.0f);

        if (Vector3.Distance(transform.position, _target.position) < _nextNodeRange) _pathNodeIndex++;

    }
}
