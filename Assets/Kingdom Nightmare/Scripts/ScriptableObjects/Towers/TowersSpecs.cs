using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower Specs", menuName = "ScriptableObjects/Tower specs")]
public class TowersSpecs : ScriptableObject
{
 //inspactore variables
    [SerializeField] UpgradeList towerUpgradeList;
    [SerializeField] string pathToIcon;

    public Sprite SpriteTower => Resources.Load<Sprite>(pathToIcon);
    public bool Purchased => true;// for future it can be set for now all towers are avaiable
    //private variables
    private int _currentLevel = 1;
    public int CurrentLevel
    {
        get
        {
            _currentLevel = GameSceneManager.Instance.CurrentLevel;
            return _currentLevel;
        }
        set
        {
            _currentLevel = value;
        }
    }

    //properties
    public int Price
    {
        get
        {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[_currentLevel] != null)
                    return towerUpgradeList[_currentLevel].price;
                else
                    return 10000000;// if there is no list to fetch price from then return a big number to make upgrade imposible
            }
            else
                return 10000000;
        }
    }

    public Vector2 Range
    {
        get
        {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[_currentLevel] != null)
                    return towerUpgradeList[_currentLevel].range;
                else
                    return Vector2.zero;// if there is no list to fetch range from, then return zero
            }
            else
                return Vector2.zero;
        }
    }
    public int Damage
    {
        get
        {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[_currentLevel] != null)
                    return towerUpgradeList[_currentLevel].damage;
                else
                    return 0;// if there is no list to fetch damage from then return 0
            }
            else
                return 0;
        }
    }
    public float FireRate
    {
        get
        {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[_currentLevel] != null)
                    return towerUpgradeList[_currentLevel].fireRate;
                else
                    return -1;// if there is no list to fetch firerate from then return -1
            }
            else
                return -1;
        }
    }
}
