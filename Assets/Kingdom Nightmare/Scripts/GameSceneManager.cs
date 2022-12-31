using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : SingletonMB<GameSceneManager>
{
    private int _currentWave;
    private Dictionary<int, Enemy> _enemies = new(); // a reference to all enemies by their ID's
    public int CurrentWave
    {
        get
        {
            return _currentWave;
        }
        set
        {

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
