using UnityEngine;
using System.Collections;

public class gameLogic : MonoBehaviour {
	public int enemycount;
	public GameObject endCan;

	// Use this for initialization
	void Start () {
		endCan.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.connections.Length==0)return;
		bool foundActive = false;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
			if (g.activeSelf)
				foundActive = true;
		if (!foundActive) {
			endCan.SetActive(true);
			Time.timeScale = 0;
		}

	
	}
}
