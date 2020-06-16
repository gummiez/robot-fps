using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
	int health = 100;
	float xRot = 0;
	float yRot = 0;
	float zRot = 0;
	bool isOnGround = false;

	[SerializeField] GameObject fallHitbox;
	[SerializeField] GameObject viewCone;
	EnemyDetectFall enemyDetectFall;
    

	Rigidbody rb;

	private void Start()
	{
		rb = gameObject.GetComponent<Rigidbody>();
		enemyDetectFall = fallHitbox.GetComponent<EnemyDetectFall>();
	}


	void Death()
	{
		UnlockStand();
	}


	public void TakeDamage(int _damageToTake, int _forceToTake, Vector3 _currentRotationHit)
	{
		health -= _damageToTake;
		Debug.Log(health + " remaining");
		if (health <= 0)
		{
			Death();
		}
		//rb.AddRelativeForce(_currentRotationHit * _forceToTake);
		Vector3 _currentRotationHitRot = _currentRotationHit - transform.position;
		_currentRotationHitRot = -_currentRotationHitRot;
		rb.AddForceAtPosition(_currentRotationHitRot * _forceToTake, _currentRotationHit);

	} 

    /*void StandUp()
    {
		//reset velocity
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		//pause physics
		rb.isKinematic = true;
		Debug.Log("standup");
		Quaternion currentRot = gameObject.transform.rotation;
		Quaternion resetRot = Quaternion.Euler(0, 0, 0);
		//Quaternion resetRot = Quaternion.identity;
		transform.localRotation = Quaternion.Lerp(currentRot, resetRot, standRate * Time.deltaTime);
		rb.isKinematic = false; 
	}*/

    void CalculateCurrentRotation()
    {
		xRot = gameObject.transform.eulerAngles.x;
        yRot = gameObject.transform.eulerAngles.y;
        zRot = gameObject.transform.eulerAngles.z;

	
		isOnGround = enemyDetectFall.enemyOnGround;
	}

	void LockStand()
	{
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	void UnlockStand()
	{
		rb.constraints = RigidbodyConstraints.None;
	}

	//Pulls enemy towards raycast

	/*public void TakeDamage(int _damageToTake, int _forceToTake, Vector3 _currentRotationHit)
	{
		health -= _damageToTake;
		Debug.Log(health + " remaining");
		//rb.AddRelativeForce(_currentRotationHit * _forceToTake);
		Vector3 _currentRotationHitRot = _currentRotationHit - transform.position;
		rb.AddForceAtPosition(_currentRotationHitRot * _forceToTake, _currentRotationHit);
	}*/
}
