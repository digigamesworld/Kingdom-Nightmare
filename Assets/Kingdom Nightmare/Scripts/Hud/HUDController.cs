using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;



public  class HUDController : SingletonMB<HUDController>
{
    //type declaration
    [Serializable]
    public struct TowerInventory
    {
        public GameObject      towerInventorypanel;
        public TextMeshProUGUI damegeTxt;
        public TextMeshProUGUI firerateTxt;
        public TextMeshProUGUI SpecsTxt;
        public Image           Icon;
    }
 

  
    [SerializeField] private TextMeshProUGUI            _health;
    [SerializeField] private TextMeshProUGUI            _Gold;
    [SerializeField] private TextMeshProUGUI            _WaveCount;
    [SerializeField] private TowerInventory             _towerInventory;
    [SerializeField] private GameObject                 _towerButtomPrefab;
    [SerializeField] private Slider                     _gameSpeedSlider;

    [Serializable]
    private struct EndScreenData
    {
        public string title;
        public GameObject _endScreenPanel;
        public TextMeshProUGUI txt;
        public Button restartBtn;
    }
    [SerializeField] private EndScreenData _endScreen;

  

    private void Awake()
    {
        //close hud panel at start
        _gameSpeedSlider.onValueChanged.AddListener(delegate { ChangeGameSpeed(); });
 
    }

    private void OnEnable()
    {
        GameSceneManager.UIOnValidate += OnValidateUI;

    }
    private void OnDisable()
    {
        GameSceneManager.UIOnValidate -= OnValidateUI;

    }
    private void Start()
    {
        CloseTowerInvetory();
        //Invoke(nameof(OnValidateUI), 0.2f);
    }
 
    private void OnValidateUI()
    {

        _health.text = GameSceneManager.Instance.CurrentPlayerHealth.ToString();
        _Gold.text = GameSceneManager.Instance.CurrentGold.ToString();
        _WaveCount.text = (GameSceneManager.Instance.CurrentWave+1).ToString() + "/" + GameSceneManager.Instance.WavesCount.ToString();
    }
    private void ChangeGameSpeed()
    {
        Time.timeScale = 1.0f + _gameSpeedSlider.value * 3.0f;
    }



    public void Exitgame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OpenTowerInventory(Sprite icon,string specTxt,float fireRate,int damage )
    {
        _towerInventory.Icon.sprite = icon;
        _towerInventory.SpecsTxt.text = specTxt;
        _towerInventory.firerateTxt.text = fireRate.ToString();
        _towerInventory.damegeTxt.text = damage.ToString();
        if (Input.mousePosition.x < Screen.width / 2)
            _towerInventory.towerInventorypanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 3, 0) ;
        else
            _towerInventory.towerInventorypanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 3, 0);

        _towerInventory.towerInventorypanel.SetActive(true);
        Invoke(nameof(CloseTowerInvetory), 5.0f);
    }
    public void CloseTowerInvetory()
    {
        CancelInvoke(nameof(CloseTowerInvetory));
        _towerInventory.towerInventorypanel.SetActive(false);
    }
    public void OpenEndScreen(bool win)
    {
        Time.timeScale = 0;
        _endScreen.restartBtn.onClick.AddListener(Restart);
        if (win)
        {
            _endScreen.txt.text = "Visctory";
            _endScreen._endScreenPanel.SetActive(true);
        }
        else
        {
            _endScreen.txt.text = "Game Over";
            _endScreen._endScreenPanel.SetActive(true);
        }
    }
}
