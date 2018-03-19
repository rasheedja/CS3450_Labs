using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CubeController : MonoBehaviour {

	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			Tween();
		}

	}

	void Tween() 
	{
        transform.DOMove(new Vector3(Random.Range(-4, 4), transform.position.y, Random.Range(-4, 4)), 2).SetEase(Ease.InOutCubic).SetLoops(5, LoopType.Yoyo).OnComplete(TweenComplete);
	}

    void TweenComplete()
    {
        Debug.Log("Tween Completed");
    }


}
