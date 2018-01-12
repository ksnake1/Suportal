using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable {

	[SerializeField]
	private float forceMultiplier = 1f;
	[SerializeField]
	private float jumpMultiplier = 30f;
	[SerializeField]
	GameObject target = null;
	[SerializeField]
	Transform spawnPoint = null;
	[SerializeField]
	int _health = 100;

	[SerializeField]
	private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	const float k_GroundedRadius = .2f;                // Radius of the overlap circle to determine if grounded

	private bool dead = false;
	private Rigidbody2D _rigidBody = null;
	private Animator _animator = null;
	public AudioSource _loseSound;
	private AudioSource _deadSound;

	// Use this for initialization
	void Start () {
		_animator = gameObject.GetComponent<Animator> ();
		_rigidBody = gameObject.GetComponent<Rigidbody2D> ();
		_loseSound = gameObject.GetComponents<AudioSource>()[2];
		_deadSound = gameObject.GetComponents<AudioSource>()[3];


	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!dead) {
			Vector2 forceVect = new Vector2 (Input.GetAxis ("Horizontal"), 0);
			_rigidBody.AddForce (forceVect * forceMultiplier);

			float jump = Input.GetAxis ("Jump");
			if (jump > 0 && IsGrounded ()) {
			
				_rigidBody.AddForce (Vector2.up * jumpMultiplier);
			}

			_animator.SetInteger ("velocity", (int)(Mathf.Abs (_rigidBody.velocity.x)));

			if (_rigidBody.velocity.x > 0) {
				gameObject.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			} else if (_rigidBody.velocity.x < 0) {
				gameObject.transform.localScale = new Vector3 (-0.3f, 0.3f, 0.3f);
			}

			_animator.SetBool ("jump", !IsGrounded ());

			if (Input.GetKey (KeyCode.Q)) {
				target.SetActive (true);
				target.GetComponent<TargetController> ().Reset ();
			} else if (Input.GetKey (KeyCode.E)) {
				target.SetActive (false);
			}


		}
	}

	private bool IsGrounded(){
		bool Grounded = false;


		Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject&& !colliders[i].gameObject.tag.Equals("mirror")&& !colliders[i].gameObject.tag.Equals("aim")&& !colliders[i].gameObject.tag.Equals("projectile"))
				Grounded = true;
		}

		return Grounded;

	}

	public void Reset(){


		gameObject.GetComponent<Transform>().position = spawnPoint.position;
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		if (rb) {
			rb.velocity = Vector2.zero;
		}
	}

	public void Damage(int d){
		_health -= d;
		if (_health <= 0) {
			if (Player.Instance.Life > 0) {
				_loseSound.Play ();
				StartCoroutine ("Blink");
				_health = 100;
			} else {
				dead = true;
				_deadSound.Play ();
				_animator.SetBool ("dead", dead);

			}

		}
	}

	private IEnumerator Blink(){
		gameObject.GetComponent<Collider2D> ().enabled = false;
		yield return new WaitForSeconds (2f);
		//other.gameObject.GetComponent<PlayerController> ().Reset();
		gameObject.GetComponent<Collider2D> ().enabled = true;


	}



		
}
