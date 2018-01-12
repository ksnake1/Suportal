using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	[SerializeField]
	Text lifeLabel;
	[SerializeField]
	Text scoreLabel;
	[SerializeField]
	Text gameOverLabel;
	[SerializeField]
	Text highScoreLabel;
	[SerializeField]
	Text hintLabel;
	[SerializeField]
	Button restartBtn;
	[SerializeField]
	GameObject Character;//playerble character




	private void initialize (){
		//initially score and life reset, GUI for gameover is set false, gameplay GUI set true
		Player.Instance.Score = 0;
		Player.Instance.Life = 3;

		Player.Instance.Complete1 = false;
		Player.Instance.Complete2 = false;

		gameOverLabel.gameObject.SetActive (false);
		highScoreLabel.gameObject.SetActive (false);
		restartBtn.gameObject.SetActive (false);
		hintLabel.gameObject.SetActive (false);

		lifeLabel.gameObject.SetActive (true);
		scoreLabel.gameObject.SetActive (true);
		Character.gameObject.SetActive (true);
		updateUI ();
	}

	private void initialize_level2 (){
		//initially score and life reset, GUI for gameover is set false, gameplay GUI set true

		if (Player.Instance.Life <= 0) {
			Player.Instance.Score = 0;
			Player.Instance.Life = 3;
		}
		gameOverLabel.gameObject.SetActive (false);
		highScoreLabel.gameObject.SetActive (false);
		restartBtn.gameObject.SetActive (false);
		hintLabel.gameObject.SetActive (false);

		lifeLabel.gameObject.SetActive (true);
		scoreLabel.gameObject.SetActive (true);
		updateUI ();

	}



	public void updateUI(){
		//Modify GUI during gameplay
		scoreLabel.text = "Score: " + Player.Instance.Score;
		lifeLabel.text = "Life: " + Player.Instance.Life;
		highScoreLabel.text = "Score Record: " + Player.Instance.HighScore;

	}

	public void gameOver(){
		//gameover disable playerable character, score and life; Displays gameover lable, record score and restart button

		gameOverLabel.gameObject.SetActive (true);
		highScoreLabel.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (true);

		hintLabel.gameObject.SetActive (false);
		lifeLabel.gameObject.SetActive (false);
		scoreLabel.gameObject.SetActive (false);
		Character.gameObject.GetComponent<PlayerController>().Damage(100);//playerable game object disappers when game is over

	}

	// Use this for initialization
	void Start () {
		Player.Instance.gCtrl = this;
		if (!Player.Instance.Complete1 && !Player.Instance.Complete2) {
			initialize ();
		} else if (Player.Instance.Complete1 && !Player.Instance.Complete2) {
			initialize_level2 ();
		} else if (Player.Instance.Complete1 && Player.Instance.Complete2) {
			initialize ();
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void RestartBtnClick(){
		Debug.Log ("gotta restart");
		//reload the scene

		if (!Player.Instance.Complete1 && !Player.Instance.Complete2) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		} else if (Player.Instance.Complete1 && !Player.Instance.Complete2) {
			SceneManager.LoadScene (1);
		} else if (Player.Instance.Complete1 && Player.Instance.Complete2) {
			SceneManager.LoadScene (0);
		}
	}

	public void newRecord(){
		//show new record score made
		highScoreLabel.text = "New Record: " + Player.Instance.HighScore + "!!!";
	}


	public void complete1(){
		gameOverLabel.text = "Next Level!";
		gameOverLabel.gameObject.SetActive (true);
		highScoreLabel.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (true);

		hintLabel.gameObject.SetActive (false);
		lifeLabel.gameObject.SetActive (false);
		scoreLabel.gameObject.SetActive (false);
		Character.gameObject.SetActive (false);

	}

	public void complete2(){
		gameOverLabel.text = "You Won!";
		gameOverLabel.gameObject.SetActive (true);
		highScoreLabel.gameObject.SetActive (true);
		restartBtn.gameObject.SetActive (true);

		hintLabel.gameObject.SetActive (false);
		lifeLabel.gameObject.SetActive (false);
		scoreLabel.gameObject.SetActive (false);
		Character.gameObject.SetActive (false);

	}

	public void hint(){
		hintLabel.gameObject.SetActive (true);

	}

}