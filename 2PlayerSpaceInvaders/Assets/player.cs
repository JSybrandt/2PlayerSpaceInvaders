using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public GameObject bullet;
	public Sprite bolt;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			GameObject g = (GameObject) Instantiate(bullet,this.transform.position,Quaternion.identity);
			g.GetComponent<Rigidbody2D>().velocity = Vector3.up*3;
			g.GetComponent<SpriteRenderer>().sprite=bolt;
			g.tag="PlayerBolt";
		}
	}
}
