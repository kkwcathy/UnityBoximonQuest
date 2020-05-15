﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 공격 동작 상위 클래스
public class AttackBase
{
	protected Character _character;
	protected GameObject _skillPrefab;
	
	protected float _elapsedTime = 0.0f;

	protected float _fireTime = 0.0f; // 애니메이션 시작 후 이펙트 발사까지 소요되는 시간
	protected float _finishTime = 0.0f;

	protected float _additionalPower;
	protected float _minDistance;
	protected float _coolTime;

	protected bool _isFired = false;

	protected float _finalPower;

	public void SetSkillInfo(string effectPath, float power, float distance, float coolTime)
	{
		_skillPrefab = Resources.Load(effectPath) as GameObject;
		_additionalPower = power;
		_minDistance = distance;
		_coolTime = coolTime;
	}

	public void SetCharacter(Character character)
	{
		_character = character;
	}

	virtual public void Init()
	{

	}

	virtual public void SetFirePoint(Transform effecTr)
	{

	}

	virtual public void StartAttack()
	{
		_elapsedTime = 0.0f;
		_isFired = false;
	}

	public void SetPower(float basePower)
	{
		_finalPower = basePower * _additionalPower;
	}

	public void AddElapsedTime()
	{
		_elapsedTime += Time.deltaTime;
		_elapsedTime = Mathf.Clamp(_elapsedTime, 0.0f, _coolTime);
	}

	public void SpawnSkillEffect()
	{
		GameObject skillEffect = _character.Fire(_skillPrefab);
		SetFirePoint(skillEffect.transform);

		// 스킬 이펙트에 공격력 전달
		skillEffect.GetComponent<SkillBase>().Power = _finalPower;
	}

	public bool IsAttackable()
	{
		return _elapsedTime >= _coolTime &&
				_character.CheckTargetDistance(_minDistance);
	}

	public bool IsFinished()
	{
		return _elapsedTime >= _finishTime;
	}

	public float GetMinDistance()
	{
		return _elapsedTime;
	}

	virtual public void RunAttack()
	{
	}

	//[SerializeField] protected GameObject _skillEffect = null;

	//[SerializeField] protected float _basicSkillPower = 20.0f; // 테스트 위해 임시로 설정

	//protected Character _character;

	//protected bool IsStart = true;
	//protected float _elapsedTime = 0.0f;

	//public AttackBase(Character character)
	//{
	//	_character = character;
	//}

	//virtual public void Fire(Transform tr)
	//{ 
	//}

	//public float GetElapsedTime()
	//{
	//	return _elapsedTime;
	//}
}
