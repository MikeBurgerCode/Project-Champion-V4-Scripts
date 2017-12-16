using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorStatus : MonoBehaviour {

	public List<Texture2D> c;
	public string status;
	public int i;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//if(this.gameObject.GetComponent<ChampionV2>().photonView.isMine){
		if(status=="summoning"){Cursor.SetCursor (c[1], Vector2.zero, CursorMode.Auto);i=1;}
		if(status=="attacking"){Cursor.SetCursor (c[2], Vector2.zero, CursorMode.Auto);i = 2;}
		if(status=="target"){Cursor.SetCursor (c[3], new Vector2(c[3].width/2,c[3].height/2), CursorMode.Auto);i = 3;}
		if(status=="X"){Cursor.SetCursor (c[4], new Vector2(c[4].width/2,c[4].height/2), CursorMode.Auto);i = 4;}
		if(status==""){Cursor.SetCursor (c[0], Vector2.zero, CursorMode.Auto);i = 0;}
		//}
	}
}
