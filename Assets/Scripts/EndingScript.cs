using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour {

	int _controllersInTrigger = 0;

	void OnTriggerEnter(Collider other)
	{
		TrixieController ctrl = other.GetComponent<TrixieController>();
		if (ctrl != null)
		{
			_controllersInTrigger++;
			CheckEndingCondition();
		}
	}

	void OnTriggerExit(Collider other)
	{
		TrixieController ctrl = other.GetComponent<TrixieController>();
		if (ctrl != null)
		{
			_controllersInTrigger--;
		}
	}

	void CheckEndingCondition()
	{
		if (_controllersInTrigger >= 2)
		{
			Debug.Log("END");
		}
	}
}
