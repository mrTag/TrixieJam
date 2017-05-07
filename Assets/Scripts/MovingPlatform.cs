using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float Speed;
	public Transform[] Targets;
	public float DistanceThreshold;

	private int _currentTargetIndex;
	private Rigidbody _rb;

	void Start ()
	{
		_rb = GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
		if (Vector3.Distance(transform.position, Targets[_currentTargetIndex].position) <= DistanceThreshold)
			NextTarget();
		_rb.velocity = (Targets[_currentTargetIndex].position - transform.position).normalized * Speed;
	}

	void NextTarget()
	{
		_currentTargetIndex++;
		if (_currentTargetIndex == Targets.Length)
			_currentTargetIndex = 0;
	}
}
