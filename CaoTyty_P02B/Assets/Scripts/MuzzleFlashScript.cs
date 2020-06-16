using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
