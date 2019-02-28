using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : GridUnit {

	public int type;
	public string inputDirection;
	public string outputDirection;

	public void convertSignal(){
		int outputStrength = 0;
		if (inputDirection.Contains("left") && (x-1 >= 0) && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "") {
			source = GameControl.grid [x - 1, y];
			receiveSignal ();
			outputStrength = input.strength / 2;
		}if (inputDirection.Contains("right") && (x+1 <= 9) && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "") {
			source = GameControl.grid [x + 1, y];
			receiveSignal();
			outputStrength = input.strength / 2;
		}if (inputDirection.Contains("up") && (y+1 <= 9) && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "") {
			source = GameControl.grid [x, y + 1];
			receiveSignal();
			outputStrength = input.strength / 2;
		}if (inputDirection.Contains("down") && (y-1 >= 0) && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "") {
			source = GameControl.grid [x, y - 1];
			receiveSignal();
			outputStrength = input.strength / 2;
		}
		if (outputStrength < 25)
			outputStrength = 0;
		output = new Signal (type, outputStrength, outputDirection);
	}

	void Start(){
		this.pieceType = ("Converter");
			if (rotation == 0) {
				inputDirection = "left";
				outputDirection = "right";
			}if (rotation == 1) {
				inputDirection = "up";
				outputDirection = "down";
			}if (rotation == 2) {
				inputDirection = "right";
				outputDirection = "left";
			}if (rotation == 3) {
				inputDirection = "down";
				outputDirection = "up";
			}
	}

	void FixedUpdate(){
		if (inputDirection.Contains("left") && (x-1 >= 0) && GameControl.grid [x - 1, y].pieceType != null && GameControl.grid [x - 1, y].pieceType != "") {
			if (GameControl.grid [x - 1, y].output != null && GameControl.grid [x - 1, y].output.direction.Contains ("right")) {
				source = GameControl.grid [x - 1, y];
				receiveSignal ();
			}
		}if (inputDirection.Contains("right") && (x+1 <= 9) && GameControl.grid [x + 1, y].pieceType != null && GameControl.grid [x + 1, y].pieceType != "") {
			if (GameControl.grid [x + 1, y].output != null && GameControl.grid [x + 1, y].output.direction.Contains ("left")) {
				source = GameControl.grid [x + 1, y];
				receiveSignal();
			}
		}if (inputDirection.Contains("up") && (y+1 <= 9) && GameControl.grid [x,y + 1].pieceType != null && GameControl.grid [x, y + 1].pieceType != "") {
			if (GameControl.grid [x, y + 1].output != null && GameControl.grid [x, y + 1].output.direction.Contains ("down")) {
				source = GameControl.grid [x, y + 1];
				receiveSignal();
			}
		}if (inputDirection.Contains("down") && (y-1 >= 0) && GameControl.grid [x, y - 1].pieceType != null && GameControl.grid [x, y - 1].pieceType != "") {
			if (GameControl.grid [x, y - 1].output != null && GameControl.grid [x, y - 1].output.direction.Contains ("up")) {
				source = GameControl.grid [x, y - 1];
				receiveSignal();
			}
		}
		if(input != null)
			convertSignal ();
	}
	public override void highlight(){
		if (source == null) {
			GameControl.singleton.updateMonitor ("Converter not currently connected!");
		} else {
			if (output.strength >= 25) {
				GameControl.singleton.updateMonitor ("Converting " + input.strength + "W of energy type " + input.type + " into " + output.strength + "W of energy type " + type + "!");
			} else {
				GameControl.singleton.updateMonitor ("Converter does not have sufficient energy to opperate!");
			}

		}
	}
	public override void lightsout(){
	}
	}
