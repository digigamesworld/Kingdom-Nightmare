using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Inventory", menuName = "ScriptableObjects/player Inventory")]
public class PlayerInventory : ScriptableObject
{
    [Tooltip("list of health on start of  levels")]
    [SerializeField] private List<int> _healthLst = new();
    [Tooltip("list of _gold on start of  levels")]
    [SerializeField] private List<int> _goldLst   = new();

    //private fields
    private int _currentLevel;
    //properties
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
    public int _health
    {
        get
        {

            if (_healthLst != null)
            {
                if (_healthLst.Count > _currentLevel)
                    return _healthLst[_currentLevel];
                else
                    return 100;// if there is no list to fetch health from then set it to 100 for default
            }
            else
                return 100;
        }
    }
    public int Gold
    {
        get
        {

            if (_goldLst != null)
            {
                if (_goldLst.Count > _currentLevel)
                    return _goldLst[_currentLevel];
                else
                    return 0;// if there is no list to fetch gold from then return -1
            }
            else
                return 0;
        }
    }


}
