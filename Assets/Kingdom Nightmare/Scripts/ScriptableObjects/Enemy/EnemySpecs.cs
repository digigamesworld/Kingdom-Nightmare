using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class enemyInfo
{
    public int _HP;
    public int _Speed;
}
/// <summary>
/// //we can create a list of enemyinfos which can be used by wave numbers.enemies can have different
/// specs in each wave
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Specs", menuName = "ScriptableObjects/New Enemy Specs")]
public class EnemySpecs : ScriptableObject
{
    //private vars
    private GameObject _enemyPrefab;

    //inspector properties
    [Tooltip("each element is used for diffent wave number, element zero for first wave ...")]
    [SerializeField] private List<enemyInfo> _enemyInfos = new();
    [SerializeField] private string          _enemyPrefabPath;
    //properties
    //public GameObject EnemyPrefab => _enemyPrefab = Resources.Load<GameObject>(_enemyPrefabPath);
    
    public enemyInfo this[int wave]
    {
        get
        {
            if (_enemyInfos.Count == 0 ) return null;
            //if there is no new info return the last one
            if (_enemyInfos.Count <= wave) return _enemyInfos[_enemyInfos.Count - 1];
            else return _enemyInfos[wave];

        }
    }
}
