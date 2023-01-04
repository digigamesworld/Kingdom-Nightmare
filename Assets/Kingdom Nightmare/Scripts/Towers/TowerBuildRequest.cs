using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
[Serializable]
public struct TowerData
{
    public TowersSpecs towersSpecs;
    public TowerBase towerbase;
    public TowerTypes towerType;
    public Sprite normalIcon;
    public Sprite highlightedIcon;
}
public class TowerBuildRequest : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
   [SerializeField] private TowerData _towerData;
    private CanvasGroup _canvasGroup;
    //inspector properties
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _priceTxt;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _priceTxt.text = _towerData.towersSpecs.Price(0).ToString();

    }

    private void OnEnable()
    {
        GameSceneManager.UIOnValidate += OnValidateUI;
        OnValidateUI();
    }
    private void OnDisable()
    {
        GameSceneManager.UIOnValidate -= OnValidateUI;
    }
    private void OnValidateUI()
    {
        if (GameSceneManager.Instance.CurrentGold >= _towerData.towersSpecs.Price(0))
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1.0f;
            _iconImage.sprite = _towerData.normalIcon;
        }
        else
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.5f;
            _iconImage.sprite = _towerData.highlightedIcon;
        }

    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameSceneManager.Instance.CurrentGold < _towerData.towersSpecs.Price(0)) return;
            var tower = TowerBuilder.instance.BuildTower(_towerData.towerType, _towerData.towerbase.transform.position);
        if (tower != null)
        {
            GameSceneManager.Instance.CurrentGold -= _towerData.towersSpecs.Price(0);
            _towerData.towerbase.SetTower(tower);
            GameSceneManager.UIOnValidate?.Invoke();
            AudioManager.Instance.Play("Click");
        }
        _towerData.towerbase.CloseTowerCanvas();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _iconImage.sprite = _towerData.highlightedIcon;
        _towerData.towerbase.ResetInvok();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _iconImage.sprite = _towerData.normalIcon;
    }
}
