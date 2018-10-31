
using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

	public string name;

	public int damage = 10;
	public float range = 100f;
    public bool firing = false;
	public float fireRate = 0f;
	public ParticleSystem muzzleFlash;
	public GameObject hitEffectPrefab;
	public int maxBullets = 20;
	[HideInInspector]
	public int bullets;
	[SerializeField]
	public Transform firePoint;
	public float reloadTime = 1f;

	public GameObject graphics;

	public PlayerWeapon ()
	{
		bullets = maxBullets;
	}
}