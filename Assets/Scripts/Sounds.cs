using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

	public static AudioSource correct;
	public static AudioSource flip;
	public static AudioSource incorrect;
	public static AudioSource jazzChord;
	public static AudioSource click;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		correct = GameObject.Find ("Correct").GetComponent<AudioSource> ();
		flip = GameObject.Find ("Flip").GetComponent<AudioSource> ();
		incorrect = GameObject.Find ("Incorrect").GetComponent<AudioSource> ();
		jazzChord = GameObject.Find ("JazzChord").GetComponent<AudioSource> ();
		click = GameObject.Find ("Click").GetComponent<AudioSource> ();
	}
}
