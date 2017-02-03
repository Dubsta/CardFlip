using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadTitle : MonoBehaviour {
	public void loadTitle () {
		Sounds.click.Play ();
		SceneManager.LoadScene ("TitleScreen");
	}
}