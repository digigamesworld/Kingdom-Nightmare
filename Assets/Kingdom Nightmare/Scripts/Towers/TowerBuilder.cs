using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public static TowerBuilder _instance;
    //inspactor
    //reference to all towers
    public List<Tower> _towers = new();
    public void Start ()
    {
      
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
    }


}
