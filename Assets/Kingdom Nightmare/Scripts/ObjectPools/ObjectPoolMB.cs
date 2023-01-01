using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolMB<T> : SingletonMB<ObjectPoolMB<T>> where T:MonoBehaviour
{
    [Serializable]
    public class ObjectToPool
    {
        public T _prefab = null;
        public int _poolSize = 6;
        public Queue<T> _objectPool = new();
    }
    public List<ObjectToPool> _prefabs = new();

    

    private void Awake()
    {
        if(_prefabs == null)
        {
            Debug.LogWarning("No prefab to pooling");
            this.enabled = false;
            return;
        }

        CreateInitialPool();
    }

    private void CreateInitialPool()
    {
        //creating initial pool
        for (int i = 0; i < _prefabs.Count; i++)
        {
            for (int j = 0; j < _prefabs[i]._poolSize; j++)
            {
                CreateNewPool(_prefabs[j]._prefab, i);
            }

        }
    }

    public T GetObjectFromPool(int poolIndex)
    {
        if (_prefabs[poolIndex]._objectPool.Count == 0) CreateInitialPool();
        if (_prefabs[poolIndex]._objectPool == null) return null;
         T dequeuedObject = _prefabs[poolIndex]._objectPool.Dequeue();
        return dequeuedObject;
    }

    public void BackObjectToPool(T _object, int poolIndex)
    {
        if (_object == null) return;
        RestoreDefaults(_object);
        _object.gameObject.SetActive(false);
        _prefabs[poolIndex]._objectPool.Enqueue(_object);
    }

    protected virtual void RestoreDefaults(T _object)
    {
     
    }

    private void CreateNewPool(T prefab, int poolIndex)
    {
        T newObject = Instantiate(prefab);
        newObject.transform.SetParent(this.transform);
        newObject.gameObject.name = prefab.name;
        newObject.gameObject.SetActive(false);
        _prefabs[poolIndex]._objectPool.Enqueue(newObject);
    }
}
