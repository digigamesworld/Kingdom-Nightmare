using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveGenerator : MonoBehaviour
{

    public static event Action NextWave;
    //private variable
    private float _timer;
    private int   _currentWave;
    //inspector variabbles
    [SerializeField] private Transform        _spawnPos;
    [SerializeField] private List<float>      _steps = new();
    [SerializeField] private float            _delayTime = 5.0f;
    [SerializeField] private EnemyPool        _enemyPool = null;
    [SerializeField] private int              _poolIndex = 0;
    [SerializeField] private int              _waveCount = 10;
    [Tooltip("Start Generating Wave at current wave(all waves from all other generators)")]
    [SerializeField] private int              _startAtWave = 0;
    //properties
    public int WaveCount => _waveCount;
    private bool AllEnemyAreDead
    {
        get
        {
            var registerdEnemies = GameSceneManager.Instance.AllEnemies();
            foreach (Enemy Ene in registerdEnemies.Values)
            {
                if (Ene.gameObject.activeInHierarchy)
                {
                    return false;
                    
                }
            }

            return true;
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(InvokeWave),0,_delayTime);
    }
    private void InvokeWave()
    {
      //work to do
        if(GameSceneManager.Instance.WavesCount == _startAtWave)
        {
            Invoke(nameof(StartSendingWave), 0.0f);
            CancelInvoke(nameof(InvokeWave));
        }

    }
    private void StartSendingWave()
    {
        _currentWave++;

        var index = 0;
        SpawnEnemy();
        StartCoroutine(SpawnInSteps());
        IEnumerator SpawnInSteps()
        {
            yield return new WaitForSeconds(_steps[index]);
            SpawnEnemy();
       
            if(_steps.Count-1 > index)
            {
                index++;
                StartCoroutine(SpawnInSteps());
            }
            else
            {
                while(!AllEnemyAreDead)
                {
                    yield return null;

                }
                if(_currentWave < _waveCount)
                {
                    NextWave?.Invoke();
                    Invoke(nameof(StartSendingWave), _delayTime);
                }
            }
   
        }
    }

    private void SpawnEnemy()
    {
        if (_spawnPos == null) return;
        var enemy = _enemyPool.GetObjectFromPool(_poolIndex);
        enemy.transform.position = _spawnPos.position;
        enemy.Initiate(_poolIndex);
    }
}
