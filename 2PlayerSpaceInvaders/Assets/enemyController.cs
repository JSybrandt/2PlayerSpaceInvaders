using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	public GameObject enemyPrefab; //better get set
	
	// Use this for initialization
	void Start () {
	
		for (float i = 3; i < 6; i+=0.5f) {
		
			for(float j = -4; j < 3; j+=1.0f){
			
				GameObject e = (GameObject) Instantiate(enemyPrefab, new Vector3(j,i,0), Quaternion.identity);

			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
