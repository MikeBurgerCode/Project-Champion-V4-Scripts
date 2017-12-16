using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AaManager : MonoBehaviour {

	public minionData mData;
	public Active actAbility;
	public Awaken awaken;
	public string type="";

	void Update () {
	
		if (mData != null) {
		
			if (type == "awaken") {
			
			
			}


			if (type == "active") {
				if (actAbility.targeting) {
					GameObject.Find ("cursorStatus").GetComponent<cursorStatus> ().status = "target";
					if (Input.GetMouseButtonDown (0)) {
						Vector3 screenPoint = Input.mousePosition;
						screenPoint.z = -10;

						Ray ray = Camera.main.ScreenPointToRay (screenPoint);
						//Debug.DrawRay(
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit, 100)) {
							//spell.

							if (actAbility.isVaildTarget (hit.transform.gameObject)) {


								actAbility.castOnTarget (hit.transform.gameObject);
								//Debug.Log ("vaild target");
								//spell.castOnTarget(hit.transform.gameObject);
							}
						}
					}

				}
			}
		}

			}


	public void cardPick(cardData cData){


		if (type == "active") {

			actAbility.cardPick (cData,mData);

		}

		if (type == "awaken") {

			awaken.cardPick (cData,mData);

		}

	}

	public void clearData(){
		mData = null;
		actAbility = null;
		type="";
	}

		
		
}
