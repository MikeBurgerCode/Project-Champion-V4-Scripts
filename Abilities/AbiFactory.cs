using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbiFactory  {

	//r=0,u=1,y=2,p=3,g=4,w=5


	public ChampAbility abi = new ChampAbility();


	public ChampAbility getChampAbility(string id){

		if (id == "abiTest") {

			abi = new TestAbility ();

		}

		if (id == "aY0") {

			abi = new abiHeal ();

		}

		if (id == "aR0") {

			abi = new abiReinforce ();

		}

		if (id == "aU0") {

			abi = new abiScry ();

		}

		if (id == "aG0") {

			abi = new abiSummonPlant ();

		}

		if (id == "aP0") {

			abi = new abiLifeDrain ();

		}

		if (id == "aW0") {

			abi = new abiFort ();

		}

		abi.id = id;
		return abi;
	}

}
