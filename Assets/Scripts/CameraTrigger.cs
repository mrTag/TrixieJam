﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	public int CameraPosIndex;
	public Collider SolidifiedCollider;

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.CompareTag("trixie"))
		{
			CameraMan.instance.JumpToPos(CameraPosIndex);
			if (SolidifiedCollider != null)
			{
				SolidifiedCollider.gameObject.SetActive(true);
				SolidifiedCollider.isTrigger = false;
			}
			gameObject.SetActive(false);
		}
	}
}
