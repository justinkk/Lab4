using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	public float maxXSpeed;
	public float maxYSpeed;
	private Rigidbody2D body;
	private bool facingLeft = true;
	private bool canMove = true;
	private int score;

	public float maxScale; //maximum x and y scale
	public float growthRate; //what fraction of the gam to the maximum scale you grow with each seaweed


	public Text scoreText;
	public Text loseText;
	public GameObject sharkObject;
	private Enemy shark;

	// Use this for initialization
	void Start() {
		body = GetComponent<Rigidbody2D>();

		score = 0;

		SetScore();
		loseText.text = "";

		shark = sharkObject.GetComponent<Enemy>();
	}
	
	void FixedUpdate() {
		//Stop after you die
		if (canMove) {
			//Set the speed
			Vector2 currSpeed = Vector2.zero;
			currSpeed.x = Input.GetAxis("Horizontal")*maxXSpeed;
			currSpeed.y = Input.GetAxis("Vertical")*maxYSpeed;

			body.velocity = currSpeed;

			//Flip if appropriate
			if ((currSpeed.x < 0) != facingLeft && currSpeed.x != 0) {
				facingLeft = !facingLeft;
				Vector3 scale = GetComponent<Transform>().localScale;
				scale.x *= -1;
				GetComponent<Transform>().localScale = scale;
			}
		}	
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherObject = other.gameObject;
		//Kill the player if the enemy eats you
		if (otherObject.tag == "Enemy")
            Die();
        else if (otherObject.tag == "Seaweed") {
        	Destroy(otherObject);
        	score++;
        	SetScore();
        	Grow();
        }
	}

	void Die() {
		loseText.text = "Game Over";
		canMove = false;
		body.velocity = Vector2.zero;

		shark.Eat();

		Destroy(gameObject);
	}

	void SetScore() {
		scoreText.text = "Seaweed eaten: " + score;
	}

	void Grow() {
		Vector3 currScale = transform.localScale;
		float delta = ((maxScale - currScale.y) * growthRate);
		
		if (currScale.x > 0)
			currScale.x += delta;
		else
			currScale.x -= delta;
		currScale.y += delta;
		transform.localScale = currScale;
	}
}
