using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayMovement : MonoBehaviour
{
	[SerializeField] float intensity = 0;
	[SerializeField] float strafeIntensity = 0;
	[SerializeField] float smooth = 0;

	Quaternion target_rotation;
	Quaternion origin_rotation;
	Transform target_position;
	Vector3 origin_position;


	private void Start()
	{
		origin_rotation = transform.localRotation;
		origin_position = transform.localPosition;
	}

	private void Update()
	{
		CalculateSwayMovement();
	}

	void CalculateSwayMovement()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");

		Quaternion targetAdjX = Quaternion.AngleAxis(intensity * mouseX, Vector3.up);
		Quaternion targetAdjY = Quaternion.AngleAxis(intensity * mouseY, Vector3.right);
		Quaternion target_rotation = origin_rotation * targetAdjX * targetAdjY;

		Vector3 targetX = (strafeIntensity * moveX * Vector3.right);
		Vector3 targetY = ((strafeIntensity * moveY * Vector3.up) / 2);
		Vector3 target_movement = origin_position + targetX + targetY;

		transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
		transform.localPosition = Vector3.Lerp(transform.localPosition, target_movement, Time.deltaTime * smooth);
	}
}
