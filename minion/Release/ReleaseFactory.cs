using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseFactory {

	Release rl = new Release();

	public Release getRelease(string id){

		if (id == "test") {
		
			rl = new TestRelease ();
		
		}

		return rl;
	}
}
