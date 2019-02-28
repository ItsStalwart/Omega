using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDShowHide : MonoBehaviour {

	public GameObject whatTo;

	void show(){
		whatTo.SetActive (true);
	}

	void hide(){
		whatTo.SetActive (false);
	}

	public void btnClick(){
		if (whatTo.activeSelf) {
			hide ();
		} else {
			show ();
		}
	}

}
