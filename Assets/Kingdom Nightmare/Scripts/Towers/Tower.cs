using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private variables
  
    private int            _currentLevel = 0;
    private float          _timer = 0.0f;
    private List<Enemy>    _listOfenemiesInRange = new();
    //inspectore variables
    [SerializeField] TowersSpecs    _towersSpecs;
    [SerializeField] int            _Index_projectilePooledItem;
    [SerializeField] float          _retargetingTime = 0.5f;
    [SerializeField] Transform      _startFireTransform;
    [SerializeField] TowerTypes     _towerType;
    [SerializeField] GameObject     _updateCanvas;
    //properties
    public TowersSpecs TowerSpecs => _towersSpecs;
    public TowerTypes TowerType => _towerType;

    private void OnEnable()
    {
        TowerUpdateHudManager.OnUpdate += OnvalidateUpdate;
    }
    private void OnDisable()
    {
        TowerUpdateHudManager.OnUpdate -= OnvalidateUpdate;
    }

    private void OnvalidateUpdate(Tower tower, int currentlevel)
    {
        if (tower == this) _currentLevel = currentlevel;
    }
    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0.0f, _retargetingTime);
    }

    private Enemy  UpdateTarget()
    {

        var registerdEnemies = GameSceneManager.Instance.RegisterdEnemis;
        _listOfenemiesInRange.Clear();
        foreach (Enemy Ene in registerdEnemies.Values)
        {
            if (!Ene.gameObject.activeInHierarchy) continue;
            var distance = Vector3.Distance(transform.position, Ene.transform.position);
            if (distance> _towersSpecs.Range(_currentLevel).x && distance < _towersSpecs.Range(_currentLevel).y)
            {
                _listOfenemiesInRange.Add(Ene);

            }
        }
        float nearestTargetDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach(Enemy Ene in _listOfenemiesInRange)
        {
            if(Vector3.Distance(transform.position, Ene.transform.position) < nearestTargetDistance)
            {
                nearestTargetDistance = Vector3.Distance(transform.position, Ene.transform.position);
                nearestEnemy = Ene;

            }
        }

        if (nearestEnemy != null && nearestTargetDistance > _towersSpecs.Range(_currentLevel).x
                                    && _towersSpecs.Range(_currentLevel).y > nearestTargetDistance)
        {
   
            return nearestEnemy;
        }
        else
            return null;
    }

    private void Update()
    {
        ShootToTarget();

    }
    private void ShootToTarget()
    {
        if (UpdateTarget() == null) return;

        _timer += Time.deltaTime;
        if (_timer > _towersSpecs.FireRate(_currentLevel))
        {
            var projectile = ProjectilePool.Instance.GetObjectFromPool(_Index_projectilePooledItem);
            projectile.transform.position = _startFireTransform.position;
            projectile.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (projectile != null) projectile.Shoot(_towersSpecs.Range(_currentLevel), _towersSpecs.Damage(_currentLevel), _startFireTransform,UpdateTarget().transform.position);
            _timer = 0;
            if (TowerType == TowerTypes.ArcherTower) AudioManager.Instance.Play("Arrow");
            else if (TowerType == TowerTypes.CanonTower) AudioManager.Instance.Play("RocketLaunch");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _towersSpecs.Range(_currentLevel).x);
    }
    private void OnMouseEnter()
    {
        HUDController.Instance.OpenTowerInventory(_towersSpecs.NormalTowerSprite, _towersSpecs.TowerSpecsDesc,
                                      _towersSpecs.FireRate(_currentLevel), _towersSpecs.Damage(_currentLevel));

    }
    private void OnMouseExit()
    {
        HUDController.Instance.CloseTowerInvetory();
    }
    private void OnMouseDown()
    {
        _updateCanvas.SetActive(true);
        AudioManager.Instance.Play("Click");
    }
}
