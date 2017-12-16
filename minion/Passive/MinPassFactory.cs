using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinPassFactory {

	minionPassive passive = new minionPassive ();

	public minionPassive getPassive(string id){


		if (id == "test") {
		
			passive = new TestMinionPassive ();
		
		}

		if (id == "sadWar") {

			passive = new mpSadWarrior ();

		}

		passive.passiveName = id;
		return passive;
	
	}
}
