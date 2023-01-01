using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class TowerLevel
{
    public int price;
    public Vector2 range;
    public float fireRate;
    public int damage;
}
[CreateAssetMenu(fileName = "New Tower level list", menuName = "ScriptableObjects/Tower level list")]
public class UpgradeList : ScriptableObject
{
    [SerializeField] private List<TowerLevel> towersLevel = new();

    public TowerLevel this[int i]
    {
        get
        {
            // Return if towersLevel don't exist, are empty or the towersLevel index
            // specified is out of range	
            if (towersLevel == null || towersLevel.Count <= i) return null;


            // return towersLevel
            return towersLevel[i];
        }
    }

}
