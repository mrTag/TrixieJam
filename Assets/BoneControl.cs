using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneControl : MonoBehaviour {

	public Transform ControlObject;
	private Quaternion rotationOffset;
	private Vector3 positionOffset;
		
	void Start () {
		rotationOffset = Quaternion.FromToRotation(ControlObject.forward, transform.forward);
		positionOffset = this.transform.position - ControlObject.transform.position;
	}
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = ControlObject.transform.position + positionOffset;
		this.transform.rotation = ControlObject.transform.rotation * rotationOffset;
	}
}
