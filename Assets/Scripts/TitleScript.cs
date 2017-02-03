using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour {

	[SerializeField]
	private GUISkin customSkin;

	void OnGUI() {
		GUI.skin = customSkin;

		//Button positioning
		int buttonHeight = 50;
		int buttonWidth = 100;
		int halfWidth = Screen.width / 2;
		int targetHeight = (int) (Screen.height * 0.8);
		int xPos = halfWidth - buttonWidth / 2;
		int yPos = targetHeight - buttonHeight / 2;

		if (GUI.Button (new Rect(xPos,yPos,buttonWidth,buttonHeight), "Start")) {
			Sounds.click.Play ();
			SceneManager.LoadScene ("GamePlay");
		}
		if (GUI.Button (new Rect(xPos,yPos + 60,buttonWidth,buttonHeight), "Quit")) {
			Sounds.click.Play ();
			Application.Quit ();
		}
	}
}
