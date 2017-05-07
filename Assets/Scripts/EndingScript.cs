using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingScript : MonoBehaviour {

	public GameObject TrixieObj;
	public Transform Foot;
	public Transform EndingText;
	public AudioSource Music;
	private AudioSource _audio; 

	int _controllersInTrigger = 0;

	void Awake()
	{
		_audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other)
	{
		TrixieController ctrl = other.GetComponent<TrixieController>();
		if (ctrl != null)
		{
			_controllersInTrigger++;
			CheckEndingCondition();
		}
	}

	void OnTriggerExit(Collider other)
	{
		TrixieController ctrl = other.GetComponent<TrixieController>();
		if (ctrl != null)
		{
			_controllersInTrigger--;
		}
	}

	void CheckEndingCondition()
	{
		if (_controllersInTrigger >= 2)
		{
			Debug.Log("END");
			Foot.DOMoveY(0f, .3f).OnComplete(() => {
				_audio.Play();
				Music.Stop();
				TrixieObj.SetActive(false);
				EndingText.DOMoveY(7f, .5f).SetDelay(1f).SetEase(Ease.OutBounce);
			});
		}
	}
}
