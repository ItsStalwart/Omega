using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceRotation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	public bool enableRotation;
	public bool selected;
	public int rotation;
	public GameObject piece;
	private GameObject lastClicked;

	public void OnPointerEnter(PointerEventData eventData){
		enableRotation = true;
	}

	public void OnPointerExit(PointerEventData eventData){
		enableRotation = false;
	}

	void Update(){
		if (enableRotation) {
			rotatePiece ();
		}
	}

	void rotatePiece(){
		if (Input.GetAxis("Mouse ScrollWheel")> 0) // forward
		{
			piece.transform.Rotate (0, 0, 90);
			if (rotation > 0){
				rotation--;
			}else{
				rotation = 3;
			}
			if(lastClicked == this.gameObject)
			GameControl.singleton.selectedUnitRotation = rotation;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			piece.transform.Rotate (0, 0, -90);
			if (rotation < 3) {
				rotation++;
			} else {
				rotation = 0;
			}
			if(lastClicked == this.gameObject)
			GameControl.singleton.selectedUnitRotation = rotation;
		}
		if (Input.GetMouseButtonDown(0)) {
			GameControl.singleton.selectedUnitRotation = rotation;
			lastClicked = this.gameObject;
		}
	}

}
