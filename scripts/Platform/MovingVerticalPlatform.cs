using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVerticalPlatform : MonoBehaviour {
	public float speed = 2;
	private float changeDirection;
	public float changeInterval;

	private bool goUp = true;
	private bool goDown = false;

	private Vector3 platDirection;

	// Use this for initialization
	void Start () {
		platDirection = Vector3.up;
		changeDirection = changeInterval;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (platDirection * speed * Time.deltaTime);

		if (changeDirection <= Time.time) {
			
			if (goUp) {
				platDirection = Vector3.down;
				goUp = false;
				goDown = true;
				changeDirection = Time.time + changeInterval;
			}

			else if (goDown) {
				
				platDirection = Vector3.up;
				goUp = true;
				goDown = false;
				changeDirection = Time.time + changeInterval;
			}
		}

	}
}
