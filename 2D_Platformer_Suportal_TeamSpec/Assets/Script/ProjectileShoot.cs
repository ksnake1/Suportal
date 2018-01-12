using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {

	[SerializeField]
	private GameObject projectile;
	[SerializeField]
	private float projectileForce=500f;
	private Animator _animator = null;

	Rigidbody2D _rb;
	Transform _transform;


	// Use this for initialization
	void Start () {
		_animator = gameObject.GetComponent<Animator> ();
		_transform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {

			_animator.SetTrigger ("fire");


		
		}
	}

	public void FireProjectile(){
		if (Player.Instance.ItemCollect) {
			GameObject p = 
				Instantiate (projectile,
					new Vector2 (_transform.position.x, _transform.position.y + 0.75f), 
					Quaternion.identity);
			_rb = p.GetComponent<Rigidbody2D> ();
			Physics2D.IgnoreCollision (
				gameObject.GetComponent<Collider2D> (),
				p.GetComponent<Collider2D> ());
			_rb.AddForce (-(_transform.right)
			* projectileForce
			* _transform.localScale.x);
			p.transform.localScale = _transform.localScale;
		}
	}

}
