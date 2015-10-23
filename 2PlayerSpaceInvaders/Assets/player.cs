using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	
	public int score = 0;
	public int speed = 5;
	public GameObject bullet;
	public Sprite bolt;
	public string inputmov = "Horizontal";
	public string boltType = "PlayerBolt1";
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		//if (Input.GetButtonDown ("Horizontal") ) {
			float ha = Input.GetAxis (inputmov) * speed;
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (ha, 0);
			//transform.position = new Vector3(transform.position.x + ha *speed *Time.deltaTime,transform.position.y,transform.position.z);
		
		

		
		
		if (Input.GetButtonDown ("Fire1")) {
			GameObject g = (GameObject) Instantiate(bullet,this.transform.position,Quaternion.identity);
			g.GetComponent<Rigidbody2D>().velocity = Vector3.up*3;
			g.GetComponent<SpriteRenderer>().sprite=bolt;
			g.tag=boltType;
		}
		
	}
	void OnTriggerEnter2D (Collider2D hit) {
		if (hit.gameObject.tag == "") {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		}
	}
}