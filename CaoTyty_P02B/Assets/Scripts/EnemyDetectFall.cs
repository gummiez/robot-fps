using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectFall : MonoBehaviour
{

    BoxCollider bC;
    public bool enemyOnGround = false;

    private void Start()
    {
        bC = gameObject.GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            enemyOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemyOnGround = false;
    }
}
