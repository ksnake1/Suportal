using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	[SerializeField]
	private float closeDelay = 2f;
	[SerializeField]
	private float openPosition;
	[SerializeField]
	private float closedPosition;
	[SerializeField]
	private float speed = 0.1f;

	private bool isOpen = false;
	private AudioSource _doorSound;

	void OnTriggerEnter2D(Collider2D other){

		StartCoroutine ("Open");

	}

	private IEnumerator Open(){

		if (!isOpen) {

			_doorSound.Play ();
			isOpen = true;

			for(float p = closedPosition; p>= openPosition; p=p-speed){
				gameObject.transform.position = 
					new Vector2 (gameObject.transform.position.x, 
						p);
				yield return new WaitForSeconds (0.1f);

			}

			yield return new WaitForSeconds (closeDelay);
			StartCoroutine ("Close");

		}

	}

	private IEnumerator Close(){

		if (isOpen) {

			for(float p = openPosition; p<= closedPosition; p=p+speed){
				gameObject.transform.position = 
					new Vector2 (gameObject.transform.position.x, 
						p);
				yield return new WaitForSeconds (0.1f);

			}
			isOpen = false;

		}

	}

	// Use this for initialization
	void Start () {
		_doorSound = gameObject.GetComponent<AudioSource>();
		gameObject.transform.position = 
			new Vector2 (gameObject.transform.position.x, 
				closedPosition);
	}

	// Update is called once per frame
	void Update () {

	}
}
