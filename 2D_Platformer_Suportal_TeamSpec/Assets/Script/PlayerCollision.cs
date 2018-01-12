using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	[SerializeField]
	GameObject teleportPoint1 = null;
	[SerializeField]
	GameObject teleportPoint2 = null;

	private AudioSource _teleportSound;
	private AudioSource _collectSound;

	// Use this for initialization
	void Start () {
		_teleportSound = gameObject.GetComponents<AudioSource>()[1];
		_collectSound = gameObject.GetComponents<AudioSource>()[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag.Equals ("mirror")) {
			if (Player.Instance.Teleport) {
				if (other.gameObject.Equals (teleportPoint1) && teleportPoint2.activeSelf) {
					gameObject.transform.position = new Vector2 (teleportPoint2.transform.position.x, teleportPoint2.transform.position.y - 1f);
					_teleportSound.Play();
				} else if (other.gameObject.Equals (teleportPoint2) && teleportPoint1.activeSelf) {
					gameObject.transform.position = new Vector2 (teleportPoint1.transform.position.x, teleportPoint1.transform.position.y - 1f);
					_teleportSound.Play();
				}
				Player.Instance.Teleport = false;
			}

			yield return new WaitForSeconds (0.5f);
			Player.Instance.Teleport = true;

		} else if (other.gameObject.tag.Equals ("entrance")) {
			Player.Instance.Score += 500;
			if (Player.Instance.Complete1) {
				Player.Instance.Complete2 = true;

			} else {
				Player.Instance.Complete1 = true;
			}


		}else if (other.gameObject.tag.Equals ("item")) {
			_collectSound.Play ();
			Player.Instance.Score += 100;
			Player.Instance.ItemCollect = true;
			other.gameObject.SetActive (false);

		}


	}
}
