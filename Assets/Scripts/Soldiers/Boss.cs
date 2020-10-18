﻿using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : EnemySoldier
{
    [Header("Health Bar")]
    [SerializeField] private Canvas _barCanvas = null;
    [SerializeField] private TextMeshPro _healthText = null;
    [SerializeField] private Image _healthBar = null;
    private float _displayedHealth;

    [Header("Attack Properties")]
    [SerializeField] private float _rangeAttack = 1f;
    [SerializeField] private int _maxTargetGiveDamage = 3;

    private new void Start()
    {
        base.Start();

        int sceneCount = (SceneManager.sceneCountInBuildSettings - 1);
        int cycleNumber = Mathf.FloorToInt((LevelManager.LevelNumber - 1) / sceneCount);
        int bossSpawnRate = 3;
        _maxHealth *= Mathf.CeilToInt(((float)LevelManager.LevelNumber / bossSpawnRate) / 2f) * (cycleNumber + 1);
        _displayedHealth = _health = _maxHealth;
        //_maxTargetGiveDamage += Mathf.CeilToInt(LevelManager.LevelNumber / bossSpawnRate) / 2;

        if (FightStage.bossHealthLeft > 0)
            _displayedHealth = _health = FightStage.bossHealthLeft;
        else
            FightStage.bossHealthLeft = _health;

        _healthText.text = _health.ToString();
        _healthBar.fillAmount = (float)_health / _maxHealth;
    }

    private void Update()
    {
        _barCanvas.transform.LookAt(_barCanvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        DecreaseHealthBar();
    }

    public override void GiveDamage()
    {
        Collider[] targets = Physics.OverlapSphere(_target.position, _rangeAttack, _friendlyLayer);
        int targetDamaged = 0;

        foreach (Collider sold in targets)
        {
            sold.GetComponent<Soldier>().GetDamage(_damage);
            targetDamaged++;

            if (targetDamaged >= _maxTargetGiveDamage)
                break;
        }
    }

    public override bool GetDamage(int damage)
    {
        base.GetDamage(damage);

        FightStage.bossHealthLeft = _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        //_healthText.text = _health.ToString();
        //_healthBar.fillAmount = (float)_health / _maxHealth;

        if (_health <= 0)
        {
            Dead(true);
            _displayedHealth = .1f;
            return true;
        }

        return false;
    }

    private void DecreaseHealthBar()
    {
        if (_displayedHealth <= _health) return;
        
        _displayedHealth -= Time.deltaTime * (_displayedHealth - _health);
        _healthText.text = Mathf.CeilToInt(_displayedHealth).ToString();
        _healthBar.fillAmount = _displayedHealth / _maxHealth;
    }
}