using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrixiePart {
	Front,
	Back
}

[RequireComponent(typeof(Rigidbody))]
public class TrixieController : MonoBehaviour {

	public TrixiePart ControlsBodyPart;
	public float MovementForce;
	public Vector3 HorizMovementAxis = new Vector3(1,0,0);
	public Vector3 VertMovementAxis = new Vector3(0,0,1);
	public float MaxSpeed;
	public float GroundCheckDist = 2.5f;

	private bool onGround = false;
	public bool OnGround { get { return onGround; } }

	private Rigidbody TrixieBody;
	private string HorizontalInputAxis;
	private string VerticalInputAxis;

	// Use this for initialization
	void Start () {
		TrixieBody = GetComponent<Rigidbody>();
		if(ControlsBodyPart == TrixiePart.Front){
			HorizontalInputAxis = "HorizontalFront";
			VerticalInputAxis = "VerticalFront";
		}
		else {
			HorizontalInputAxis = "HorizontalBack";
			VerticalInputAxis = "VerticalBack";
		}
		HorizMovementAxis.Normalize();
		VertMovementAxis.Normalize();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Debug.DrawLine(transform.position, transform.position - transform.up * GroundCheckDist, Color.yellow, 0, false);
		onGround = Physics.Raycast(transform.position, -transform.up, GroundCheckDist);

		Vector2 input = new Vector2(
			Input.GetAxis(HorizontalInputAxis),
			Input.GetAxis(VerticalInputAxis)
		);

		if(!onGround) input = Vector2.zero;

		if(input.sqrMagnitude > 0){
			Vector3 worldForce = (input.x * HorizMovementAxis) + (input.y * VertMovementAxis);
			worldForce.Normalize();
			float currentSpeedInMovementDir = Vector3.Dot(worldForce, TrixieBody.velocity);
			worldForce *= MovementForce * (1 - Mathf.InverseLerp(0, MaxSpeed, currentSpeedInMovementDir));
			TrixieBody.AddForce(worldForce * Time.fixedDeltaTime, ForceMode.Force);
			TrixieBody.drag = 0.5f;
		}
		else {
			TrixieBody.drag = 5;
		}
	}
}
