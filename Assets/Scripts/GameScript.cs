using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

	// skin
	[SerializeField]
	private GUISkin customSkin;

	// variables
	private int cols = 4;
	private int rows = 4;
	private int numCards;
	private int matchesToWin;
	private int totalMatches;
	private int cardW = 100;
	 
	// card arrays and lists
	List<Card> generatedCards = new List<Card>();
	private Card[,] dealtCards;
	List<Card> flippedCards = new List<Card>();

	// game state
	private bool canClick = true;
	public static bool win;
	private bool gameOver = false;


	// Use this for initialization
	void Start () {
		win = false;
		numCards = cols * rows;
		matchesToWin = (int)(numCards * 0.5);

		buildDeck ();

		dealtCards = new Card[rows, cols];
		for (int i = 0; i < rows; i++) {
			for (var j = 0; j < cols; j++) {
				// take a random card from generatedCards and add to to dealt, removing from generatedCards
				int rand = Random.Range (0, generatedCards.Count);
				dealtCards [i,j] = generatedCards[rand];
				generatedCards.RemoveAt (rand);
			}
		}
	}

	// Draw the buttons
	void OnGUI() {
		GUI.skin = customSkin;

		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
		buildGrid ();
		GUILayout.EndArea ();

		if (TimerScript.timeOut) {
			buildGameOverBox ("Time Out!");
		}
		else if (win) {
			buildGameOverBox("You Win!");
		}

	}

	void buildGrid() {
		// continously updates and builds the buttons on the screen

		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		for (int i = 0; i < rows; i++) {
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			for (var j = 0; j < cols; j++) {
				string img;
				Card cardForButton = dealtCards [i, j];
				if (cardForButton.isFaceUp) {
					if (cardForButton.isMatched == true) {
						img = "img/blank";
					} else {
						img = "img/" + cardForButton.img;
					}
				} 
				else {
					img = "img/wrench";			
				}
				if (gameOver || cardForButton.isMatched) {
					GUI.enabled = false;
				}
				if (GUILayout.Button ((Resources.Load (img) as Texture), GUILayout.MaxWidth (cardW))) {
					if (canClick) {
						// Thing for the yeild
						StartCoroutine(turnCard(cardForButton));
					}
				}
				GUI.enabled = true;
			}
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
		}
		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();
	}

	IEnumerator turnCard (Card cardToBeTurned) {
		// Logic for turning cards over, pausing the clicking, checking pairs
		if (flippedCards.IndexOf (cardToBeTurned) < 0) {
			cardToBeTurned.isFaceUp = true;
			flippedCards.Add (cardToBeTurned);
			Sounds.flip.Play ();

			if (flippedCards.Count >= 2) {
				canClick = false;
				// wait
				yield return new WaitForSeconds (1);
				if (flippedCards [0].pairId == flippedCards [1].pairId) {
					Sounds.correct.Play ();
					flippedCards [0].isMatched = true;
					flippedCards [1].isMatched = true;
					totalMatches++;
					if (totalMatches >= matchesToWin) {
						win = true;
						Sounds.jazzChord.Play ();
					}
				} else {
					Sounds.incorrect.Play ();
					foreach (var card in flippedCards) {
						card.isFaceUp = false;
					}
				}
				flippedCards.Clear ();
				canClick = true;
			}
		}
	}

	void buildDeck () {
		// builds the deck of card to use (in sorted order) into generatedCards
		int numRobots = 4;
		int pairId = 0;
		for (int i = 1; i <= numRobots; i++) {
			var parts = new List<string> {"Head", "Arm", "Leg"};
			for (int j = 0; j < 2; j++) {
				// only 2 times, becase we only want 2 pairs per robot
				int rand = Random.Range (0, parts.Count);
				// add part card
				generatedCards.Add(new Card("robot"+i+parts[rand], pairId));
				// and corrresponding robot card
				generatedCards.Add(new Card("robot"+i+"Missing"+parts[rand], pairId));
				parts.RemoveAt (rand);
				pairId++;
			}
		}
	}

	void buildGameOverBox (string winOrLose) {
		gameOver = true;

		float winBoxH = 250;
		float winBoxW = 300;

		GUI.BeginGroup (new Rect (Screen.width / 2 - winBoxW / 2, Screen.height / 2 - winBoxH / 2, winBoxW, winBoxH));
		GUI.Box (new Rect (0, 0, winBoxW, winBoxH), winOrLose);
		if (GUI.Button (new Rect(100, 200, 100, 40), "Play again?")) {
			Sounds.click.Play ();
			SceneManager.LoadScene ("TitleScreen");
		}
		GUI.EndGroup ();

	}

	// Card class
	public class Card: System.Object  {
		public bool isFaceUp = false;
		public bool isMatched = false;
		public int pairId;
		public string img;
		public Card(string imgTarget, int IdNumber) {
			img = imgTarget;
			pairId = IdNumber;
		}
	}
}
