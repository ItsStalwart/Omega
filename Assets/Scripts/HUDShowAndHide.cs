using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDShowAndHide : MonoBehaviour {

	public GameObject whatTo;
	public GameObject whatNotTo;

	void show(){
		whatTo.SetActive (true);
		whatNotTo.SetActive (false);
	}

	void hide(){
		whatTo.SetActive (false);
		whatNotTo.SetActive (true);
	}

	public void btnClick(){
		if (whatTo.activeSelf) {
			hide ();
		} else {
			show ();
		}
	}
}
