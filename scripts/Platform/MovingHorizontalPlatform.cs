using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHorizontalPlatform : MonoBehaviour {
	public float speed = 2;
	private float changeDirection;
	public float changeInterval;

	private bool goFoward = true;
	private bool goBack = false;

	private Vector3 platDirection;

	// Use this for initialization
	void Start () {
		platDirection = Vector3.forward;
		changeDirection = changeInterval;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (platDirection * speed * Time.deltaTime);

		if (changeDirection <= Time.time) {
			
			if (goFoward) {
				platDirection = Vector3.back;
				goFoward = false;
				goBack = true;
				changeDirection = Time.time + changeInterval;
			}

			else if (goBack) {
				
				platDirection = Vector3.forward;
				goFoward = true;
				goBack = false;
				changeDirection = Time.time + changeInterval;
			}
		}

	}
}
