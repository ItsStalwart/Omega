using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSelector : MonoBehaviour {
	public int selector;

	public void StartPuzzle(){
		PuzzleControl.singleton.selectPuzzle (selector);
		PuzzleControl.singleton.runGame ();
	}
}
