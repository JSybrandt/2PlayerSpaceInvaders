using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public AudioSource shoot;
	public int score = 0;
	public int speed = 5;
	public GameObject bullet;
	public Sprite bolt;
	public string inputmov = "Horizontal";
	public string boltType = "PlayerBolt1";
	public string fire = "Fire1";
	
	
	// Use this for initialization
	void Start () {
		 shoot = GetComponent<AudioSource> ();
			
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.connections.Length==0)return;
		
		//if (Input.GetButtonDown ("Horizontal") ) {
			float ha = Input.GetAxis (inputmov) * speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, 0);
			//transform.position = new Vector3(transform.position.x + ha *speed *Time.deltaTime,transform.position.y,transform.position.z);
		
		

		
		
		if (Input.GetButtonDown (fire)) {
			GameObject g = (GameObject) Network.Instantiate(bullet,this.transform.position,Quaternion.identity,0);
			g.GetComponent<Rigidbody2D>().velocity = Vector3.up*3;
			g.GetComponent<SpriteRenderer>().sprite=bolt;
			g.tag=boltType;
			shoot.Play();
		}
		
	}
	void OnTriggerEnter2D (Collider2D hit) {
		if (hit.gameObject.tag == "EnemyBolt") {
			gameObject.SetActive(false);
		}
	}
}