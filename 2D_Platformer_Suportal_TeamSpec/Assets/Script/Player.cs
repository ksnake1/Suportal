using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{

	static private Player _instance;
	static public Player Instance{
		//player instance associated to game controller to make change to GUI and player record
		get{ 
			if (_instance == null) {
				_instance = new Player ();
			}
			return _instance;
		}
	}
	private Player (){



	}

	public static bool _teleport = true;

	public bool Teleport{
		get{
			return _teleport;
		}
		set{ 
			_teleport = value;

		}
	}

	public static bool _bullet1 = true;

	public bool Bullet1{
		get{
			return _bullet1;
		}
		set{ 
			_bullet1 = value;

		}
	}
	public static bool _bullet2 = true;

	public bool Bullet2{
		get{
			return _bullet2;
		}
		set{ 
			_bullet2 = value;

		}
	}

	public static bool _immune = false;

	public bool Immune{
		get{
			return _immune;
		}
		set{ 
			_immune = value;

		}
	}

	public GameController gCtrl;//associated to game controller to make change on GUI textboxes

	public int _score = 0;
	public int _life = 3;
	public int _highScore = 0;
	public bool _complete1 = false;
	public bool _complete2 = false;
	public bool _itemCollect = false;

	public int Score{
		get{
			return _score;
		}
		set{ 
			_score = value;//when score changes ui is updated
			gCtrl.updateUI ();
		}
	}

	public int Life{
		get{
			return _life;
		}
		set{ 
			_life = value;
			if (_life <= 0) {
				if (Score > HighScore) {
					HighScore = Score;// at gameover, if score exceeds highest previous score, the score becomes the new highest score



				}
				gCtrl.gameOver ();//gameover is called when life goes less than 1
			} 
			else {
				gCtrl.updateUI ();//ui is updated when life is above 0 and there is change

			}
		}
	}

	public int HighScore{
		get{
			return _highScore;
		}
		set{ 
			_highScore = value;
			gCtrl.newRecord ();//when highestscore changes new score is recorded

		}
	}

	public bool Complete1{
		get{
			return _complete1;
		}
		set{ 
			_complete1 = value;
			if (Score > HighScore) {
				HighScore = Score;// at gameover, if score exceeds highest previous score, the score becomes the new highest score


			}
			if (value == true){
				gCtrl.complete1 ();//when highestscore changes new score is recorded
			}
		}
	}

	public bool Complete2{
		get{
			return _complete2;
		}
		set{ 
			_complete2 = value;
			if (Score > HighScore) {
				HighScore = Score;// at gameover, if score exceeds highest previous score, the score becomes the new highest score



			}
			if (value == true){
				gCtrl.complete2 ();//when highestscore changes new score is recorded
			}
		}
	}

	public bool ItemCollect{
		get{
			return _itemCollect;
		}
		set{ 
			_itemCollect = value;
			if (value == true) {
				gCtrl.hint ();
			}
		}
	}




}
