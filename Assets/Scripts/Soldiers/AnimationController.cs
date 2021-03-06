﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Soldier _soldier = null;
    [SerializeField] private Animator _animator = null;

    private int isAttackingParamID;
    private int isVictoryParamID;
    private int isDeadParamID;
    private int isChargeParamID;
    private int speedParamID;
    private int deadAnimParamID;
    private int attackAnimParamID;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _stepSounds = new AudioClip[1];
    [SerializeField] private AudioClip[] _attackClips = null;

    private void Start()
    {
        isAttackingParamID          = Animator.StringToHash("isAttacking");
        isDeadParamID               = Animator.StringToHash("isDead");
        isVictoryParamID            = Animator.StringToHash("isVictory");
        isChargeParamID             = Animator.StringToHash("isCharge");
        speedParamID                = Animator.StringToHash("speed");
        deadAnimParamID             = Animator.StringToHash("deadAnimation");
        attackAnimParamID           = Animator.StringToHash("attackAnimation");
    }

    void Update()
    {
        _animator.SetBool(isAttackingParamID, _soldier.isAttack);
        _animator.SetBool(isDeadParamID, _soldier.isDead);
        _animator.SetBool(isVictoryParamID, _soldier.isVictory);
        _animator.SetBool(isChargeParamID, _soldier.isCharge);

        _animator.SetInteger(deadAnimParamID, _soldier.deadAnimIndex);
        _animator.SetInteger(attackAnimParamID, _soldier.attackAnimIndex);
        
        _animator.SetFloat(speedParamID, _soldier.Speed);
    }

    private void GiveDamage()
    {
        AudioManager.PlayClipAtPosition(_attackClips[Random.Range(0, _attackClips.Length)], transform.position);
        _soldier.GiveDamage();
    }

    private void EndOfAttack()
    {
        _soldier.EndOfAttack();
    }

    private void StepSound(AnimationEvent evt)
    {
        // Dont call audio if animation is not active in blend tree
        if (evt.animatorClipInfo.weight <= .5f || _soldier.isStepSoundSource == false) return;

        int index = Random.Range(0, _stepSounds.Length);
        AudioManager.PlayClipAtPosition(_stepSounds[index], transform.position, .5f);
    }
}
