using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	public Sprite e2, e3;
	public GameObject enemyPrefab; //better get set
	private ArrayList enemies = new ArrayList();
	private float timer =0;
	private const float MAX_STEP = 0.25f;
	private const float MIN_STEP = 0f;
	//we are going to intentionally make this look like crap

	private float lBound,rBound;
	private const float MIN_DIST_TO_WALL = 1.0f;

	private const float MIN_SPEED = 1.0f;
	private const float MAX_SPEED = 4.5f;

	private int startingCount;
	private int currentCount;

	bool movingLeft = false;
	bool moveDownNext = false;

	// Use this for initialization
	void Start () {
	
		lBound = GameObject.Find ("LWall").transform.position.x;
		rBound = GameObject.Find ("RWall").transform.position.x;

		for (float i = 3; i < 6; i+=0.5f) {
		
			for(float j = -4; j < 3; j+=1.0f){
			
				GameObject e = (GameObject) Instantiate(enemyPrefab, new Vector3(j,i,0), Quaternion.identity);
				enemies.Add(e);
				if(enemies.Count>14){
					if(enemies.Count>28){
						e.GetComponent<SpriteRenderer>().sprite = e2;
					}else{
						e.GetComponent<SpriteRenderer>().sprite = e3;
					}
				}
			}

		}

		currentCount = startingCount = enemies.Count;

	}

	void FixedUpdate(){
		timer += Time.deltaTime;
		float stepTime = linearInterOnEnemy (MIN_STEP, MAX_STEP);
		if (timer >= stepTime) {
			timer=0;
			float speed = linearInterOnEnemy(MAX_SPEED,MIN_SPEED);

			if(moveDownNext){
				moveDownNext=false;
				float vMin = float.PositiveInfinity;
				foreach(GameObject g in enemies){
					if(g.gameObject.activeSelf){
						g.GetComponent<enemyScript>().act();
						g.transform.position+=Vector3.down*speed*stepTime;
						vMin = Mathf.Min(vMin,g.transform.position.y);
					}

				}
				if(vMin<=0)
					Time.timeScale=0;
			}else{
				float min = float.PositiveInfinity;
				float max = float.NegativeInfinity;



				Vector3 dir = Vector3.right;
				if(movingLeft) dir = Vector3.left;

				foreach(GameObject g in enemies){
					if(g.gameObject.activeSelf){
						g.GetComponent<enemyScript>().act();
						g.transform.position+=dir*speed*stepTime;
						min =  Mathf.Min(min,g.transform.position.x);
						max =  Mathf.Max(max,g.transform.position.x);
					}
				}

				if(min - MIN_DIST_TO_WALL  <= lBound){
					movingLeft=false;
					moveDownNext=true;
				}
				if(max + MIN_DIST_TO_WALL  >= rBound){
					movingLeft=true;
					moveDownNext=true;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		currentCount = 0;
		foreach (GameObject g in enemies) {
			if(g.activeSelf) currentCount++;
		}

	}

	float linearInterOnEnemy(float min, float max){
		float d = max - min;
		return min+d*((float)currentCount/startingCount);
	}

}
