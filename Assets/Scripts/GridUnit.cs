using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour {

	public int x;
	public int y;
	public int rotation;

	public bool flowing;

	public Signal input;

	public Signal output;

	public GridUnit source;

	public string pieceType = null;

	void OnMouseEnter(){
		highlight ();
	}

	void OnMouseExit(){
		lightsout ();
	}

	public void receiveSignal(){
		input = source.output;
	}

	public void setSource(){
		if (input.direction.Contains("left") && GameControl.grid [x, y - 1].pieceType != null) {
			source = GameControl.grid [x - 1 , y];
		}if (input.direction.Contains("right") && GameControl.grid [x, y + 1].pieceType != null) {
			source = GameControl.grid [x + 1, y];
		}if (input.direction.Contains("up") && GameControl.grid [x + 1,y].pieceType != null) {
			source = GameControl.grid [x, y + 1];
		}if (input.direction.Contains("down") && GameControl.grid [x - 1, y].pieceType != null) {
			source = GameControl.grid [x, y - 1];
		}
	}

	public virtual void highlight(){
		this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	public virtual void lightsout(){
		this.gameObject.GetComponent<MeshRenderer> ().material.color = Color.black;
	}
	public virtual void unitSpawn(){
		GameControl.singleton.spawnSelectedUnit (x,y);
	}
	public void OnMouseDown(){
		unitSpawn ();
	}



}
