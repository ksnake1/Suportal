using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour {

	[SerializeField]
	GameObject mirror1 = null;
	[SerializeField]
	GameObject mirror2 = null;
	[SerializeField]
	GameObject explosion = null;

	private int mirror_cnt = 0;
	private AudioSource _setSound;
	private AudioSource _disableSound;

	// Use this for initialization
	void Start () {
		_disableSound = gameObject.GetComponents<AudioSource>()[0];
		_setSound = gameObject.GetComponents<AudioSource>()[1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag.Equals ("wall")) {
			if (mirror_cnt < 1) {
				mirror1.SetActive (true);
				mirror1.transform.position = gameObject.transform.position;
				mirror_cnt++;
			} else if (mirror_cnt > 0) {
				mirror2.SetActive (true);
				mirror2.transform.position = gameObject.transform.position;
				mirror_cnt = 0;
			}
			_setSound.Play ();
			gameObject.GetComponent<TargetController> ().Reset ();
		
		} else if (other.gameObject.tag.Equals ("blackWall")) {
			Instantiate (explosion).GetComponent<Transform> ().position = gameObject.GetComponent<Transform> ().position;
			_disableSound.Play ();
			gameObject.GetComponent<TargetController> ().Reset ();
		}
	}
}
