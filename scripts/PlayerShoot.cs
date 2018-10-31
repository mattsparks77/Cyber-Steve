using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof (WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";
	//public GameObject missle;
	//public Transform firepoint;
	public GameObject explosion;
	private float nextFire = 0.5f;

	[SerializeField]
	public MissleLauncher missleLauncher;
	public Missle missle;
	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;
    private GameObject weaponHolderFP;

    public PlayerWeapon currentWeapon;
	private WeaponManager weaponManager;
	private Transform weaponHolder;

	private PlayerSound playerSound;

	void Start ()
	{

		if (cam == null)
		{
			//Debug.LogError("PlayerShoot: No camera referenced!");
			this.enabled = false;
		}

		weaponManager = GetComponent<WeaponManager>();
		playerSound = this.GetComponent<PlayerSound> ();


	}

	void Update ()
	{
        if (this.gameObject.layer == 14)
            return;
		currentWeapon = weaponManager.GetCurrentWeapon();

        //if (currentWeapon.name == "AI")
        //    return;
        //if (currentWeapon.bullets < currentWeapon.maxBullets)
        //{
        //	if (Input.GetKeyDown(KeyCode.X))
        //	{
        //		weaponManager.Reload();

        //	}
        //}

        if (currentWeapon.name == "MissleLauncher")
		{
			if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
			{
				// weaponManager.GetCurrentWeapon().firing = true;
				nextFire = Time.time + currentWeapon.fireRate;
				if (currentWeapon.bullets > 0) {
					playerSound.PlaySound ("shootLauncher");
				} else {
					playerSound.PlaySound ("ammoDry");
				}
				ShootMissile();
			}

		}

		if (currentWeapon.name == "Pistol") {
			if (currentWeapon.fireRate <= 0f)
			{

			} else
			{
				if (Input.GetButtonDown("Fire1")&& Time.time > nextFire)
				{
					// weaponManager.GetCurrentWeapon().firing = true;
					nextFire = Time.time + currentWeapon.fireRate;
					if (currentWeapon.bullets > 0) {
						playerSound.PlaySound ("pistolShot");
					} else {
						playerSound.PlaySound ("ammoDry");
					}
					Shoot();
				} 
			}
		}
		if (currentWeapon.name == "Sniper") {
			if (currentWeapon.fireRate <= 0f)
			{

			} else
			{
				if (Input.GetButtonDown("Fire1")&& Time.time > nextFire)
				{
					// weaponManager.GetCurrentWeapon().firing = true;
					nextFire = Time.time + currentWeapon.fireRate;
					if (currentWeapon.bullets > 0) {
						playerSound.PlaySound ("pistolShot");
					} else {
						playerSound.PlaySound ("ammoDry");
					}
					Shoot();
				} 
			}
		}
		if (currentWeapon.name == "Assault Rifle") {
			if (currentWeapon.fireRate <= 0f)
			{

			} else
			{
				if (Input.GetButtonDown("Fire1")&& Time.time > nextFire)
				{
					// weaponManager.GetCurrentWeapon().firing = true;
					nextFire = Time.time + currentWeapon.fireRate;
					if (currentWeapon.bullets > 0) {
						playerSound.PlaySound ("AR");
					} else {
						playerSound.PlaySound ("ammoDry");
					}
					Shoot();
				} 
			}
//			if (currentWeapon.fireRate <= 0f)
//			{
//
//
//			} else
//			{
//				if (Input.GetButtonDown ("Fire1")) {
//					InvokeRepeating ("Shoot", 0f, 1f / currentWeapon.fireRate);
//					InvokeRepeating ("playAR", 0f, 1f / currentWeapon.fireRate);
//
//
//				} else if (Input.GetButtonUp ("Fire1")) {
//					CancelInvoke ("Shoot");
//					CancelInvoke ("playAR");
//
//				}
//			}
		}
		//if (currentWeapon.name == "Pistol") {

		//}
	}

	//Is called on the server when a player shoots
	[Command]
	void CmdOnShoot ()
	{
		RpcDoShootEffect();
	}

	[Client]
	void ShootMissile()
	{
		if (!isLocalPlayer || weaponManager.isReloading)
		{
			return;
		}

		if (currentWeapon.bullets <= 0)
		{
			weaponManager.Reload();
			return;
		}


		Debug.Log("Remaining bullets: " + currentWeapon.bullets);
		CmdOnShoot ();
		CmdLaunchMissile();
		currentWeapon.bullets--;

		if (currentWeapon.bullets <= 0)
		{
			weaponManager.Reload();
		}
	}
	//Is called on all clients when we need to do
	// a shoot effect
	[ClientRpc]
	void RpcDoShootEffect ()
	{
		weaponManager.GetCurrentGraphics().muzzleFlash.Play();
	}

	//Is called on the server when we hit something
	//Takes in the hit point and the normal of the surface
	[Command]
	void CmdOnHit (Vector3 _pos, Vector3 _normal)
	{
		RpcDoHitEffect(_pos, _normal);
	}
	[Command]
	void CmdLaunchMissile ()
	{
		RpcInstantiateMissile();
	}

	//Is called on all clients
	//Here we can spawn in cool effects
	[ClientRpc]
	void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
	{
		GameObject _hitEffect = (GameObject)Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
		Destroy(_hitEffect, 2f);
	}
	[ClientRpc]
	void RpcInstantiateMissile()
	{
		//weaponHolder = weaponManager.GetWeaponHolderTransform();
        weaponHolderFP = weaponManager.GetWeaponHolderFPTransform();
        GameObject rocketInstance;

		rocketInstance = Instantiate(missleLauncher.missle, weaponHolderFP.transform.position,
            weaponHolderFP.transform.rotation);
		rocketInstance.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 5000);
	}

	[Client]
	void playAR(){
		playerSound.PlaySound ("AR");
	}

	[Client]
	void Shoot () // shoots automatic guns
	{
		if (!isLocalPlayer || weaponManager.isReloading)
		{
			return;
		}

		if (currentWeapon.bullets <= 0)
		{
			weaponManager.Reload();
			return;
		}


		Debug.Log("Remaining bullets: " + currentWeapon.bullets);

		//We are shooting, call the OnShoot method on the server

		CmdOnShoot();



		currentWeapon.bullets--;
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask) )
		{
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage, transform.name);
            }
			//} else if (_hit.collider.tag == "AI")
			//	CmdPlayerShot ("AI", currentWeapon.damage, transform.name);

			// We hit something, call the OnHit method on the server
			CmdOnHit(_hit.point, _hit.normal);
		}

		if (currentWeapon.bullets <= 0)
		{
			weaponManager.Reload();
		}	
	}

    //void ShootMissle(){
    //       //missleLauncher = GetComponent<MissleLauncher> ();
    //       //missleLauncher.firing = true;
    //       missleLauncher.CmdFireMissile();
    //}


	//void OnTriggerEnter(Collider other)
	//{
	//			if (other.gameObject.tag == "DeathBarrier")
	//				Die ("Player fell to their death.");
	//	//makes the playr take damage from explosions but not direct missiles
	//	if (other.gameObject.tag == "Explosion")
	//	{
	//		CmdPlayerShot (other.gameObject.name, 50, "Explosion");
	//	}
	//}

    [Command]
	public void CmdPlayerShot (string _playerID, int _damage, string _sourceID)
	{
		Debug.Log(_playerID + " has been shot.");

		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(_damage, _sourceID);
	}
	//	void OnCollisionEnter(Collision other){
	//		if (other.gameObject.tag == "Missile")
	//		{
	//			CmdPlayerShot (other.collider.name, 50, transform.name);
	//
	//			Instantiate (explosion, other.transform.position, other.transform.rotation);
	//			Destroy (other.gameObject);
	//		}
	//		
	//		}

}