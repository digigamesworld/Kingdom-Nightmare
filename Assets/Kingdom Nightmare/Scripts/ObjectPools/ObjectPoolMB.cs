using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolMB<T> : MonoBehaviour where T:MonoBehaviour
{
    [SerializeField] T   _prefab   = null;
    [SerializeField] int _poolSize = 6;

    private Queue<T> _objectPool = new();

    private void Awake()
    {
        if(_prefab== null)
        {
            Debug.LogWarning("No prefab to pooling");
            this.enabled = false;
            return;
        }

        //creating initial pool
        for(int i=0; i< _poolSize; i++)
        {
            CreateNewPool();
        }
    }

    public T EnqueFromPool()
    {
        if (_objectPool.Count == 0) CreateNewPool();
        T dequeuedObject = _objectPool.Dequeue();
        return dequeuedObject;
    }

    public void DequeueObject(T _object)
    {
        RestoreDefaults(_object);
        _object.gameObject.SetActive(false);
        _objectPool.Enqueue(_object);
    }

    protected virtual void RestoreDefaults(T _object)
    {
     
    }

    private void CreateNewPool()
    {
        T newObject = Instantiate(_prefab);
        newObject.transform.SetParent(this.transform);
        newObject.gameObject.name = _prefab.name;
        newObject.gameObject.SetActive(false);
        _objectPool.Enqueue(newObject);
    }
}
