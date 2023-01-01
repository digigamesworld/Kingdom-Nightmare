using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]

public class GameSceneManager : SingletonMB<GameSceneManager>
{
    

    //private fields
    private int _currentWave;
    private int _currenLevel = 1;
    private Dictionary<int, Enemy> _enemies = new(); // a reference to all enemies by their ID's
    //properties
    public int CurrentLevel => -CurrentWave;
    public int RegisterdEnemyCount => _enemies.Count;
    public Dictionary<int, Enemy> RegisterdEnemies => _enemies;
    public int CurrentWave
    {
        get
        {
            return _currentWave;
        }
        set
        {
            _currentWave = value;
        }
    }
    public void RegisterEnemies(int keyID, Enemy enemy)
    {
        if (!_enemies.ContainsKey(keyID))
        {
            _enemies[keyID] = enemy;
        }
    }
    public Enemy GetEnemy(int keyID)
    {
        Enemy enemy = null;
        if (_enemies.TryGetValue(keyID, out enemy))
        {
            return enemy;
        }

        return null;
    }
}
