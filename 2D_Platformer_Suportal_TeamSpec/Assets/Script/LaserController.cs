using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

	// Use this for initialization
	//maximum and minimum speed bomb object will travel
	[SerializeField]
	float Speed = 5f;
	[SerializeField]
	Transform robot = null;
	[SerializeField]
	float x = 5f;
	[SerializeField]
	float y = 5f;
	[SerializeField]
	GameObject teleportPoint1 = null;
	[SerializeField]
	GameObject teleportPoint2 = null;
	[SerializeField]
	GameObject explosion = null;

	private Transform _tranform;
	private Vector2 _currentPos;
	private bool bullet_transport;
	private float _currentSpeed;
	private AudioSource _shootSound;
	private AudioSource _hitSound;

	// Use this for initialization
	void Start () {
		_tranform = gameObject.GetComponent<Transform> ();//transformation of bomb object

		_shootSound = gameObject.GetComponents<AudioSource>()[0];
		_hitSound = gameObject.GetComponents<AudioSource>()[1];


		if (gameObject.tag.Equals("bullet1")){
			bullet_transport = Player.Instance.Bullet1;
		}
		else if (gameObject.tag.Equals("bullet2")){
			bullet_transport = Player.Instance.Bullet2;
		}

		Reset ();//travel begins from the reset position
	}

	// Update is called once per frame
	void Update () {
		_currentPos = _tranform.position;
		_currentPos -= new Vector2(_currentSpeed,0);//position of bomb object updates toward left
		_tranform.position = _currentPos;

		if (_currentPos.x < -16.18||_currentPos.x>14.7) {//bomb reset after out of camera view
			Reset ();
		}

	}

	public void Reset(){

		_tranform.position = new Vector2 (x, y);
		_currentSpeed = Speed;
		StartCoroutine ("DelayPlay");


	}

	public void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag.Equals ("player") && !Player.Instance.Immune) {
			StartCoroutine ("Blink", other);
		} else if (other.gameObject.tag.Equals ("player") && Player.Instance.Immune) {
			return;
		} else if (other.gameObject.tag.Equals ("robot")) {
			_hitSound.Play ();
			Player.Instance.Score += 100;
			StartCoroutine ("Delay");
			other.gameObject.SetActive (false);

			Instantiate (explosion).GetComponent<Transform> ().position = gameObject.GetComponent<Transform> ().position;
			return;

		} else if (other.gameObject.tag.Equals ("aim") || other.gameObject.tag.Equals ("bullet1") || other.gameObject.tag.Equals ("bullet2")) {
			return;
		} else if (other.gameObject.tag.Equals ("wall") && !bullet_transport) {
			_currentSpeed = _currentSpeed * -1f;
			return;
			
		}
		_hitSound.Play ();
		Instantiate (explosion).GetComponent<Transform> ().position = gameObject.GetComponent<Transform> ().position;
		Reset ();
	}

	public IEnumerator OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag.Equals ("mirror")) {


			if (bullet_transport) {
				if (other.gameObject.Equals (teleportPoint1) && teleportPoint2.activeSelf) {
					gameObject.transform.position = new Vector2 (teleportPoint2.transform.position.x - (_currentSpeed*5), teleportPoint2.transform.position.y);
					bullet_transport = false;
				} else if (other.gameObject.Equals (teleportPoint2) && teleportPoint1.activeSelf) {
					gameObject.transform.position = new Vector2 (teleportPoint1.transform.position.x - (_currentSpeed*5), teleportPoint1.transform.position.y);					
					bullet_transport = false;
				}

			}

			yield return new WaitForSeconds (0.5f);
			bullet_transport = true;
		} 
	}

	private IEnumerator Blink(Collision2D other){
		Player.Instance.Immune = true;
		other.gameObject.GetComponent<PlayerController>()._loseSound.Play ();
		other.gameObject.GetComponent<Collider2D> ().enabled = false;
		yield return new WaitForSeconds (1.5f);
		//other.gameObject.GetComponent<PlayerController> ().Reset();
		other.gameObject.GetComponent<Collider2D> ().enabled = true;
		Player.Instance.Immune = false;

	}

	private IEnumerator Delay(){
		yield return new WaitForSeconds (0.2f);
		gameObject.SetActive (false);
	}

	private IEnumerator DelayPlay(){
		yield return new WaitForSeconds (0.2f);
		_shootSound.Play ();
	}



}
