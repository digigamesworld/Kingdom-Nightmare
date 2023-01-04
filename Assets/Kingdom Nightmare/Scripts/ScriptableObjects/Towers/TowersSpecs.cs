using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tower Specs", menuName = "ScriptableObjects/Tower specs")]
public class TowersSpecs : ScriptableObject
{
 //inspactore variables
    [SerializeField] UpgradeList towerUpgradeList;
    [SerializeField] string pathToNormalTowerSprite;
    [SerializeField] string pathToHighlightedTowerSprite;
    [SerializeField] [TextArea] string _towerSpectsDesc;
    public Sprite NormalTowerSprite => Resources.Load<Sprite>(pathToNormalTowerSprite);
    public Sprite HightlightedTowerSprite => Resources.Load<Sprite>(pathToHighlightedTowerSprite);
    public bool Purchased => true;// for future it can be set, for now all towers are avaiable


    //properties
    public string TowerSpecsDesc => _towerSpectsDesc;
    public int Price(int currentLevel)
    {

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[currentLevel] != null)
                    return towerUpgradeList[currentLevel].price;
                else
                    return 10000000;// if there is no list to fetch price from then return a big number to make upgrade imposible
            }
            else
                return 10000000;
        
    }

    public Vector2 Range(int currentLevel)
    {
       
            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[currentLevel] != null)
                    return towerUpgradeList[currentLevel].range;
                else
                    return Vector2.zero;// if there is no list to fetch range from, then return zero
            }
            else
                return Vector2.zero;
        
    }
    public int Damage(int currentLevel)
    {
       

            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[currentLevel] != null)
                    return towerUpgradeList[currentLevel].damage;
                else
                    return 0;// if there is no list to fetch damage from then return 0
            }
            else
                return 0;
        
    }
    public float FireRate(int currentLevel)
    {
        
            if (towerUpgradeList != null)
            {
                if (towerUpgradeList[currentLevel] != null)
                    return towerUpgradeList[currentLevel].fireRate;
                else
                    return -1;// if there is no list to fetch firerate from then return -1
            }
            else
                return -1;
       
    }
}
