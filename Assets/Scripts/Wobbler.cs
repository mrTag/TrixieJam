using UnityEngine;
using System.Collections;

public class Wobbler : MonoBehaviour {

	public Transform WobbelTarget;
	public float WobbleAmount = 100;
	public float SpringForce = 1;
	public float SpringDamping = 0.1f;
	public float MaxAngle = 45;

	private Vector3 lastFrameWorldPosition;
	private Vector3 lastFrameVelocity;
	private Vector2 currentWobbelVelocity;
	private Vector2 currentWobbelPosition;
	private float wobbelPositionToAngleFactor;
	private float wobbleRadius;

	// Use this for initialization
	void Start () {
		lastFrameWorldPosition = transform.position;
		wobbelPositionToAngleFactor = Mathf.Deg2Rad * MaxAngle;
		wobbleRadius = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 velocity = transform.position - lastFrameWorldPosition;
		Vector3 acceleration = (velocity - lastFrameVelocity) / Time.fixedDeltaTime;

		float localForwardAccel = WobbleAmount * Vector3.Dot(transform.forward, acceleration);
		float localRightAccel = WobbleAmount * Vector3.Dot(transform.right, acceleration);
		float localUpAccel = WobbleAmount * Vector3.Dot(transform.up, acceleration);

		currentWobbelVelocity.x += -localRightAccel * Time.fixedDeltaTime;
		currentWobbelVelocity.y += -localUpAccel * Time.fixedDeltaTime;

		currentWobbelPosition += currentWobbelVelocity * Time.fixedDeltaTime;
		currentWobbelPosition.x = Mathf.Clamp(currentWobbelPosition.x, -1, 1);
		currentWobbelPosition.y = Mathf.Clamp(currentWobbelPosition.y, -1, 1);

		currentWobbelVelocity -= currentWobbelPosition * SpringForce * Time.fixedDeltaTime;
		currentWobbelVelocity -= currentWobbelVelocity * SpringDamping * Time.fixedDeltaTime;

		Vector3 localTargetPosition = new Vector3(
		 	wobbleRadius * Mathf.Sin(currentWobbelPosition.x * wobbelPositionToAngleFactor),
			wobbleRadius * Mathf.Sin(currentWobbelPosition.y * wobbelPositionToAngleFactor),
			wobbleRadius * Mathf.Cos(currentWobbelPosition.x * wobbelPositionToAngleFactor) * Mathf.Cos(currentWobbelPosition.y * wobbelPositionToAngleFactor));
		WobbelTarget.transform.localPosition = localTargetPosition;
		WobbelTarget.localEulerAngles = new Vector3(-Mathf.Rad2Deg * Mathf.Sin(currentWobbelPosition.y * wobbelPositionToAngleFactor), Mathf.Rad2Deg * Mathf.Sin(currentWobbelPosition.x * wobbelPositionToAngleFactor), 0);

		lastFrameWorldPosition = transform.position;
		lastFrameVelocity = velocity;
	}
}
