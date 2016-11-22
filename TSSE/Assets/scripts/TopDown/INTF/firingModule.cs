using UnityEngine;
using System.Collections;

// The firingModule interface provides the functionality required of 
// any new firing module.
public interface FiringModule {
    // Attempt to fire the equipped weapon
    void fire();

    // This is the distance between the ship and target for which
    // firing presents a reasonable chance of successful impact
    // This is most useful for the AI to know when to fire
    float getEffectiveDistance();

    // Similar to getEffectiveDistance, this is the angle between
    // the ship and target vectors
    float getEffectiveAngle();

    // True if the weapon has ammunition in its clip and is not
    // on cooldown
    // This is useful if the AI wants to implement different behaviour
    // during cooldown
    bool canFire();
}
