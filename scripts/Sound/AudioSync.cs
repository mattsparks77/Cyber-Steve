using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class AudioSync : NetworkBehaviour {

	private AudioSource source;

	public AudioClip[] clips;

	// Use this for initialization
	void Start () {
		source = this.GetComponent<AudioSource> ();
	}
	
	public void PlaySound(int id){
		if (id >= 0 && id < clips.Length) {
			CmdSendServerSoundID (id);
		}
	}

	public void stopSource1(){
		
	}

	public void stopSource2(){
		
	}

	[Command]
	void CmdSendServerSoundID(int id){
		RpcSendSoundIDToClients (id);
	}

	[ClientRpc]
	void RpcSendSoundIDToClients(int id){
		source.PlayOneShot (clips [id]);
	}

}
