using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrab : InteractionObject
{

    public bool hideControllerModelOnGrab; //Controller model should be hidden when this object is picked up
    private Rigidbody rb; //Cache the Rigidbody component for performance and ease of use

    public override void Awake()
    {
        base.Awake(); //Call Awake() on the base class RWVR_InteractionObject
        rb = GetComponent<Rigidbody>(); //Store the attached Rigidbody component
    }

    private void AddFixedJointToController(InteractionController controller) //This method accepts a controller to “stick” to as a parameter and then proceeds to create a FixedJoint component
    {
        FixedJoint fx = controller.gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        fx.connectedBody = rb;
    }

    private void RemoveFixedJoint(InteractionController controller) //The controller passed as a parameter is relieved from its FixedJoint component
    {
        if (controller.gameObject.GetComponent<FixedJoint>())
        {
            FixedJoint fx = controller.gameObject.GetComponent<FixedJoint>();
            fx.connectedBody = null;
            Destroy(fx);
        }
    }

    public override void OnTriggerWasPressed(InteractionController controller) //Override the base OnTriggerWasPressed() method
    {
        base.OnTriggerWasPressed(controller); //Call the base method to intialize the controller

        if (hideControllerModelOnGrab) //f the hideControllerModelOnGrab flag was set, hide the controller model
        {
            controller.HideControllerModel();
        }

        AddFixedJointToController(controller); //Add a FixedJoint to the controller
    }

    public override void OnTriggerWasReleased(InteractionController controller) //Override the base OnTriggerWasReleased() method
    {
        base.OnTriggerWasReleased(controller); //Call the base method to unassign the controller

        if (hideControllerModelOnGrab) //If the hideControllerModelOnGrab flag was set, show the controller model again
        {
            controller.ShowControllerModel();
        }

        rb.velocity = controller.velocity; //Pass the controller’s velocity and angular velocity to this object’s rigidbody
        rb.angularVelocity = controller.angularVelocity;

        RemoveFixedJoint(controller); //Remove the FixedJoint


    }
}
