using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour {
	public GameObject obj;

	public Text myText;
	 

	void Start(){

		myText = GetComponent<Text> ();
	}
	void Update(){
		//myText.text = obj.GetComponent<player> ().score.ToString();
	

	}

}
