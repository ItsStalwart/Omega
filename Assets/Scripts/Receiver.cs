using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : GridUnit {

	public string inputDirection;
	public int inputType;
	public int inputLowerLimit;
	public int inputUpperLimit;
	public bool cleared;



	void FixedUpdate(){
		if (inputDirection.Contains("left") && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "") {
			source = GameControl.grid [x - 1, y];
			receiveSignal ();
		}if (inputDirection.Contains("right") && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "") {
			source = GameControl.grid [x + 1, y];
			receiveSignal();
		}if (inputDirection.Contains("up") && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "") {
			source = GameControl.grid [x, y + 1];
			receiveSignal();
		}if (inputDirection.Contains("down") && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "") {
			source = GameControl.grid [x, y - 1];
			receiveSignal();
		}
		if(input != null)
		clearCheck ();
	}

	void Start(){
		pieceType = "Receiver";
		this.output = null;
	}

	void clearCheck(){
		if (input.strength <= inputUpperLimit && input.strength >= inputLowerLimit && input.type == inputType) {
			cleared = true;
			GameControl.singleton.winCheck ();
		} else {
			cleared = false;
		}
	}

	public override void highlight(){
		if (!cleared)
			GameControl.singleton.updateMonitor ("This receiver requires an input between " + inputLowerLimit + "W and " + inputUpperLimit + "W of energy type " + inputType + " from its " + inputDirection + " to become active!");
		else
			GameControl.singleton.updateMonitor ("This receiver is powered and fully active!");
	}
	public override void lightsout(){
	}
	public override void unitSpawn(){
	}
}
