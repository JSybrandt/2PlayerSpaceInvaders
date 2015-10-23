using UnityEngine;
using System.Collections;

public class boltScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Wall") {
			gameObject.SetActive (false);
		}
	}
}
