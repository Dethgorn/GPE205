using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup
{
    public float duration;
    public int damageTaken;
    public bool isPermanent;
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;

    public void OnActivate(TankData target)
    {
        target.forwardSpeed += speedModifier;
        target.health += healthModifier;
        target.maxHealth += maxHealthModifier;
        target.shotDamage -= damageTaken;
    }

    public void OnDeactivate(TankData target)
    {
        target.forwardSpeed -= speedModifier;
        target.health -= healthModifier;
        target.maxHealth -= maxHealthModifier;
        target.shotDamage += damageTaken;
    }

}
