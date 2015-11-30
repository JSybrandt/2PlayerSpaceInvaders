using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public Sprite bolt;
	public GameObject bullet;
	public GameObject player1;
	public GameObject player2;
	public GameObject p1score;
	public int p2score;
	void Start(){
		 player1 = GameObject.FindGameObjectWithTag ("player1");
		 player2 = GameObject.FindGameObjectWithTag ("player2");
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PlayerBolt1") {
			gameObject.SetActive (false);
			other.gameObject.SetActive (false);
			player1.GetComponent<player>().score += 100;


		} else if (other.gameObject.tag == "PlayerBolt2") {
			gameObject.SetActive (false);
			other.gameObject.SetActive (false);
			player2.GetComponent<player>().score += 100;
		}





	}
	public void act(){
		if (Random.Range (0.0f, 1.0f) >= 0.97) {
			GameObject g = (GameObject) Instantiate(bullet,this.transform.position,Quaternion.identity);
			g.GetComponent<Rigidbody2D>().velocity = Vector3.down*3;
			g.GetComponent<SpriteRenderer>().sprite=bolt;
			g.tag="EnemyBolt";
		}
	}
}
