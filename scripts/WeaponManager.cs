using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponManager : NetworkBehaviour {

	[SerializeField]
	private string weaponLayerName = "Weapon";
  

	[SerializeField]
	private Transform weaponHolder;

    public GameObject weaponHolderFP;
    public PlayerWeapon[] weaponsList;


	//private bool PickedUpWeapon = false;
	public float WeaponPickUpTime =5f;

	public PlayerWeapon currentWeapon;
	private WeaponGraphics currentGraphics;

	public bool isReloading = false;

	private PlayerSound playerSound;


    public Animator animator;
    private bool Scoped = false;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;
    public float scopedFOV = 10f;
    private float normalFOV;
    private bool firsttime = false;
    // Use this for initialization


    // Update is called once per frame
    void Update()
    {
        if (!firsttime &&scopeOverlay)
        {
            scopeOverlay.SetActive(false);
            firsttime = true;
        }
       
        if (Input.GetButtonDown("Fire2"))
        {
            Scoped = !Scoped;
            animator.SetBool("isScoped", Scoped);
            if (Scoped)
                StartCoroutine(OnScoped());
            else
                OnUnScoped();
        }

    }
    public GameObject GetWeaponHolderFPTransform()
    {
        return weaponHolderFP;
    }
    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        if (currentWeapon.name == "Sniper")
        {
            scopeOverlay.SetActive(true);
            weaponCamera.SetActive(false);
            normalFOV = mainCamera.fieldOfView;
            mainCamera.fieldOfView = scopedFOV;
        }
        else {
            
            normalFOV = mainCamera.fieldOfView;
            mainCamera.fieldOfView = 50f;
        }
       
        
    }
    void OnUnScoped()
    {
		if (currentWeapon.name == "Sniper") {
			scopeOverlay.SetActive (false);
			weaponCamera.SetActive (true);
			mainCamera.fieldOfView = normalFOV;
		} else {
			weaponCamera.SetActive (true);
			mainCamera.fieldOfView = normalFOV;
		}
    }

    void Start ()
	{
        
        EquipPistol();
        playerSound = this.GetComponent<PlayerSound>();
		scopeOverlay = GameObject.FindGameObjectWithTag("ScopeOverlay");

	}
    public Transform GetWeaponHolderTransform()
    {
        return weaponHolder;
    }
	public PlayerWeapon GetCurrentWeapon ()
	{
		return currentWeapon;
	}

	public WeaponGraphics GetCurrentGraphics()
	{
		return currentGraphics;
	}
	void DisableWeaponGraphics()
	{
		playerSound.PlaySound("pickup");
		currentGraphics.gameObject.SetActive(false);
	}
    public void EquipPistol()
    {
        currentWeapon = weaponsList[0];
        currentWeapon.bullets = currentWeapon.maxBullets;
        GameObject _weaponIns = (GameObject)Instantiate(weaponsList[0].graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        //currentGraphics.hitEffectPrefab = currentWeapon.hitEffectPrefab;
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        //currentWeapon.name = weaponsList[0].name;
        //currentGraphics.muzzleFlash = currentWeapon.muzzleFlash;
        currentGraphics.gameObject.SetActive(true);
        if (currentGraphics == null)
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);

        if (isLocalPlayer)
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

    }
    public void EquipRPG()
    {
        currentWeapon = weaponsList[2];
        currentWeapon.bullets = currentWeapon.maxBullets;
      
        GameObject _weaponIns = (GameObject)Instantiate(weaponsList[2].graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        //currentGraphics.hitEffectPrefab = currentWeapon.hitEffectPrefab;
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();

        //currentWeapon.name = weaponsList[0].name;
        //currentGraphics.muzzleFlash = currentWeapon.muzzleFlash;
        currentGraphics.gameObject.SetActive(true);
        if (currentGraphics == null)
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);

        if (isLocalPlayer)
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

    }
    public void EquipAR()
    {
        currentWeapon = weaponsList[1];
        currentWeapon.bullets = currentWeapon.maxBullets;
        GameObject _weaponIns = (GameObject)Instantiate(weaponsList[1].graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        //currentGraphics.hitEffectPrefab = currentWeapon.hitEffectPrefab;
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        //currentWeapon.name = weaponsList[0].name;
        //currentGraphics.muzzleFlash = currentWeapon.muzzleFlash;
        currentGraphics.gameObject.SetActive(true);
        if (currentGraphics == null)
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);

        if (isLocalPlayer)
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

    }
    public void EquipSniper()
    {
        currentWeapon = weaponsList[3];
        currentWeapon.bullets = currentWeapon.maxBullets;
        GameObject _weaponIns = (GameObject)Instantiate(weaponsList[3].graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        //currentGraphics.hitEffectPrefab = currentWeapon.hitEffectPrefab;
       
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        //currentWeapon.name = weaponsList[3].name;
        //currentGraphics.muzzleFlash = currentWeapon.muzzleFlash;
		currentGraphics.gameObject.SetActive(true);
        if (currentGraphics == null)
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);

        if (isLocalPlayer)
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));

    }
