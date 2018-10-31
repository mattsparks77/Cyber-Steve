using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissleSound : NetworkBehaviour {

	public AudioClip explode;
	public AudioClip flyBy;

	private AudioSource[] aSrcs;
	public AudioSource audioSrc1;
	public AudioSource audioSrc2;



	// Use this for initialization
	void Start () {
		flyBy = Resources.Load<AudioClip> ("Sound/MissleLauncher/Missle/flyBy");
		explode = Resources.Load<AudioClip> ("Sound/MissleLauncher/Missle/explode");

		aSrcs = GetComponents<AudioSource> ();

		audioSrc1 = aSrcs [0];
		audioSrc2 = aSrcs [1];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound (string clip)
	{
		print ("test");
		switch (clip) {
		case "explode":
			audioSrc1.panStereo = 0f;
			audioSrc1.volume = .05f;
			CmdSendServerSoundID ("explode");
			break;
		case "flying":
			audioSrc2.panStereo = 0f;
			audioSrc2.volume = 1f;
			CmdSendServerSoundID ("flying");
			break;
		}
	}

	//[Client]
	public void stopSound(int id){
		CmdStopServerSound (id);
	}

	//[Command]
	void CmdSendServerSoundID(string clip){
		RpcSendSoundIDToClients (clip);
	}
	//[Command]
	void CmdStopServerSound(int id){
		RpcStopServerSound (id);
	}

	//[ClientRpc]
	void RpcSendSoundIDToClients(string clip){
		switch (clip) {
		case "explode":
			audioSrc1.PlayOneShot (explode);
			print ("exploding");
			break;
		case "flying":
			audioSrc2.PlayOneShot (flyBy);
			break;
		}
	}

	[ClientRpc]
	void RpcStopServerSound(int id){
		if (id == 1) {
			audioSrc1.Stop();
		}
		if (id == 2) {
			audioSrc2.Stop();
		}
	}
}
