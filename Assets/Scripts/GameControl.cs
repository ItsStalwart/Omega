using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public static GameControl singleton;
	public int gridSize;

	private Transform boardHolder;
	public GameObject generator;
	public GameObject receiver;
	public GameObject[] transmitter;
	public GameObject[] converter;
	public GameObject[] ampmixer;
	public GameObject boardUnit;
	public GameObject victoryStuff;
	public GameObject transStuff;
	public GameObject convStuff;
	public GameObject ampStuff;

	private GameObject selectedUnit;
	public int selectedUnitRotation;

	public Text textInput;
	public Text textOutput;

	public static GridUnit[,] grid;
	public int [,] puzzleMatrix;

	public List<Receiver> clearList;

	public GameObject tutStuff1;
	public GameObject tutStuff2;
	public GameObject tutStuff3;

	void Awake(){
		if (singleton == null) {
			singleton = this;
		}
		grid = new GridUnit[gridSize, gridSize];
		fetchPuzzle ();
		spawnBoard ();
		if (PuzzleControl.singleton.selectedPuzzle == 0) {
			grid [8, 4].output = new Signal (1, 150, "right");

			grid [0, 4].gameObject.GetComponent<Receiver> ().inputDirection = "right";
			grid [0, 4].gameObject.GetComponent<Receiver> ().inputType = 1;
			grid [0, 4].gameObject.GetComponent<Receiver> ().inputLowerLimit = 100;
			grid [0, 4].gameObject.GetComponent<Receiver> ().inputUpperLimit = 200;
			clearList.Add (grid [0, 4].gameObject.GetComponent<Receiver> ());
			tutStuff1.SetActive (true);
			convStuff.SetActive (false);
			ampStuff.SetActive (false);
		}

		if (PuzzleControl.singleton.selectedPuzzle == 1) {
			grid [9, 4].output = new Signal (1, 200, "up");
			grid [9, 4].gameObject.transform.Rotate (0, 0, 90);

			grid [1, 0].gameObject.GetComponent<Receiver> ().inputDirection = "up";
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputType = 1;
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputLowerLimit = 700;
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputUpperLimit = 1000;
			grid [1, 0].gameObject.transform.Rotate (0, 0, 90);
			clearList.Add (grid [1, 0].gameObject.GetComponent<Receiver> ());
			tutStuff2.SetActive (true);
			convStuff.SetActive (false);
		}
		if (PuzzleControl.singleton.selectedPuzzle == 2) {
			grid [9, 8].output = new Signal (1, 400, "down");
			grid [9, 8].gameObject.transform.Rotate (0, 0, -90);

			grid [1, 4].gameObject.GetComponent<Receiver> ().inputDirection = "left";
			grid [1, 4].gameObject.GetComponent<Receiver> ().inputType = 2;
			grid [1, 4].gameObject.GetComponent<Receiver> ().inputLowerLimit = 100;
			grid [1, 4].gameObject.GetComponent<Receiver> ().inputUpperLimit = 200;
			grid [1, 4].gameObject.transform.Rotate (0, 0, 180);
			clearList.Add (grid [1, 4].gameObject.GetComponent<Receiver> ());
			tutStuff3.SetActive (true);
			ampStuff.SetActive (false);
		}

		if (PuzzleControl.singleton.selectedPuzzle == 3) {
			grid [0, 2].output = new Signal (1, 100, "right");

			grid [5, 5].output = new Signal (2, 150, "down");
			grid [5, 5].gameObject.transform.Rotate (0, 0, -90);

			grid [2, 8].gameObject.GetComponent<Receiver> ().inputDirection = "right";
			grid [2, 8].gameObject.GetComponent<Receiver> ().inputType = 1;
			grid [2, 8].gameObject.GetComponent<Receiver> ().inputLowerLimit = 225;
			grid [2, 8].gameObject.GetComponent<Receiver> ().inputUpperLimit = 350;
			clearList.Add (grid [2, 8].gameObject.GetComponent<Receiver> ());

			grid [5, 0].gameObject.GetComponent<Receiver> ().inputDirection = "right";
			grid [5, 0].gameObject.GetComponent<Receiver> ().inputType = 3;
			grid [5, 0].gameObject.GetComponent<Receiver> ().inputLowerLimit = 75;
			grid [5, 0].gameObject.GetComponent<Receiver> ().inputUpperLimit = 125;
			clearList.Add (grid [5, 0].gameObject.GetComponent<Receiver> ());
		}

		if (PuzzleControl.singleton.selectedPuzzle == 4) {
			grid [0, 9].output = new Signal (1, 200, "right");

			grid [1, 0].gameObject.GetComponent<Receiver> ().inputDirection = "left";
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputType = 1;
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputLowerLimit = 150;
			grid [1, 0].gameObject.GetComponent<Receiver> ().inputUpperLimit = 300;
			grid [1, 0].gameObject.transform.Rotate (0, 0, 180);
			clearList.Add (grid [1, 0].gameObject.GetComponent<Receiver> ());

			grid [9, 4].gameObject.GetComponent<Receiver> ().inputDirection = "down";
			grid [9, 4].gameObject.GetComponent<Receiver> ().inputType = 1;
			grid [9, 4].gameObject.GetComponent<Receiver> ().inputLowerLimit = 200;
			grid [9, 4].gameObject.GetComponent<Receiver> ().inputUpperLimit = 250;
			grid [9, 4].gameObject.transform.Rotate (0, 0, -90);
			clearList.Add (grid [9, 4].gameObject.GetComponent<Receiver> ());
		}
	}

	public void winCheck(){
		int allCleared = 0;
		for (int i = 0; i < clearList.Count; i++) {
			if (clearList [i].cleared) {
				allCleared++;
			} else {
				allCleared--;
			}
		}
		if (allCleared == clearList.Count) {
			StartCoroutine ("winRoutine");
		}
	}

	public IEnumerator winRoutine(){
		victoryStuff.SetActive (true);
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("menu");
	}

	void spawnBoard(){
		boardHolder = new GameObject ("Board").transform;

		for (int i = 0; i < gridSize; i++) {
			for (int j = 0; j < gridSize; j++) {
				if (puzzleMatrix [i, j] == 0) {
					GameObject boardTile = Instantiate (generator, new Vector2 (i, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
				}
				if (puzzleMatrix [i, j] == 1) {
					GameObject boardTile = Instantiate (receiver, new Vector2 (i, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
				}
				if (puzzleMatrix [i, j] == 5) {
					GameObject boardTile = Instantiate (boardUnit, new Vector2 (i, j), Quaternion.identity) as GameObject;
					boardTile.transform.SetParent (boardHolder);
					boardTile.GetComponent<GridUnit> ().x = i;
					boardTile.GetComponent<GridUnit> ().y = j;
					grid [i, j] = boardTile.GetComponent<GridUnit> ();
				}
			}
		}	
	}

	void fetchPuzzle(){
		if (PuzzleControl.singleton.selectedPuzzle == 0) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
			};
		}
		if (PuzzleControl.singleton.selectedPuzzle == 1) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 1, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 0, 5, 5, 5, 5, 5 }
			};
		}
		if (PuzzleControl.singleton.selectedPuzzle == 2) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 0, 5 }
			};
		}
		if (PuzzleControl.singleton.selectedPuzzle == 3) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 0, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 1, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 1, 5, 5, 5, 5, 0, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
				{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }
			};
		}
		if (PuzzleControl.singleton.selectedPuzzle == 4) {
			puzzleMatrix = new int [10, 10] {{ 5, 5, 5, 5, 5, 5, 5, 5, 5, 0 },
											 { 1, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
											 { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
										 	 { 5, 5, 5, 5, 1, 5, 5, 5, 5, 5 }
			};
		}
							
	
	}
	public void spawnSelectedUnit(int px,int py){
		if (selectedUnit != null) {
			GameObject boardTile = Instantiate (selectedUnit, new Vector2 (px, py), Quaternion.identity) as GameObject;
			boardTile.transform.SetParent (boardHolder);
			boardTile.GetComponent<GridUnit> ().x = px;
			boardTile.GetComponent<GridUnit> ().y = py;
			boardTile.GetComponent<GridUnit> ().rotation = selectedUnitRotation;
			Destroy (grid [px, py].gameObject);
			grid [px, py] = boardTile.GetComponent<GridUnit> ();
			if (selectedUnitRotation == 1) {
				boardTile.transform.Rotate (0, 0, -90);
			}if (selectedUnitRotation == 2) {
				boardTile.transform.Rotate (0, 0, -180);
			}if (selectedUnitRotation == 3) {
				boardTile.transform.Rotate (0, 0, -270);
			}
			Debug.Log (selectedUnit + " spawned at " + px.ToString () + "," + py.ToString () + " at rotation " + selectedUnitRotation + "!");
		} else {
			Debug.Log ("This message should not show up");
		}
	}

	public void selectPiece(int selector){
		if (selector == 0) {
			selectedUnit = transmitter [0];
		}
		if (selector == 1) {
			selectedUnit = transmitter [1];
		}
		if (selector == 2) {
			selectedUnit = transmitter [2];	
		}
		if (selector == 3) {
			selectedUnit = transmitter [3];
		}
		if (selector == 4) {
			selectedUnit = transmitter [4];
		}
		if (selector == 5) {
			selectedUnit = transmitter [5];
		}
		if (selector == 6) {
			selectedUnit = transmitter [6];
		}
		if (selector == 7) {
			selectedUnit = converter [0];
		}
		if (selector == 8) {
			selectedUnit = converter [1];
		}
		if (selector == 9) {
			selectedUnit = ampmixer [0];
		}
		if (selector == 10) {
			selectedUnit = ampmixer [1];
		}
		if (selector == 11) {
			selectedUnit = ampmixer [2];
		}
		Debug.Log (selectedUnit);
	}

	public void updateMonitor(string inputText){
		textInput.text = inputText;
	}
}
