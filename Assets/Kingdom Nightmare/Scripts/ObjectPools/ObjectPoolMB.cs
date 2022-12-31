using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolMB<T> : MonoBehaviour where T:MonoBehaviour
{
    [Serializable]
    public class ObjectToPool
    {
        public T _prefab = null;
        public int _poolSize = 6;
    }
    [SerializeField] List<ObjectToPool> _prefabs = new();

    private Queue<T> _objectPool = new();

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
                CreateNewPool(_prefabs[i]._prefab);
            }

        }
    }

    public T EnqueFromPool()
    {
        if (_objectPool.Count == 0) CreateInitialPool();
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

    private void CreateNewPool(T prefab)
    {
        T newObject = Instantiate(prefab);
        newObject.transform.SetParent(this.transform);
        newObject.gameObject.name = prefab.name;
        newObject.gameObject.SetActive(false);
        _objectPool.Enqueue(newObject);
    }
}
