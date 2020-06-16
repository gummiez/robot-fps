using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
	FPSInput _input = null;
	FPSMotor _motor = null;
	FireWeaponRay fireWeaponScript;

	[SerializeField] float _iniMoveSpeed = .1f;
	[SerializeField] float _moveSpeed = .1f;
	[SerializeField] float _turnSpeed = 6f;
	[SerializeField] float _jumpStrength = 10f;
	[SerializeField] float _sprintSpeed = .18f;

	private void Awake()
	{
		_input = GetComponent<FPSInput>();
		_motor = GetComponent<FPSMotor>();
	}

    private void Start()
    {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		fireWeaponScript = gameObject.GetComponent<FireWeaponRay>();
    }

    private void OnEnable()
	{
		_input.MoveInput += OnMove;
		_input.RotateInput += OnRotate;
		_input.JumpInput += OnJump;
		_input.SprintInput += OnSprint;
		_input.SprintRelease += OnSprintRelease;
		_input.FireInput += OnFire;
		_input.FireRelease += OnFireRelease;
	}

	private void OnDisable()
	{
		_input.MoveInput -= OnMove;
		_input.RotateInput -= OnRotate;
		_input.JumpInput -= OnJump;
		_input.SprintInput += OnSprint;
		_input.SprintRelease += OnSprintRelease;
		_input.FireInput += OnFire;
	}

	void OnMove(Vector3 movement)
	{
		_motor.Move(movement * _moveSpeed);
	}
	void OnRotate(Vector3 rotation)
	{
		_motor.Turn(rotation.y * _turnSpeed);
		_motor.Look(rotation.x * _turnSpeed);
	}
	void OnJump()
	{
		_motor.Jump(_jumpStrength);
	}

    void OnFire()
    {
		_motor.Fire();
		fireWeaponScript.ShootRay();
    }

	void OnFireRelease()
	{
		_motor.FireRelease();
	}

    void OnSprint()
    {
		_moveSpeed = _sprintSpeed;
    }
	void OnSprintRelease()
    {
		_moveSpeed = _iniMoveSpeed;
    }
}
