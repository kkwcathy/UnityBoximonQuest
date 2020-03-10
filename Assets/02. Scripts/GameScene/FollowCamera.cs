﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

	public Transform playerFocus;

	Transform target;

	public float moveDamping = 15.0f;
	//public float rotateDamping = 10.0f;
	private float xDistance;
	private float zDistance;

	private float _height;

	private Transform tr;

	//bool isTargetChange = false;
	bool isEnemyFocus = false;

	Vector3 followPos;

	// Start is called before the first frame update
	void Start()
	{
		target = playerFocus;

		tr = GetComponent<Transform>();

		xDistance = tr.position.x;
		zDistance = tr.position.z;
		_height = tr.position.y;
	}

    public void ChangeDistance(int x, int y, int z)
    {
        xDistance += x;
        _height += y;
        zDistance += z;
    }
	private void LateUpdate()
	{
		followPos = target.position + (Vector3.forward * zDistance) + (Vector3.right * xDistance);
		followPos.y = _height;

		if (!isEnemyFocus)
		{
			tr.position = followPos;
		}
		else
		{
			tr.position = Vector3.Lerp(tr.position, followPos, Time.deltaTime * moveDamping);

			if (Utility.GetIsNear(tr.position, followPos) && target == playerFocus)
			{
				isEnemyFocus = false;
			}

		}
		
	}
	public void ChangeTarget(Transform change)
	{
		isEnemyFocus = true;

		StartCoroutine(FocusEnemy(change));
	}

	public IEnumerator FocusEnemy(Transform change)
	{

		target = change;
		yield return new WaitForSeconds(Utility.spawnDelayTime);

		target = playerFocus;

		
	}
}