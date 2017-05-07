using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class DelayedBlendIn : MonoBehaviour {

	public float Delay = 5;

	// Use this for initialization
	IEnumerator Start () {
		var cg = GetComponent<CanvasGroup>();
		cg.alpha = 0;
		yield return new WaitForSeconds(Delay);
		cg.DOFade(1, 0.75f);
	}
	
}
