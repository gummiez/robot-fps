using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public GameObject muzzleFlash;
    [SerializeField] GameObject barrelLocation = null;
    Vector3 currentLocation;
    Quaternion currentRotation;


	public void SpawnMuzzleFlash()
    {
        currentLocation = barrelLocation.transform.position;
        currentRotation = gameObject.transform.rotation;
        Instantiate(muzzleFlash, currentLocation, currentRotation);
    }

}
