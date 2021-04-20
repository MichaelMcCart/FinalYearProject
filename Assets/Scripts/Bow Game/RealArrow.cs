using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealArrow : MonoBehaviour {

    public BoxCollider pickupCollider; //The arrows have two colliders: one trigger to detect collisions when fired and a regular one that’s used for physical interaction and picking the arrow up again once fired
    private Rigidbody rb; //A cached reference to this arrow’s Rigidbody
    private bool launched; //Gets set to true when an arrow is launched from the bow
    private bool stuckInWall; //Will be set to true when this arrow hits a solid object

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (launched && !stuckInWall && rb.velocity != Vector3.zero) //If this arrow is launched
        {
            rb.rotation = Quaternion.LookRotation(rb.velocity); //Look in the direction of the velocity
        }
    }

    public void SetAllowPickup(bool allow) //Method that enables or disables the pickupCollider
    {
        pickupCollider.enabled = allow;
    }

    public void Launch() //Called when the arrow gets shot by the bow
    {
        launched = true;
        SetAllowPickup(false);
    }

    private void GetStuck(Collider other) //Gets a Collider as a parameter. This is what the arrow will attach itself to
    {
        launched = false;
        rb.isKinematic = true; //Make the arrow kinematic so it’s not affected by physics
        stuckInWall = true; //Sets the stuckInWall flag to true
        SetAllowPickup(true); //Allow picking this arrow as it stopped moving
        transform.SetParent(other.transform); //Parent the arrow to the object it hit
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller") || other.GetComponent<Bow>()) //If this arrow hit a controller or a bow, don’t get stuck
        {
            return;
        }

        if(launched && !stuckInWall) //This arrow was launched and isn’t stuck yet, so attach it to the object it hit
        {
            GetStuck(other);
        }
    }


}

