using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public int score;
	public int speed;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown () ) {

			GameObject a = (GameObject)Instantiate (bullet, pos, q);

			
		}

		if (Input.GetButtonDown () ) {
			

			
			
		}


	
	}
}
