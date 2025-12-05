using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public Transform respawnPoint;
    public CharacterController controller;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            // Resetear velocidad del CharacterController
            controller.enabled = false;
            transform.position = respawnPoint.position;
            controller.enabled = true;
        }
    }
}
