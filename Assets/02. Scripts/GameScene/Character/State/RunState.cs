﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : CharacterState
{
	public RunState(Character character) : base(character)
	{

	}

	public override void StartState()
	{
		_character.BeginMove();
		_character.PlayAnimation("Run");
	}

	public override void UpdateState()
	{
		_character.SearchTarget();

		if(!_character.CheckTargetExist())
		{
			_character.ChangeState(Character.StateType.NoTarget);
		}
		else if (_character.CheckTargetDistance())
		{
			_character.ChangeState(Character.StateType.Fight);
		}
	}
}
