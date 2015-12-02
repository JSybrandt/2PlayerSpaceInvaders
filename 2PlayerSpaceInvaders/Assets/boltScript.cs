using UnityEngine;
using System.Collections;

public class boltScript : MonoBehaviour {

	public NetworkView netView;

	bool stuff = true;

	void OnTriggerEnter2D(Collider2D other){
		if (!stuff)
			return;
		if (other.gameObject.tag == "Wall") {
			transform.localScale = new Vector3 (0, 0, 0);
		}
	}

	[RPC]
	void deactivate(){
		stuff = false;
		transform.localScale = new Vector3 (0, 0, 0);
		//Network.Destroy (netView.viewID);
		//NetworkView.Destroy (this);
		//gameObject.SetActive (false);
	}

	[RPC]
	void launch(){
		GetComponent<Rigidbody2D> ().velocity = Vector3.down*3;
		gameObject.tag="EnemyBolt";
	}
	[RPC]
	void launchUp(){
		GetComponent<Rigidbody2D> ().velocity = Vector3.up*3;
		gameObject.tag="PlayerBolt1";
	}

	public void shoot(){
		netView.RPC ("launch", RPCMode.AllBuffered, null);
	}

}
