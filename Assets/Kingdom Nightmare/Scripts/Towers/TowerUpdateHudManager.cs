using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class TowerUpdateHudManager : MonoBehaviour
{
    //inspector properties
     [SerializeField] private Tower       _tower;
     [SerializeField] private CanvasGroup _canvasGroup;
     [SerializeField] private GameObject   _updateCanvas;
  //private fields
     private int _towerLevel;
    private GameSceneManager manager;
    public static event Action<Tower, int> OnUpdate;//int stands for _towerLevel
    private void Awake()
    {
        manager = GameSceneManager.Instance;
        _updateCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        GameSceneManager.UIOnValidate += OnValidateUI;
    }
    private void OnDisable()
    {
        GameSceneManager.UIOnValidate -= OnValidateUI;
    }
 
    private void OnValidateUI()
    {
        if (manager.CurrentGold >= _tower.TowerSpecs.Price(_towerLevel))
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1.0f;
        }
        else
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.7f;
        }

    }


    public void UpdateTower()
    {
        AudioManager.Instance.Play("Click");
        manager.CurrentGold -= _tower.TowerSpecs.Price(_towerLevel);
        _towerLevel++;
        OnUpdate?.Invoke(_tower, _towerLevel);
        GameSceneManager.UIOnValidate?.Invoke();
        ResetInvok();
    }
    public void RecycleTower()
    {
        AudioManager.Instance.Play("Click");
        manager.CurrentGold += _tower.TowerSpecs.Price(_towerLevel);
        GameSceneManager.UIOnValidate?.Invoke();
        Destroy(_tower.gameObject);
    }
    private void OnMouseDown()
    {
        _updateCanvas.SetActive(true);
        AudioManager.Instance.Play("Click");
        Invoke(nameof(DeactiveSelf) , 3);
    }
    private void ResetInvok()
    {
        CancelInvoke(nameof(DeactiveSelf));
        Invoke(nameof(DeactiveSelf), 3);
    }
    private void DeactiveSelf()
    {
        _updateCanvas.SetActive(false);
    }
}
