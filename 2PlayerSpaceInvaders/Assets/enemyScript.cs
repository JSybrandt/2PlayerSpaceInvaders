using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public GameObject bullet;
	private NetworkView netView;

	bool stuff = true;

	void Start(){
		netView = GetComponent<NetworkView> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!stuff)
			return;
		if (other.gameObject.tag == "PlayerBolt1") {

			Debug.Log("deleting bolt");
			other.gameObject.GetComponent<NetworkView>().RPC("deactivate",RPCMode.AllBuffered,null);
			Debug.Log("deleting enemy");
			netView.RPC("deactivate",RPCMode.AllBuffered,null);

			//other.gameObject.GetComponent<boltScript>().kill();

		} 

	}
	public void act(){
		if(Network.connections.Length==0)return;
		if (Random.Range (0.0f, 1.0f) >= 0.97) {
			//GameObject g = (GameObject) Instantiate(bullet,this.transform.position,Quaternion.identity);
			//g.GetComponent<boltScript>().shoot();
		}
	}
	[RPC]
	public void deactivate(){
		stuff = false;
		transform.localScale = new Vector3 (0, 0, 0);
		//Network.Destroy (netView.viewID);
		//NetworkView.Destroy (this);
		//gameObject.SetActive(false);
	}
}
