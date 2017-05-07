using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UprightController : MonoBehaviour {

	public float UprightForce;
	public float GroundCheckDist;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		bool onGround = Physics.Raycast(transform.position, Vector3.down, GroundCheckDist);

		float angleDiff = Vector3.Angle(Vector3.up, transform.up);
		if(angleDiff != 0){
			float springFactor = Mathf.InverseLerp(0, 90, angleDiff);
			if(!onGround)
				springFactor *= 0.01f;
			Vector3 torqueAxis = Vector3.Cross(Vector3.up, transform.up);			
			rb.AddTorque(-springFactor * torqueAxis * UprightForce * Time.fixedDeltaTime, ForceMode.Force);
		}
	}
}
