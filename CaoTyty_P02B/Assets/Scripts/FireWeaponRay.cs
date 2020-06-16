using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponRay : MonoBehaviour
{

	[SerializeField] Camera cameraController;
	[SerializeField] Transform rayOrigin;
	[SerializeField] float shootDistance = 100f;
	[SerializeField] GameObject visualFeedbackObject = null;
	[SerializeField] GameObject visualFeedbackEnemy = null;
	[SerializeField] int weaponDamage = 20;
	[SerializeField] int weaponForce = 20;
	[SerializeField] LayerMask hitLayers;

	RaycastHit objectHit;

	public void ShootRay()
	{
		Vector3 rayDirection = cameraController.transform.forward;
		Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.red, 1f);
		
		if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
		{
			Debug.Log("You hit the " + objectHit.transform.name);
			if (objectHit.transform.tag == "Enemy")
			{
				//apply visual feedback
				//visualFeedbackEnemy.transform.position = objectHit.point;

				//apply damage
				EnemyShooter enemyShooter = objectHit.transform.GetComponent<EnemyShooter>();
				if (enemyShooter != null)
				{
					Vector3 hitPos = objectHit.point;
					Vector3 hitRot = objectHit.point;
					//Quaternion hitQuat = cameraController.gameObject.transform.rotation;
					//Vector3 hitRot = hitQuat.eulerAngles;
					Debug.Log(hitRot);
					enemyShooter.TakeDamage(weaponDamage, weaponForce, hitRot);
					Instantiate(visualFeedbackEnemy, hitPos, cameraController.transform.rotation);
				}
			}
			else
			{
				//visualFeedbackObject.transform.position = objectHit.point;
				Vector3 hitPos = objectHit.point;
				Instantiate(visualFeedbackObject, hitPos, cameraController.transform.rotation);
			}
		}
		else
		{
			Debug.Log("Missed");
		}
	}
}
