using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class TextController : MonoBehaviour {

	public Text text;
    public float score;

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			TweenText ();
		}
	}

	void TweenText() 
	{
        DOTween.To(() => score, x => score = x, Random.Range(1, 1000), 1).OnUpdate(UpdateUI);
	}

    void UpdateUI()
    {
        text.text = Mathf.Round(score).ToString();
    }
}
