using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSound : NetworkBehaviour {
	public bool isAI = false;
	public AudioClip jetPackSound;
	public AudioClip walkSound;
	public AudioClip pistolSound;
	public AudioClip arSound;
	public AudioClip ammoDry;
	public AudioClip rocketLauncher;
	public AudioClip pickup;
	public AudioClip dead;
	public AudioClip spawn;


	private AudioSource[] aSrcs;
	private AudioSource audioSrc1;
	private AudioSource audioSrc2;
	private AudioSource audioSrc3;


	// Use this for initialization
	private void Awake () {
        if (!isAI) { 
			jetPackSound = Resources.Load<AudioClip> ("Sound/Jetpack/JetpackAir");
			walkSound = Resources.Load<AudioClip> ("Sound/PlayerWalk/Walk");
			pistolSound = Resources.Load<AudioClip> ("Sound/Pistol/PistolShot");
			ammoDry = Resources.Load<AudioClip> ("Sound/AmmoOut/AmmoDry");
			arSound = Resources.Load<AudioClip> ("Sound/AR/AR");
			rocketLauncher = Resources.Load<AudioClip> ("Sound/MissleLauncher/Launcher/shootLauncher");
			pickup = Resources.Load<AudioClip> ("Sound/Pickup/PickupWeapon");
			dead = Resources.Load<AudioClip> ("Sound/PlayerDead/playerDead");
			spawn = Resources.Load<AudioClip> ("Sound/Spawn/spawn");

			aSrcs = GetComponents<AudioSource> ();

			audioSrc1 = aSrcs [0];
			audioSrc2 = aSrcs [1];
			audioSrc3 = aSrcs [2];
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound (string clip)
	{
		if (isLocalPlayer) {
			switch (clip) {
			case "jetPackSound":
				audioSrc1.panStereo = 0f;
				audioSrc1.volume = .05f;
				CmdSendServerSoundID ("jetPackSound");
				break;
			case "walkSound":
				audioSrc2.panStereo = 0f;
				audioSrc2.volume = .02f;
				CmdSendServerSoundID ("walkSound");
				break;
			case "pistolShot":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .5f;
				CmdSendServerSoundID ("pistolShot");
				break;
			case "ammoDry":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .5f;
				CmdSendServerSoundID ("ammoDry");
				break;
			case "AR":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .5f;
				CmdSendServerSoundID ("AR");
				break;
			case "shootLauncher":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .5f;
				CmdSendServerSoundID ("shootLauncher");
				break;
			case "pickup":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .5f;
				CmdSendServerSoundID ("pickup");
				break;
			case "dead":
				audioSrc1.panStereo = 0f;
				audioSrc1.volume = .5f;
				CmdSendServerSoundID ("dead");
				break;
			case "spawn":
				audioSrc3.panStereo = 0f;
				audioSrc3.volume = .1f;
				CmdSendServerSoundID ("spawn");
				break;
			}
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
		if (isLocalPlayer) {
			switch (clip) {
			case "jetPackSound":
				if (!audioSrc1.isPlaying)
					audioSrc1.PlayOneShot (jetPackSound);
				break;
			case "walkSound":
				if (!audioSrc2.isPlaying)
					audioSrc2.PlayOneShot (walkSound);
				break;
			case "pistolShot":
				audioSrc3.PlayOneShot (pistolSound);
				break;
			case "ammoDry":
				audioSrc3.PlayOneShot (ammoDry);
				break;
			case "AR":
				audioSrc3.PlayOneShot (arSound);
				break;
			case "shootLauncher":
				audioSrc3.PlayOneShot (rocketLauncher);
				break;
			case "pickup":
				audioSrc3.PlayOneShot (pickup);
				break;
			case "dead":
				audioSrc1.PlayOneShot (dead);
				break;
			case "spawn":
				audioSrc3.PlayOneShot (spawn);
				break;

			}
		}
	}
	//[ClientRpc]
	void RpcStopServerSound(int id){
		if (id == 1) {
			audioSrc1.Stop();
		}

		if (id == 2) {
			audioSrc2.Stop();
		}

		if (id == 3) {
			audioSrc3.Stop();
		}
	}
}
