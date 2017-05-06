using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteController : MonoBehaviour {

	public LayerMask BiteLayerMask;
	private bool currentlyBitten = false;
	private HingeJoint biteJoint;
	private GameObject parentGO;
	private Rigidbody parentRB;

	bool biteDownLastFrame = false;

	// Use this for initialization
	void Start () {
		parentGO = transform.parent.gameObject;
		parentRB = parentGO.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		bool biteDown = Input.GetButton("BiteFront") || Mathf.Abs(Input.GetAxis("BiteFront")) > 0.1f;
		if(currentlyBitten){
			if(!biteDown){
				if(biteJoint != null) Destroy(biteJoint);
				currentlyBitten = false;
			}
		}
		else if(biteDown && !biteDownLastFrame){
			var overlapBite = Physics.OverlapSphere(transform.position, 0.5f * transform.localScale.x, BiteLayerMask);
			if(overlapBite.Length > 0){
				if(biteJoint != null) Destroy(biteJoint);
				biteJoint = parentGO.AddComponent<HingeJoint>();
				Rigidbody bittenBody = null;
				foreach(var biteColl in overlapBite){
					bittenBody = biteColl.GetComponent<Rigidbody>();
					if(bittenBody != null) break;
				}
				biteJoint.connectedBody = bittenBody;
				biteJoint.anchor = transform.localPosition;
				biteJoint.axis = Vector3.up;
				biteJoint.useSpring = true;
				var spring = new JointSpring() { spring=1000, damper=100 };
				biteJoint.spring = spring;
				currentlyBitten = true;
			}
		}
		biteDownLastFrame = biteDown;
	}
}
