using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
	public int CameraPosIndex;

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.gameObject.CompareTag("trixie"))
			CameraMan.instance.JumpToPos(CameraPosIndex);
	}
}
