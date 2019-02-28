using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleControl : MonoBehaviour {

	public static PuzzleControl singleton;

	public int selectedPuzzle;
	void Awake(){
		if (singleton == null) {
			singleton = this;
		} else {
			Destroy (this.gameObject);	
		}
		DontDestroyOnLoad (singleton);
	}

	public void selectPuzzle(int selection){
		selectedPuzzle = selection;
	}

	public void runGame(){
		SceneManager.LoadScene ("main");
	}
}
