using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMan : MonoBehaviour
{
	public static CameraMan instance;

	public Transform CameraStartPos;
	public Transform CameraEndPos;
	public Transform TrixieStartPos;
	public Transform TrixieEndPos;

	private Transform _trixieFront;
	private Transform _trixieBack;

	public void RegisterTrixieTransform(Transform trixieFront, Transform trixieBack)
	{
		_trixieFront = trixieFront;
		_trixieBack = trixieBack;
	}

	void Awake ()
	{
		instance = this;
	}

	void Update ()
	{
		if (_trixieFront != null && _trixieBack != null)
		{
			float trixieZPos = Mathf.Lerp(_trixieFront.position.z, _trixieBack.position.z, .5f);
			float factor = Mathf.InverseLerp(TrixieStartPos.position.z, TrixieEndPos.position.z, trixieZPos);
			float camZPos = Mathf.Lerp(CameraStartPos.position.z, CameraEndPos.position.z, factor);
			transform.position = new Vector3(transform.position.x, transform.position.y, camZPos);
		}
	}

}
