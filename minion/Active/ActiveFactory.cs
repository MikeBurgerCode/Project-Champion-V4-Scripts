using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFactory  {


	Active act = new Active();
	// Use this for initialization

	public Active GetActiveAbility(string id){
	
	
		if (id == "actTest") {
		
			act = new ActiveTest ();
		
		}


		if (id == "merch0") {

			act = new actElfMerch ();

		}

		if (id == "bazooka") {

			act = new actBazooka ();

		}

		act.text = new LoadData ().activeText (id);
		act.cost = new LoadData ().activeCost (id);
		return act;
	}
}
