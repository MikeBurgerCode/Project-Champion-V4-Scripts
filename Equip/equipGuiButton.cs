using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class equipGuiButton : MonoBehaviour {

	public minionData mData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseOver(){
	
		if (Input.GetMouseButtonDown (0)) {
			GameObject instance = GameObject.Instantiate(Resources.Load("GraveListGui", typeof(GameObject))) as GameObject;
			instance.transform.parent = GameObject.Find ("Canvas").transform;
			instance.transform.localPosition = new Vector3 (0, 0, 0);

			foreach (var equip in mData.equipment) {
				instance.GetComponent<GuiCardList> ().addCard (equip.cData);
			}
		}
	}
}
