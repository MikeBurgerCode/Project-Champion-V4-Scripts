using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenFactory  {

	Awaken a = new Awaken ();

	public Awaken getAwaken(string id){
	

		if (id == "test") {
		
			a = new TestAwaken ();
			a.type="target";

		
		}

		if (id == "graveRob") {

			a = new mpGraveRob ();
			a.type="play";


		}
	
		return a;
	}

}
