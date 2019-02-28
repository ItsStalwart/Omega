using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal{

	public int type;
	public int strength;
	public string direction;

	public Signal(int type,int strength, string direction){
		this.type = type;
		this.strength = strength;
		this.direction = direction;
	}

	public void signalLog(){
		Debug.Log (type);
		Debug.Log (strength);
		Debug.Log (direction);
	}

}
