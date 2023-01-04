using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerTypes { ArcherTower, CanonTower}
public class TowerBuilder : MonoBehaviour
{
    public static TowerBuilder instance;

    //inspactor
    //reference to all towers
    [SerializeField]private  List<Tower> _towers = new();
    public List<Tower> Towers => _towers;
    public void Awake ()
    {
      
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
    }
    public TowersSpecs GetTowerSpec(TowerTypes towerTypes)
    {
        for (int i = 0; i < _towers.Count; i++)
        {
            if (_towers[i].TowerType == towerTypes)
            {
                return _towers[i].TowerSpecs;

            }
        }
        return null;

    }
    public Tower BuildTower(TowerTypes towerType, Vector3 position)
    {
        if (_towers.Count == 0) return null;
        for(int i=0; i<_towers.Count;i++)
        {
            if(_towers[i].TowerType == towerType)
            {
                return Instantiate(_towers[i], position, Quaternion.identity);

            }
        }
        return null;
    }

}
