using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellFactory {

	SpellData sp = new SpellData();

	public SpellData getSpell(string id){

		if (id == "spTest") {

			sp = new TestSpell ();
			sp.type="target";
		}
	
		if (id == "abiScry") {

			sp = new sAbiScry ();
			sp.type="play";
		}

		if (id == "sP0") {

			sp = new sRevenge ();
			sp.type="play";
		}

		if (id == "sB0") {

			sp = new sDisDraw ();
			sp.type="play";
		}

		if (id == "sR0") {

			sp = new spDisarm();
			sp.type="target";
		}
		return sp;
	}
}
