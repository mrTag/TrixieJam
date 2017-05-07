using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject TrixiePrefab;
	public Transform TrixieSpawn;
	public Transform CameraTransform;
	public EndingScript Ending;

	void Start()
	{
		Vector3 camForwardProjected = new Vector3(CameraTransform.forward.x, 0f, CameraTransform.forward.z).normalized;
		Vector3 camRightProjected = new Vector3(CameraTransform.right.x, 0f, CameraTransform.right.z).normalized;
		GameObject trixie = Instantiate(TrixiePrefab, TrixieSpawn.position, TrixieSpawn.rotation);
		TrixieController[] ctrlrs = trixie.GetComponentsInChildren<TrixieController>();
		CameraMan.instance.RegisterTrixieTransform(ctrlrs[0].transform, ctrlrs[1].transform);
		for (int i = 0; i < ctrlrs.Length; ++i)
		{
			ctrlrs[i].VertMovementAxis = camForwardProjected;
			ctrlrs[i].HorizMovementAxis = camRightProjected;
		}
		Ending.TrixieObj = trixie;
	}
}
