
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;

	[SerializeField]
	private float thrusterFuelBurnSpeed = 1f;
	[SerializeField]
	private float thrusterFuelRegenSpeed = 0.3f;
	private float thrusterFuelAmount = 1f;

	public float GetThrusterFuelAmount()
	{
		return thrusterFuelAmount;
	}
	[SerializeField]
	private LayerMask environmentMask;
	[Header("Spring Settings: ")]

	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;
	private PlayerMotor motor;

	private Animator animator;
	private ConfigurableJoint joint;

	public AudioSource audioSrc1;
	public AudioSource audioSrc2;

	private PlayerSound playerSound;
	void Start()
	{
		motor = GetComponent<PlayerMotor> ();
		joint = GetComponent<ConfigurableJoint> ();
		animator = GetComponent<Animator> ();
		SetJointSettings (jointSpring);
		Cursor.lockState = CursorLockMode.Locked;
		playerSound = this.GetComponent<PlayerSound> ();
	}
	void Update()
	{
		if (!this.gameObject.CompareTag ("AI")) {
			
			if (PauseMenu.IsOn) {
				Cursor.lockState = CursorLockMode.None;
				return;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}

			if (Input.GetKeyDown (KeyCode.Equals)) {
				lookSensitivity += 1;
			}

			if (Input.GetKeyDown (KeyCode.Minus)) {
				lookSensitivity -= 1;
			}
			
			RaycastHit _hit;
			if (Physics.Raycast (transform.position, Vector3.down, out _hit, 100f, environmentMask)) {
				joint.targetPosition = new Vector3 (0f, -_hit.point.y, 0f);
			} else {
				joint.targetPosition = new Vector3 (0f, 0f, 0f);

			}
			float xMov = Input.GetAxis ("Horizontal");
			float zMov = Input.GetAxis ("Vertical");

			if (xMov > 0.3 || xMov < -0.3 || zMov > 0.3 || zMov < -0.3) {
				if (!audioSrc2.isPlaying) {
					playerSound.PlaySound ("walkSound");
				}
			} else {
				playerSound.stopSound (2);
			}


			Vector3 movHor = transform.right * xMov;
			Vector3 movVer = transform.forward * zMov;
	
			Vector3 _velocity = (movHor + movVer) * speed;

			animator.SetFloat ("ForwardVelocity", zMov);
			motor.Move (_velocity);
			float yRot = Input.GetAxisRaw ("Mouse X");
			Vector3 rotation = new Vector3 (0f, yRot, 0f) * lookSensitivity;
			motor.Rotate (rotation);

			float xRot = Input.GetAxisRaw ("Mouse Y");
			float camera_rotationX = xRot * lookSensitivity;
			motor.RotateCamera (camera_rotationX);

			Vector3 _thrusterForce = Vector3.zero;

			if (Input.GetButton ("Jump") && thrusterFuelAmount > 0f) {

				thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;
				if (thrusterFuelAmount >= 0.01f) {
					// allows the player to pass thru objects
					//gameObject.layer = LayerMask.NameToLayer("PassThruTerrain");

					if (!audioSrc1.isPlaying) {
						playerSound.PlaySound ("jetPackSound");
					}

					_thrusterForce = Vector3.up * thrusterForce;
					SetJointSettings (0f);

				}


			} else {
				//sets the players layer back to normal after jumping
				//gameObject.layer = LayerMask.NameToLayer("LocalPlayer");

				playerSound.stopSound (1);
				thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;

				SetJointSettings (jointSpring);
			}
			thrusterFuelAmount = Mathf.Clamp (thrusterFuelAmount, 0f, 1f);

			motor.ApplyThruster (_thrusterForce);
		} 
		}
	private void SetJointSettings(float _jointSpring)
	{
		joint.yDrive = new JointDrive {

			positionSpring = _jointSpring, 
			maximumForce = jointMaxForce
		};
	}
}
