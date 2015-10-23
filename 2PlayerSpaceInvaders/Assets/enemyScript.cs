using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	public Sprite bolt;
	public GameObject bullet;
	public

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PlayerBolt1") {
			gameObject.SetActive (false);
			other.gameObject.SetActive (false);
		} else if (other.gameObject.tag == "PlayerBolt2") {
			gameObject.SetActive (false);
			other.gameObject.SetActive (false);

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
