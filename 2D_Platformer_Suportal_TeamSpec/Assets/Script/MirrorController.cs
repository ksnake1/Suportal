using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorController : MonoBehaviour {



	void Start(){
		gameObject.SetActive (false);
	}

	void Update(){
		if (Input.GetKey (KeyCode.R)) {
			gameObject.SetActive (false);
		}
	}

}
