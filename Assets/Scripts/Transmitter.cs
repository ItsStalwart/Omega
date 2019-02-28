using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : GridUnit {

	public int type;
	public string inputDirection;

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
		transmitSignal ();
	}

	void Start(){
		this.pieceType = ("Transmitter");
		if (type == 0) { 
			if (rotation == 0) {
				inputDirection = "left";
			}if (rotation == 1) {
				inputDirection = "up";
			}if (rotation == 2) {
				inputDirection = "right";
			}if (rotation == 3) {
				inputDirection = "down";
			}
		}if (type == 1) { 
			if (rotation == 0) {
				inputDirection = "left";
			}if (rotation == 1) {
				inputDirection = "up";
			}if (rotation == 2) {
				inputDirection = "right";
			}if (rotation == 3) {
				inputDirection = "down";
			}
		}if (type == 2) { 
			if (rotation == 0) {
				inputDirection = "down";
			}if (rotation == 1) {
				inputDirection = "left";
			}if (rotation == 2) {
				inputDirection = "up";
			}if (rotation == 3) {
				inputDirection = "right";
			}
		}if (type == 3) { 
			if (rotation == 0) {
				inputDirection = "left";
			}if (rotation == 1) {
				inputDirection = "up";
			}if (rotation == 2) {
				inputDirection = "right";
			}if (rotation == 3) {
				inputDirection = "down";
			}
		}if (type == 4) { 
			if (rotation == 0) {
				inputDirection = "left";
			}if (rotation == 1) {
				inputDirection = "up";
			}if (rotation == 2) {
				inputDirection = "right";
			}if (rotation == 3) {
				inputDirection = "down";
			}
		}if (type == 5) { 
			if (rotation == 0) {
				inputDirection = "down";
			}if (rotation == 1) {
				inputDirection = "left";
			}if (rotation == 2) {
				inputDirection = "up";
			}if (rotation == 3) {
				inputDirection = "right";
			}
		}if (type == 6) { 
			if (rotation == 0) {
				inputDirection = "up";
			}if (rotation == 1) {
				inputDirection = "right";
			}if (rotation == 2) {
				inputDirection = "down";
			}if (rotation == 3) {
				inputDirection = "left";
			}
		}
	}

	public void transmitSignal(){
		
		string outputDirection = "";

		if (type == 0) { 
			if (rotation == 0) {
				outputDirection = "right";
			}if (rotation == 1) {
				outputDirection = "down";
			}if (rotation == 2) {
				outputDirection = "left";
			}if (rotation == 3) {
				outputDirection = "up";
			}
		}if (type == 1) { 
			if (rotation == 0) {
				outputDirection = "down";
			}if (rotation == 1) {
				outputDirection = "left";
			}if (rotation == 2) {
				outputDirection = "up";
			}if (rotation == 3) {
				outputDirection = "right";
			}		
		}if (type == 2) { 
			if (rotation == 0) {
				outputDirection = "left";
			}if (rotation == 1) {
				outputDirection = "up";
			}if (rotation == 2) {
				outputDirection = "right";
			}if (rotation == 3) {
				outputDirection = "down";
			}
		}if (type == 3) { 
			if (rotation == 0) {
				outputDirection = "right, up, down";
			}if (rotation == 1) {
				outputDirection = "down, left, right";
			}if (rotation == 2) {
				outputDirection = "left, up, down";
			}if (rotation == 3) {
				outputDirection = "up, left, right";
			}
		}if (type == 4) { 
			if (rotation == 0) {
				outputDirection = "up, down";
			}if (rotation == 1) {
				outputDirection = "left, right";
			}if (rotation == 2) {
				outputDirection = "up, down";
			}if (rotation == 3) {
				outputDirection = "left, right";
			}
		}if (type == 5) { 
			if (rotation == 0) {
				outputDirection = "left, up";
			}if (rotation == 1) {
				outputDirection = "up, right";
			}if (rotation == 2) {
				outputDirection = "right, down";
			}if (rotation == 3) {
				outputDirection = "down, left";
			}
		}if (type == 6) { 
			if (rotation == 0) {
				outputDirection = "down, left";
			}if (rotation == 1) {
				outputDirection = "left, up";
			}if (rotation == 2) {
				outputDirection = "up, right";
			}if (rotation == 3) {
				outputDirection = "right, down";
			}
		}
		output = new Signal (input.type, input.strength, outputDirection);
	}
	public override void highlight(){
		if (source == null) {
			GameControl.singleton.updateMonitor ("Transmitter not currently connected!");
		} else {
			GameControl.singleton.updateMonitor ("Transmitting " + input.strength + "W of energy type " + input.type +  "!");
		}
	}
	public override void lightsout(){
	}
}
