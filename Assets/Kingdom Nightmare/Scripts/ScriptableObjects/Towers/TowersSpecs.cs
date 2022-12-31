using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower Specs", menuName = "ScriptableObjects/Tower specs")]
public class TowersSpecs : ScriptableObject
{
 //inspactore variables
    [SerializeField] UpgradeList towerUpgradeList;

    //private variables
    private int _currentLevel = 1;
    public int CurrentLevel
    {
        get
        {
            return _currentLevel;
        }
        set
        {

        }
    }

    //properties
    public int Price
    {
        get
        {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList != null)
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
                if (towerUpgradeList != null)
                    return towerUpgradeList[_currentLevel].range;
                else
                    return Vector2.zero;// if there is no list to fetch price from then return a big number to make upgrade imposible
            }
            else
                return Vector2.zero;
        }
    }
}
