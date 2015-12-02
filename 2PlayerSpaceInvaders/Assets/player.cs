using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public AudioSource shoot;
	public int score = 0;
	public int speed = 5;
	public GameObject bullet;
	public string inputmov = "Horizontal1";
	public string boltType = "PlayerBolt1";
	public string fire = "Fire1";
	private NetworkView netView;

	private bool stuff = true;

	// Use this for initialization
	void Start () {
		 shoot = GetComponent<AudioSource> ();
		 netView = GetComponent<NetworkView> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!stuff)
			return;
		if(Network.connections.Length==0)return;
		if (!netView.isMine)
			return;
		//if (Input.GetButtonDown ("Horizontal") ) {
			float ha = Input.GetAxis (inputmov) * speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, 0);
			//transform.position = new Vector3(transform.position.x + ha *speed *Time.deltaTime,transform.position.y,transform.position.z);
		
	
		
		
		if (Input.GetButtonDown (fire)) {
			GameObject g = (GameObject) Network.Instantiate(bullet,this.transform.position,Quaternion.identity,0);
			g.GetComponent<NetworkView>().RPC("launchUp",RPCMode.AllBuffered,null);
			shoot.Play();
		}
		
	}

	[RPC]
	void deactivate(){
		stuff = false;
		transform.localScale = new Vector3 (0, 0, 0);
		//Network.Destroy (netView.viewID);
		//NetworkView.Destroy (this);
		//gameObject.SetActive(false);
	}

	void OnTriggerEnter2D (Collider2D hit) {
		if (!stuff)
			return;
		if (hit.gameObject.tag == "EnemyBolt") {
			netView.RPC("deactivate",RPCMode.AllBuffered,null);
			//gameObject.SetActive(false);
		}
	}
}