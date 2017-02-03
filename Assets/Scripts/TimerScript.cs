using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	[SerializeField]
	private Text clockText;
	[SerializeField]
	private float currentTimer = 15.00f;
	private float timerStart;
	private bool timePaused = false;

	public static bool timeOut;

	[SerializeField]
	private Image timerBar;

	// Use this for initialization after scene is enabled
	void Start () {
		timeOut = false;
		timerStart = currentTimer;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameScript.win) {
			timePaused = true;
		}

		if (!timePaused) {
			currentTimer -= Time.deltaTime;
		}

		if (currentTimer <= 0) {
			timePaused = true;
			timeOut = true;
		}

		drawClock ();
	}

	void drawClock () {
		clockText.text = string.Format ("{0}:{1:00}", (int)currentTimer / 60, (int)currentTimer % 60);
		timerBar.fillAmount = currentTimer / timerStart;
	}
}
