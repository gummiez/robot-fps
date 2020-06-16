using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
	public event Action Land = delegate { };

	[SerializeField] GameGUIManager gM;
	[SerializeField] Camera _camera = null;
	[SerializeField] GameObject _gun = null;
	[SerializeField] GroundDetector _groundDetector = null;
	[SerializeField] float _cameraAngleLimit = 70f;
	[SerializeField] Animator playerAnim = null;
	[SerializeField] Animator recoilAnim = null;
	BasicGun bS;

	private float _currentCameraRotationX = 0f;
	Rigidbody _rigidbody = null;

	Vector3 _movementThisFrame = Vector3.zero;
	float _turnAmountThisFrame = 0;
	float _lookAmountThisFrame = 0;
	[SerializeField] bool _isGrounded = false;
	bool _isMenuOpen;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		bS = _gun.GetComponent<BasicGun>();
	}

	private void Start()
	{
		playerAnim = playerAnim.GetComponent<Animator>();
	}

    private void FixedUpdate()
	{
		ApplyMovement(_movementThisFrame);
		ApplyTurn(_turnAmountThisFrame);
		ApplyLook(_lookAmountThisFrame);
	}

    void OnEnable()
    {
		_groundDetector.GroundDetected += OnGroundDetected;
		_groundDetector.GroundVanished += OnGroundVanished;
    }

	public void Move(Vector3 requestedMovement)
	{
		_movementThisFrame = requestedMovement;
	}

	public void Turn(float turnAmount)
	{
		_turnAmountThisFrame = turnAmount;
	}

	public void Look(float lookAmount)
	{
		_lookAmountThisFrame = lookAmount;
	}

	public void Jump(float jumpForce)
	{
        if(_isGrounded == false)
        {
			Debug.Log("airborne");
			return;
        }

		_rigidbody.AddForce(Vector3.up * jumpForce);
	}

	public void Fire()
	{
		_isMenuOpen = gM.MenuOpen;
		if (_isMenuOpen == false)
        { 
		    bS.SpawnMuzzleFlash();
			playerAnim.Play("GunClick");
			recoilAnim.Play("GunRecoil");
			//playerAnim.SetBool("isShooting", true);
		    AudioManager.instance.Play("GunShot");
	    }
    }

	public void FireRelease()
	{
		//playerAnim.SetBool("isShooting", false);
	}

	private void ApplyMovement(Vector3 moveVector)
	{
		if (moveVector == Vector3.zero)
			return;

		//_rigidbody.MovePosition(_rigidbody.position + moveVector);
		_rigidbody.AddForce(moveVector);
		_movementThisFrame = Vector3.zero;
	}

	private void ApplyTurn(float rotateAmount)
	{
		if (rotateAmount == 0)
			return;

		Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
		_rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
		_turnAmountThisFrame = 0;
	}

	private void ApplyLook(float lookAmount)
	{
		if (lookAmount == 0)
		{
			return;
		}

		_currentCameraRotationX -= lookAmount;
		_currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);
		_camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);

		_lookAmountThisFrame = 0;
	}

    void OnGroundDetected()
    {
		_isGrounded = true;
		//notify that landed
		Land?.Invoke();
    }
    void OnGroundVanished()
    {
		_isGrounded = false;
    }
}
