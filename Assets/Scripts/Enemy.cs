using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed;
	public GameObject player;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//Move towards the player
		Vector2 direction = player.GetComponent<Transform>().position - GetComponent<Transform>().position;
		direction = direction.normalized;
		body.velocity = direction*speed;
	}
}
