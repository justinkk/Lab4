using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed;
	public GameObject player;
	private Rigidbody2D body;
	private AudioSource source;
	private SpriteRenderer sprenderer;

	public float minFreq;
	public float maxFreq;
	public float minDistance;
	public float maxDistance;

	private bool isHunting;

	//private AudioSource source;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
		source = GetComponent<AudioSource>();
		sprenderer = GetComponent<SpriteRenderer>();

		//Make the shark hidden and sneaky
		sprenderer.enabled = false;

		//Set position randomly, at least half the screen away
		float newX = Random.Range(0.75f, 1.0f);
		float newY = Random.Range(0.75f, 1.0f);
		if (Random.value > 0.5)
			newX = 1 - newX;
		if (Random.value > 0.5)
			newY = 1 - newY;
		GetComponent<Transform>().position = Camera.main.ViewportToWorldPoint(new Vector3(newX, newY, 10)); //10 is to offset camera's z position

		//Start making noise
		isHunting = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Move towards the player
		Vector2 direction = player.GetComponent<Transform>().position - GetComponent<Transform>().position;
		Vector2 normalizedDirection = direction.normalized;
		body.velocity = normalizedDirection*speed;

		//Play sound with frequency dependant on distance
					//Frequency scaled linearly     *     Proportion between minDistance and maxDistance, linearly
		source.pitch = maxFreq - ((maxFreq - minFreq) * Mathf.Max((Mathf.Min(maxDistance, direction.magnitude) - minDistance)/(maxDistance - minDistance), 0f));
		if(!source.isPlaying && isHunting) {
			source.Play();
		}
	}

	//Become visible and stop making noise
	public void Eat() {
		isHunting = false;
		sprenderer.enabled = true;
	}
}
