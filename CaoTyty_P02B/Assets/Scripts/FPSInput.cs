using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPSInput : MonoBehaviour
{
	[SerializeField] bool _invertVertical = false;

    public event Action<Vector3> MoveInput = delegate { };
    public event Action<Vector3> RotateInput = delegate { };
    public event Action JumpInput = delegate { };
	public event Action SprintInput = delegate { };
	public event Action SprintRelease = delegate { };
	public event Action FireInput = delegate { };
	public event Action FireRelease = delegate { };

    private void Update()
    {
        DetectMoveInput();
        DetectRotateInput();
        DetectJumpInput();
		DetectSprintInput();
		DetectFireInput();
    }

    void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if(xInput !=0 || yInput != 0)
        {
            Vector3 _horizontalMovement = transform.right * xInput;
			Vector3 _forwardMovement = transform.forward * yInput;

			Vector3 movement = (_horizontalMovement + _forwardMovement).normalized;

			MoveInput?.Invoke(movement);
        }
    }

	void DetectRotateInput()
	{
		float xInput = Input.GetAxisRaw("Mouse X");
		float yInput = Input.GetAxisRaw("Mouse Y");

		if (xInput != 0 || yInput != 0)
		{
			if(_invertVertical == true)
			{
				yInput = -yInput;
			}
			//mouse left/right should be y axis, up/down is x axis
			Vector3 rotation = new Vector3(yInput, xInput, 0);

			RotateInput?.Invoke(rotation);
		}
	}

	void DetectJumpInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			JumpInput?.Invoke();
		}
	}

    void DetectSprintInput()
    {
		float shiftInput = Input.GetAxisRaw("Fire3");
		if (shiftInput != 0)
		{
			SprintInput?.Invoke();
		}
		else
		{
			SprintRelease?.Invoke();
		}
	}

    void DetectFireInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
		    FireInput?.Invoke();
        } else
		{
			FireRelease?.Invoke();
		}
    }
}
