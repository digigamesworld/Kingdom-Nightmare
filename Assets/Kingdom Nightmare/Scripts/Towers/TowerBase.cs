
using UnityEngine;



public class TowerBase : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private HUDController _hudController;
    [SerializeField] private Vector3 _towerOffset;
  
   

    private Tower _tower_Deployed;
    [SerializeField] private GameObject some;

    private void OnMouseDown()
    {
        Debug.Log("OPen Hud");
        _hudController.OpenHud(this);
    }
    public void SetTower(Tower tower)
    {
        if (_tower != null) return;
        _tower = Instantiate(tower, Vector3.zero, Quaternion.identity);
        // _tower.transform.SetParent(transform, false);
        _tower.transform.position = transform.position;
        _tower.transform.position += _towerOffset;
        
    }
}
