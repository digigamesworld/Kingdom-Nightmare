using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ParticlePoolObject : MonoBehaviour
{
    [SerializeField] int _poolIndex;
    [SerializeField] float _timeTobackToPool;

    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _timeTobackToPool)
        {
            _timer = 0;
            ParticlePool.Instance.BackObjectToPool(this, _poolIndex);
        }
    }
    

}