//    public void EquipWeapon ()
//	{
//		
//		
//		GameObject _weaponIns = (GameObject)Instantiate(currentWeapon.graphics, weaponHolder.position, weaponHolder.rotation);
//		_weaponIns.transform.SetParent(weaponHolder);
//
//        //currentGraphics.hitEffectPrefab = currentWeapon.hitEffectPrefab;
//        
//		//currentWeapon.name = _weapon.name;
//		//currentGraphics.muzzleFlash = currentWeapon.muzzleFlash;
//		currentGraphics.gameObject.SetActive (true);
//		if (currentGraphics == null)
//			Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);
//
//		if (isLocalPlayer)
//			Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
//		//Invoke ("DisableWeaponGraphics", WeaponPickUpTime);
//		//Invoke ("EquipPistol", WeaponPickUpTime);
//
//
//	}

	public void Reload ()
	{
		if (isReloading)
			return;

		StartCoroutine(Reload_Coroutine());
	}

	private IEnumerator Reload_Coroutine ()
	{
		Debug.Log("Reloading...");

		isReloading = true;

		//CmdOnReload();

		yield return new WaitForSeconds(currentWeapon.reloadTime);

		currentWeapon.bullets = currentWeapon.maxBullets;

		isReloading = false;
	}

//	[Command]
//	void CmdOnReload ()
//	{
//		RpcOnReload();
//	}

//	[ClientRpc]
//	void RpcOnReload ()
//	{
//		Animator anim = currentGraphics.GetComponent<Animator>();
//		if (anim != null)
//		{
//			anim.SetTrigger("Reload");
//		}
//	}
	void OnTriggerEnter(Collider other){
		if (this.gameObject.GetComponent < Player> ().isAI)
			return;
		if (other.gameObject.tag == "MissileLauncher") {
			//source.Play ();
			//playerSound.PlaySound("pickup");
			other.gameObject.SetActive(false);
            //currentGraphics.gameObject.SetActive (false);
            DisableWeaponGraphics();
            EquipRPG();
			//EquipTempWeapon(weaponsList[2]);
			currentWeapon.bullets = 4;
		
		}
		if (other.gameObject.tag == "Assault Rifle") {
			//source.Play ();
			//playerSound.PlaySound("pickup");
			other.gameObject.SetActive(false);
			//currentGraphics.gameObject.SetActive (false);
            DisableWeaponGraphics();
            EquipAR();
			//EquipTempWeapon(weaponsList[1]);

		}
		if (other.gameObject.tag == "Pistol") {
			//source.Play ();
			//playerSound.PlaySound("pickup");
			other.gameObject.SetActive(false);
            //currentGraphics.gameObject.SetActive (false);
            DisableWeaponGraphics();
            EquipPistol();
			currentWeapon.bullets = 15;

		}
		if (other.gameObject.tag == "Sniper") {
			//source.Play ();
			//playerSound.PlaySound("pickup");
			other.gameObject.SetActive(false);
            //currentGraphics.gameObject.SetActive (false);
            DisableWeaponGraphics();
            EquipSniper();
			//EquipTempWeapon(weaponsList[3]);
			currentWeapon.bullets = 5;

		}
	}

}