using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;

    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerupController powCon = other.GetComponent<PowerupController>();

        // If the other object has a PowerupController
        if (powCon != null)
        {
            AudioSource.PlayClipAtPoint(AudioController.instance.pickingUp, AudioController.instance.transform.position);
            // Add the powerup
            powCon.Add(powerup);
            GameManager.instance.pickups.Remove(gameObject);

            // Destroy this pickup
            Destroy(gameObject);
        }
    }

}
