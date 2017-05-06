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
		Vector2 input = new Vector2(
			Input.GetAxis(HorizontalInputAxis),
			Input.GetAxis(VerticalInputAxis)
		);
		if(input.sqrMagnitude > 0){
			Vector3 worldForce = (input.x * HorizMovementAxis) + (input.y * VertMovementAxis);
			worldForce *= MovementForce;
			TrixieBody.AddForce(worldForce * Time.fixedDeltaTime, ForceMode.Force);
		}
	}
}
