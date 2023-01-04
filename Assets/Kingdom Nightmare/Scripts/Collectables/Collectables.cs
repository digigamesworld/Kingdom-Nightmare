using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CollectableType { Gold, Heart}
public abstract class Collectables : MonoBehaviour
{
    [SerializeField] CollectableType _collectableType;
    [SerializeField] int _poolIndex;
    public static event Action<CollectableType> Collected;
    protected virtual void OnMouseEnter()
    {
        CollectablePools.Instance.BackObjectToPool(this, _poolIndex);
        switch (_collectableType)
        {
            case (CollectableType.Gold):
                AudioManager.Instance.Play("CollectCoin");
                break;
                
        }
        Collected?.Invoke(_collectableType);
    }
}
