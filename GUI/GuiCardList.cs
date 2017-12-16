using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiCardList : MonoBehaviour {

	public Transform content;
	public GameObject guiCard;

	public List<cardData> cards;
	// Use this for initialization
	void Start () {

		//GameObject cg = Instantiate (guiCard);
		//cg.transform.SetParent (content);
		//b.onClick.AddListener(

	}
	
	// Update is called once per frame
	void Update () {
	
		content.Find ("Top").SetAsFirstSibling ();
		content.Find ("Bottom").SetAsLastSibling ();

		for (int i = 1; i < content.childCount - 1; i++) {
		
			content.GetChild (i).gameObject.GetComponent<GuiCardData> ().cData = cards [i-1];
		
		}
	

	}

	public void addCard(cardData cData){
		//,UnityEngine.Events.UnityAction call

		cards.Add (cData);
		GameObject cg = Instantiate (guiCard);
		cg.transform.SetParent (content);

	//	if (call != null) {
		//	cg.transform.Find ("Button").gameObject.GetComponent<Button> ().onClick.AddListener (call);
	//	}
	}

	public void close(){

		Destroy (this.gameObject);

	}


}
