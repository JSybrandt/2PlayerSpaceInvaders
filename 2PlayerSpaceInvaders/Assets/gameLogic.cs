using UnityEngine;
using System.Collections;

public class gameLogic : MonoBehaviour {
	public int enemycount;
	public GameObject player1;
	public GameObject player2;
	public GameObject endCan;

	// Use this for initialization
	void Start () {
		endCan.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.connections.Length==0)return;
		if (!player1.activeSelf && !player2.activeSelf) {
			endCan.SetActive(true);
			Time.timeScale = 0;
		}

	
	}
}
