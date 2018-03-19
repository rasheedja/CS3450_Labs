using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == Globals.BALL) {
			GameManager.Instance.IncreaseScore ();
		}
	}


}
