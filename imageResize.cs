using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class imageResize : MonoBehaviour {


	public Image pic;
	public float ratio=0f;
	public float baseWidth=0;
	public float baseXPos =0;
	// Use this for initialization
	void Awake(){
		baseXPos = pic.rectTransform.localPosition.x;
		baseWidth = pic.rectTransform.rect.width;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (pic.transform.localPosition);
		pic.rectTransform.localPosition = new Vector3 (baseXPos + (ratio * 0.5208f), -327f,0);
		pic.rectTransform.sizeDelta = new Vector2(baseWidth+(ratio*1), 100f);

	}
}
