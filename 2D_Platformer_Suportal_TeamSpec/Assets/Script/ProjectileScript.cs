using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	[SerializeField]
	int damage = 10;
	[SerializeField]
	GameObject explosion = null;

	private AudioSource _hitSound;
	private AudioSource _shootSound;


	// Use this for initialization
	void Start () {
		_hitSound = gameObject.GetComponents<AudioSource>()[0];
		_shootSound = gameObject.GetComponents<AudioSource>()[1];
		StartCoroutine ("Finish");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
	
		IDamageable d = 
			other.gameObject.GetComponent<IDamageable> ();
		if (d != null) {
			_hitSound = gameObject.GetComponents<AudioSource>()[0];
			_hitSound.Play ();
			Instantiate (explosion).GetComponent<Transform> ().position = gameObject.GetComponent<Transform> ().position;
			d.Damage (damage);
		}

		Destroy (gameObject);
	}

	private IEnumerator Finish(){

		_shootSound.Play ();
		yield return new WaitForSeconds (10);
		Destroy (gameObject);
	}
}

public interface IDamageable{

	void Damage (int damage);

}
