using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {

	public SpellData spell;
	public minionData targetMinion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (spell != null) {
			if (spell.targeting) {
				GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
				if (Input.GetMouseButtonDown (0)) {
					Vector3 screenPoint = Input.mousePosition;
					screenPoint.z = -10;

					Ray ray = Camera.main.ScreenPointToRay (screenPoint);
					//Debug.DrawRay(
					RaycastHit hit;

					if (Physics.Raycast (ray, out hit, 100)) {
						//spell.

						if (spell.isVaildTarget (hit.transform.gameObject)) {

							Debug.Log ("vaild target");
							spell.castOnTarget(hit.transform.gameObject);
						}
					}
				}

			}
		}
	}
}
