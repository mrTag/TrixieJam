using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FootstepSounds : MonoBehaviour {
	private Animator myAnimator;
	public AudioSource[] StepSounds;
	private bool isFrontReady = false;
	private bool isBackReady = false;
	
	public float Cooldown = 0.4f;

	void Start() {
		myAnimator = this.GetComponent<Animator>();		
		DOVirtual.DelayedCall(0.5f, Ready);
	}
	void StepFront () {
				
		if (isFrontReady) {
			int rand = Random.Range(0,StepSounds.Length-1);
			StepSounds[rand].volume = myAnimator.GetFloat("SpeedFront");
			StepSounds[rand].Play();			
			isFrontReady = false;
			DOVirtual.DelayedCall(Cooldown + Random.Range(0,0.15f), FrontReady);
		}		
	}
	void Ready() {
		isFrontReady = true;
		isBackReady = true;
	}

	void StepBack () { 
		if (isBackReady) {
			int rand = Random.Range(0,StepSounds.Length-1);
			StepSounds[rand].volume = myAnimator.GetFloat("SpeedBack");
			StepSounds[rand].Play();
			isBackReady = false;
			DOVirtual.DelayedCall(Cooldown + Random.Range(0,0.15f), BackReady);
		}
	}

	void FrontReady() {
		isFrontReady = true;
	}
	void BackReady() {
		isBackReady = true;
	}
	
}
