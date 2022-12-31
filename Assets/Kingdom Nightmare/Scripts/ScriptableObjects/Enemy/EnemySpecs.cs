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
    //inspector properties
    [SerializeField] private List<enemyInfo> enemyInfos = new();
    [SerializeField] private GameObject _enemyPrefab;
    //properties
    public GameObject EnemyPrefab => _enemyPrefab;

    public enemyInfo this[int wave]
    {
        get
        {
            if (enemyInfos.Count == 0 ) return null;
            //if there is no new info return the last one
            if (enemyInfos.Count <= wave) return enemyInfos[enemyInfos.Count - 1];
            else return enemyInfos[wave];

        }
    }
}
