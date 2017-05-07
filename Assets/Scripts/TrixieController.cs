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
	public float JumpImpulse = 1000;
	public Animator TrixieAnimator;

	private bool onGround = false;
	public bool OnGround { get { return onGround; } }

	private Rigidbody TrixieBody;
	private string HorizontalInputAxis;
	private string VerticalInputAxis;
	private string JumpButton;
	private int WalkAnimParameter;

	// Use this for initialization
	void Start () {
		TrixieBody = GetComponent<Rigidbody>();
		if(ControlsBodyPart == TrixiePart.Front){
			HorizontalInputAxis = "HorizontalFront";
			VerticalInputAxis = "VerticalFront";
			JumpButton = "JumpFront";
			WalkAnimParameter = Animator.StringToHash("SpeedFront");
		}
		else {
			HorizontalInputAxis = "HorizontalBack";
			VerticalInputAxis = "VerticalBack";
			JumpButton = "JumpBack";
			WalkAnimParameter = Animator.StringToHash("SpeedBack");
		}
		HorizMovementAxis.Normalize();
		VertMovementAxis.Normalize();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		onGround = Physics.Raycast(transform.position, -transform.up, GroundCheckDist);

		Vector2 input = new Vector2(
			Input.GetAxis(HorizontalInputAxis),
			Input.GetAxis(VerticalInputAxis)
		);

		if(!onGround) input *= 0.33333f;

		if(input.sqrMagnitude > 0){
			Vector3 worldForce = (input.x * HorizMovementAxis) + (input.y * VertMovementAxis);
			float movementLen = worldForce.magnitude;
			worldForce /= movementLen;
			float currentSpeedInMovementDir = Vector3.Dot(worldForce, TrixieBody.velocity);
			worldForce *= MovementForce * (1 - Mathf.InverseLerp(0, MaxSpeed*movementLen, currentSpeedInMovementDir));
			TrixieBody.AddForce(worldForce * Time.fixedDeltaTime, ForceMode.Force);
			TrixieBody.drag = 0.5f;
		}
		else {
			if(onGround)
				TrixieBody.drag = 5;
			else
				TrixieBody.drag = 0.1f;
		}

		if(onGround && Input.GetButtonDown(JumpButton)){
			TrixieBody.AddForce(Vector3.up * JumpImpulse, ForceMode.Impulse);
		}

		float speedParam = Mathf.InverseLerp(0, MaxSpeed, TrixieBody.velocity.magnitude);
		TrixieAnimator.SetFloat(WalkAnimParameter, onGround ? speedParam : 0);
	}
}
