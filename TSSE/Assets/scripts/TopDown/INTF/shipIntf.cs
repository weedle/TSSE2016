using UnityEngine;
using System.Collections;

// This is the interface for any ship.
// It details all the actions a ship can take, which action is chosen
// is decided by the ShipController
public interface ShipIntf {

    ShipDefinitions.SState getState();

    float getAngle();

    void brake();

    void start();

    void stop();

    void move(float vertical);

    void rotate(float horizontal);

    void fire();

    float getEffectiveDistance();

    float getEffectiveAngle();

    string getName();

    void isHit(float damage);

    float getHealthPercent();

    void setText(string newText);

    bool getActive();

    void pause();

    void unpause();
}
