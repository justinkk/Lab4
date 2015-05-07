using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float maxXSpeed;
	public float maxYSpeed;
	private Rigidbody2D body;

	// Use this for initialization
	void Start() {
		body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate() {
		//Set the speed
		Vector2 currSpeed = Vector2.zero;
		currSpeed.x = Input.GetAxis("Horizontal")*maxXSpeed;
		currSpeed.y = Input.GetAxis("Vertical")*maxYSpeed;

		body.velocity = currSpeed;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log("colliding");
		
		//Kill the player if the enemy eats you
		if (coll.gameObject.tag == "Enemy")
            Die();
	}

	void Die() {

		Destroy(gameObject);
	}
}
