using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;



public class GameSceneManager : SingletonMB<GameSceneManager>
{
    public static  Action UIOnValidate;
    //inspector variables
    [SerializeField] private PlayerInventory _playerInventory;


    //private fields
    private int                    _currentWave;
    private int                    _currenLevel;
    private Dictionary<int, Enemy> _registerdEnemies = new(); // a reference to all enemies by their ID's
    private int                    _currentGold;
    private int                    _currentPlayerHealth;
    public DrawPath DrawPath => FindObjectOfType<DrawPath>();
    //properties
    public Dictionary<int, Enemy> RegisterdEnemis => _registerdEnemies;
    public int CurrentGold { get { return _currentGold; } set { _currentGold = value; } }
    public int WavesCount { get; set; }
    public int CurrentLevel => _currenLevel;
    public int CurrentPlayerHealth => _currentPlayerHealth;
    public int RegisterdEnemyCount => _registerdEnemies.Count;
    public PlayerInventory PlayerInventoryOnThisLevel => _playerInventory;
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
    public bool AllEnemyAreDead
    {     get
        {
            foreach (Enemy Ene in _registerdEnemies.Values)
            {
                if (Ene.gameObject.activeInHierarchy)
                {
                    return false;

                }
            }

            return true;
        }
    }

    private void Awake()
    {
        //fetch waves generators and add all waves count
        var _waves = FindObjectsOfType<WaveGenerator>();
        foreach (WaveGenerator wave in _waves)
        {
            WavesCount += wave.WaveCount;
        }
        _currentGold = _playerInventory.Gold;
        _currentPlayerHealth = _playerInventory.Health;


    }
    private void Start()
    {
        AudioManager.Instance.Play("Music");
        UIOnValidate?.Invoke();
    }
    private void OnEnable()
    {

        WaveGenerator.NextWave += SetWaveCount;
        Collectables.Collected += AddCollectables;
        Enemy.EnemyReachedEndNode += SetPlayerHealth;
    }
    private void OnDisable()
    {
        WaveGenerator.NextWave -= SetWaveCount;
        Collectables.Collected -= AddCollectables;
        Enemy.EnemyReachedEndNode -= SetPlayerHealth;
    }
    private void SetPlayerHealth(Enemy enemy)
    {
        _currentPlayerHealth -= enemy.Enemyspec[_currentWave]._damage;
        if (_currentPlayerHealth <= 0) HUDController.Instance.OpenEndScreen(false);
        UIOnValidate?.Invoke();
    }
    private void AddCollectables(CollectableType collectableType)
    {

        if (collectableType == CollectableType.Gold) _currentGold += (CurrentWave + 1);
        UIOnValidate?.Invoke();
    }
    //calculate current wave
    private void SetWaveCount()
    {
        _currentWave++;
        if (_currentWave >= WavesCount && AllEnemyAreDead)
            HUDController.Instance.OpenEndScreen(true);
        UIOnValidate?.Invoke();
    }
    public void RegisterEnemies(int keyID, Enemy enemy)
    {
        if (!_registerdEnemies.ContainsKey(keyID))
        {
            _registerdEnemies[keyID] = enemy;
        }
    }
    public Enemy GetEnemy(int keyID)
    {
        Enemy enemy = null;
        if (_registerdEnemies.TryGetValue(keyID, out enemy))
        {
            return enemy;
        }

        return null;
    }


}
