using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	[SerializeField]
	private float speed = .3f;

	[SerializeField]
	private float leftX;
	[SerializeField]
	private float rightX;
	[SerializeField]
	private float topY;
	[SerializeField]
	private float bottomY;
	[SerializeField]
	private float maxleftX;
	[SerializeField]
	private float maxrightX;
	[SerializeField]
	private float maxtopY;
	[SerializeField]
	private float maxbottomY;
	[SerializeField]
	Transform boundaryCentre = null;



	private Transform _transform;
	private Vector2 _currentPos;


	// Use this for initialization
	void Start () {
		_transform = gameObject.GetComponent<Transform> ();
		_currentPos = _transform.position;
		Reset ();
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {


		_currentPos = _transform.position;

		if (Input.GetKey (KeyCode.I)) {
			_currentPos += new Vector2 (0, speed);
		}

		else if (Input.GetKey (KeyCode.K)) {
			_currentPos -= new Vector2 (0, speed);
		}
		else if (Input.GetKey (KeyCode.J)) {
			_currentPos -= new Vector2 (speed, 0);
		}
		else if (Input.GetKey (KeyCode.L)) {
			_currentPos += new Vector2 (speed, 0);
		} 

		checkBounds();//Playable game object cannot go outside of view of camera
		_transform.position = _currentPos;
	}

	private void checkBounds(){
		//prohibit bounds of horizontal movement

		if (_currentPos.x < boundaryCentre.position.x - leftX) {
			_currentPos.x = boundaryCentre.position.x - leftX;

		}
		if (_currentPos.x < maxleftX) {
			_currentPos.x = maxleftX;				
		}
		if (_currentPos.x > boundaryCentre.position.x + rightX) {
			_currentPos.x = boundaryCentre.position.x + rightX;

		}
		if (_currentPos.x > maxrightX) {
			_currentPos.x = maxrightX;				
		}
		if (_currentPos.y < boundaryCentre.position.y - bottomY) {
			_currentPos.y = boundaryCentre.position.y - bottomY;

		}
		if (_currentPos.y > maxtopY) {
			_currentPos.y = maxtopY;				
		}
		if (_currentPos.y > boundaryCentre.position.y + topY) {
			_currentPos.y = boundaryCentre.position.y + topY;

		}
		if (_currentPos.y < maxbottomY) {
			_currentPos.y = maxbottomY;				
		}
	}

	public void Reset(){
		_currentPos = new Vector2(boundaryCentre.position.x, boundaryCentre.position.y+1f);
		_transform.position = _currentPos;
	
	}




}
