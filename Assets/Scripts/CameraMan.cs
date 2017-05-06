using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMan : MonoBehaviour
{
	public static CameraMan instance;

	public Transform[] CameraPositions;

	private int mCurrentPosIndex = 0;

	void Awake ()
	{
		instance = this;
	}
	
	public void NextPos ()
	{
		mCurrentPosIndex++;
		mCurrentPosIndex = Mathf.Clamp(mCurrentPosIndex, 0, CameraPositions.Length - 1);
		transform.DOKill();
		transform.DOMove(CameraPositions[mCurrentPosIndex].position, 1.5f).SetEase(Ease.InOutSine);
	}

	public void PreviousPos()
	{
		mCurrentPosIndex--;
		mCurrentPosIndex = Mathf.Clamp(mCurrentPosIndex, 0, CameraPositions.Length - 1);
		transform.DOKill();
		transform.DOMove(CameraPositions[mCurrentPosIndex].position, 1.5f).SetEase(Ease.InOutSine);
	}

	public void JumpToPos(int index)
	{
		if (index == mCurrentPosIndex) return;
		mCurrentPosIndex = index;
		mCurrentPosIndex = Mathf.Clamp(mCurrentPosIndex, 0, CameraPositions.Length - 1);
		transform.DOKill();
		transform.DOMove(CameraPositions[mCurrentPosIndex].position, 1.5f).SetEase(Ease.InOutSine);
	}

	#if UNITY_EDITOR
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			NextPos();
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			PreviousPos();
		}
	}
	#endif
}
