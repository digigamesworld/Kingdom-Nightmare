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
        public Queue<T> _objectPoolInQueue = new();
    }
    [SerializeField]List<ObjectToPool> _PooledObjectList = new();

    

    private void Awake()
    {
        if(_PooledObjectList == null)
        {
            Debug.LogWarning("No prefab to pooling");
            this.enabled = false;
            return;
        }
        for(int i=0; i< _PooledObjectList.Count; i++)
        {
            CreateInitialPool(i);
        }
  
    }

    private void CreateInitialPool(int index)
    {
        if (index >= _PooledObjectList.Count) return;
        //creating initial pool
      
            for (int j = 0; j < _PooledObjectList[index]._poolSize; j++)
            {
                CreateNewPool(_PooledObjectList[index]._prefab, index);
            }

        
    }

    public T GetObjectFromPool(int poolIndex)
    {
   
        if (_PooledObjectList[poolIndex]._objectPoolInQueue.Count == 0) CreateInitialPool(poolIndex);
        if (_PooledObjectList[poolIndex]._objectPoolInQueue == null) return null;
         T dequeuedObject = _PooledObjectList[poolIndex]._objectPoolInQueue.Dequeue();
          dequeuedObject.gameObject.SetActive(true);
        return dequeuedObject;
    }

    public void BackObjectToPool(T _object, int poolIndex)
    {
        if (_object == null) return;
        RestoreDefaults(_object);
        _object.gameObject.SetActive(false);
        _PooledObjectList[poolIndex]._objectPoolInQueue.Enqueue(_object);
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
        _PooledObjectList[poolIndex]._objectPoolInQueue.Enqueue(newObject);
    }
}
