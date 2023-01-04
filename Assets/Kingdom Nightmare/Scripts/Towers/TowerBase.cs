
using UnityEngine;



public class TowerBase : MonoBehaviour
{
    private Tower _tower;
    [SerializeField] private Vector3 _towerOffset;
    [SerializeField] private Canvas towerCanvas;

    private void Start()
    {
        towerCanvas.gameObject.SetActive(false);
        towerCanvas.worldCamera = FindObjectOfType<Camera>();
    }

    private void OnMouseDown()
    {
        if(_tower == null)
        {
            towerCanvas.gameObject.SetActive(true);
            AudioManager.Instance.Play("Click");
        }
          
        Invoke(nameof(CloseTowerCanvas), 3);
    }
  
    public void SetTower(Tower tower)
    {
        _tower = tower;
        
    }
    public void CloseTowerCanvas()
    {
        towerCanvas.gameObject.SetActive(false);
    }

    public void ResetInvok()
    {
        CancelInvoke(nameof(CloseTowerCanvas));
        Invoke(nameof(CloseTowerCanvas), 5);
    }

}
