using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missle : MonoBehaviour {

	public GameObject explosion;
    public ParticleSystem muzzleFlash;
	public PlayerShoot ps;
	bool waitToDestroy;
	//private float nextActionTime = 3.0f;
	public float period = 3f;
	public float waitTime = 2.5f;
	public int missileDamage = 50;
    // Use this for initialization

	private MissleSound missleSound;

    void Start () {
		ps = FindObjectOfType<PlayerShoot> ();
		missleSound = this.GetComponent<MissleSound> ();
	}

    // Update is called once per frame
    //	void Update () {
    //		if (waitToDestroy &&Time.time > nextActionTime){
    //			Destroy (this.gameObject);
    //			nextActionTime += period;
    //		}
    //
    //			
    //}

    void OnCollisionEnter(Collision collision)
    {
        //float nextActionTime = Time.time;
        //if (collision.gameObject.tag !=  "Player") {

        //missleSound.stopSound (2);
        //Instantiate (explosion, transform.position, transform.rotation);

        //AudioSource.PlayClipAtPoint (missleSound.explode, transform.position);
        //this.gameObject.SetActive (false);
        //Destroy (this.gameObject);
        //waitToDestroy = true;


        //}
        //int oldLayer = gameObject.layer;

        //Change object layer to a layer it will be alone
        //gameObject.layer = LayerMask.NameToLayer("Ghost");

        //int layerToIgnore = 1 << gameObject.layer;
        //layerToIgnore = ~layerToIgnore;
        if (collision.gameObject.layer != 8)
        // if (Physics.Raycast(ray, distance, layerToIgnore))
        {
			if (collision.gameObject.tag == "Player"||collision.gameObject.tag == "AI" )
            {
                Instantiate(explosion, transform.position, transform.rotation);
                collision.gameObject.GetComponent<PlayerShoot>().CmdPlayerShot(collision.gameObject.name, missileDamage, "MissileHit");
                AudioSource.PlayClipAtPoint(missleSound.explode, transform.position);
                //this.gameObject.SetActive (false);
                Destroy(this.gameObject);
            }
            else
            {
                Instantiate(explosion, transform.position, transform.rotation);

                AudioSource.PlayClipAtPoint(missleSound.explode, transform.position);
                //this.gameObject.SetActive (false);
                Destroy(this.gameObject);
            }
            
            //waitToDestroy = true;
            //do what you need
        }


        // set the game object back to its original layer
        //gameObject.layer = oldLayer;
    }
    }


