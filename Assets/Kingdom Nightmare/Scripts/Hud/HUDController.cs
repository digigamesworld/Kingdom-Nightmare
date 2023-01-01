using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public  class HUDController : MonoBehaviour
{

    [SerializeField] private PlayerInventory            _playerInventory;
    [SerializeField] private GameObject                 _towersPanel;
    [SerializeField] private GameObject                 _towerButtonTemplate;

    //private vars
    private TowerBase _towerBase;

    private void Awake()
    {
        //close hud panel at start
        _towersPanel.gameObject.SetActive(false);
    }
    private void Start()
    {
        
    }
    public void OpenHud(TowerBase towerBase)
    {
        if (_towersPanel.gameObject.activeInHierarchy || towerBase == _towerBase) return;
        _towerBase = towerBase;
        var towers = TowerBuilder._instance._towers;
        if (towers == null) return;
        for (int i=0; i< towers.Count; i++)
        {
            if(towers[i].TowerSpecs.Purchased)
            {
                var index = i;
                var btn = Instantiate(_towerButtonTemplate, Vector3.zero, Quaternion.identity);
                btn.transform.SetParent(_towersPanel.transform, false);
                _towerButtonTemplate.GetComponent<Image>().sprite = towers[i].TowerSpecs.SpriteTower;
                _towerButtonTemplate.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = towers[i].TowerSpecs.Price.ToString();
                btn.GetComponent<Button>().onClick.RemoveAllListeners();
            
                btn.GetComponent<Button>().onClick.AddListener
                                                     (delegate { towerBase.SetTower(towers[index]); _towersPanel.gameObject.SetActive(false);});

        
            }
        }
        _towersPanel.gameObject.SetActive(true);
    }

    private void SelectTower(int index)
    {

    }

    public void BuyTower()
    {
        //check player stats
 
    }


}
