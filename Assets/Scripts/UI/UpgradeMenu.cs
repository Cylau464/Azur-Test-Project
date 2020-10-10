﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _damageLevel = null;
    [SerializeField] private TextMeshProUGUI _damageCost = null;
    [Space]
    [SerializeField] private TextMeshProUGUI _coinsMultiplierLevel = null;
    [SerializeField] private TextMeshProUGUI _coinsMultiplierCost = null;
    [Space]
    [SerializeField] private TextMeshProUGUI _healthLevel = null;
    [SerializeField] private TextMeshProUGUI _healthCost = null;

    [Header("Buttons")]
    [SerializeField] private Button _damageUpgradeBtn = null;
    [SerializeField] private Button _coinsUpgradeBtn = null;
    [SerializeField] private Button _healthUpgradeBtn = null;

    [Header("Disable Image")]
    [SerializeField] private Image _damageDisableImage = null;
    [SerializeField] private Image _coinsDisableImage = null;
    [SerializeField] private Image _healthDisableImage = null;

    [Header("Upgrade Image")]
    [SerializeField] private RectTransform _damageUpgrade = null;
    [SerializeField] private RectTransform _coinsUpgrade = null;
    [SerializeField] private RectTransform _healthUpgrade = null;
    
    [SerializeField] private float _verticalAmplitude = 20f;
    [SerializeField] private float _verticalSpeed = 1f;
    private float _startPosY;


    [Space]
    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private GraphicRaycaster _raycaster = null;

    private void Start()
    {
        DamageUpgrade();
        HealthUpgrade();
        CoinsUpgrade();

        GameManager.gameStart.AddListener(HideMenu);
        UpgradeStats.damageUpgrade.AddListener(DamageUpgrade);
        UpgradeStats.healthUpgrade.AddListener(HealthUpgrade);
        UpgradeStats.coinsUpgrade.AddListener(CoinsUpgrade);
    }

    private void Update()
    {
        float newPos = Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmplitude;
        _damageUpgrade.localPosition += Vector3.up * newPos;
        _coinsUpgrade.localPosition += Vector3.up * newPos;
        _healthUpgrade.localPosition += Vector3.up * newPos;
    }

    private void DamageUpgrade()
    {
        _damageLevel.text = "Ур. " + UpgradeStats.damage.ToString();
        _damageCost.text = UpgradeStats.damageUpgradeCost.ToString();
        CheckNewCost();
    }

    private void HealthUpgrade()
    {
        _healthLevel.text = "Ур. " + UpgradeStats.health.ToString();
        _healthCost.text = UpgradeStats.healthUpgradeCost.ToString();
        CheckNewCost();
    }

    private void CoinsUpgrade()
    {
        _coinsMultiplierLevel.text = "Ур. " + UpgradeStats.coinsMultiplier.ToString();
        _coinsMultiplierCost.text = UpgradeStats.coinsMultiplierUpgradeCost.ToString();
        CheckNewCost();
    }

    private void CheckNewCost()
    {
        if (UpgradeStats.damageUpgradeCost > GameManager.Coins)
        {
            _damageUpgradeBtn.interactable = false;
            _damageDisableImage.enabled = true;
            _damageUpgrade.gameObject.SetActive(false);
        }
        else
        {
            _damageUpgradeBtn.interactable = true;
            _damageDisableImage.enabled = false;
            _damageUpgrade.gameObject.SetActive(true);
        }

        if (UpgradeStats.healthUpgradeCost > GameManager.Coins)
        {
            _healthUpgradeBtn.interactable = false;
            _healthDisableImage.enabled = true;
            _healthUpgrade.gameObject.SetActive(false);
        }
        else
        {
            _healthUpgradeBtn.interactable = true;
            _healthDisableImage.enabled = false;
            _healthUpgrade.gameObject.SetActive(true);
        }

        if (UpgradeStats.coinsMultiplierUpgradeCost > GameManager.Coins)
        {
            _coinsUpgradeBtn.interactable = false;
            _coinsDisableImage.enabled = true;
            _coinsUpgrade.gameObject.SetActive(false);
        }
        else
        {
            _coinsUpgradeBtn.interactable = true;
            _coinsDisableImage.enabled = false;
            _coinsUpgrade.gameObject.SetActive(true);
        }
    }

    private void HideMenu()
    {
        _raycaster.enabled = false;
        _canvasGroup.alpha = 0f;
    }
}
