using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToController : InteractionObject {

    public bool hideControllerModel; //A flag to tell whether the controller’s model should be hidden once the player grabs this object
    public Vector3 snapPositionOffset; //The position added after snapping. The object snaps to the controller’s position by default
    public Vector3 snapRotationOffset; //Same as above, except this handles the rotation

    private Rigidbody rb; //A cached reference of this object’s Rigidbody component

    public override void Awake() //Overrides the base Awake() method, calls the base and caches the RigidBody component
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void ConnectToController(InteractionController controller) //Accept a controller as a parameter to connect to
    {
        cachedTransform.SetParent(controller.transform); //Set this object’s parent to be the controller

        cachedTransform.rotation = controller.transform.rotation; //Make this object’s rotation the same as the controller and add the offset
        cachedTransform.Rotate(snapRotationOffset); 
        cachedTransform.position = controller.snapColliderOrigin.position; ////Make this object’s position the same as the controller and add the offset
        cachedTransform.Translate(snapPositionOffset, Space.Self);

        rb.useGravity = false; //Disable the gravity on this object
        rb.isKinematic = true; //Make this object kinematic
    }

    private void ReleaseFromController(InteractionController controller) //Accept the controller to release as a parameter
    {
        cachedTransform.SetParent(null); //Unparent the object

        rb.useGravity = true; //Re-enable gravity and make the object non-kinematic again
        rb.isKinematic = false;

        rb.velocity = controller.velocity; //Apply the controller’s velocities to this object
        rb.angularVelocity = controller.angularVelocity;
    }

    public override void OnTriggerWasPressed(InteractionController controller) //Override OnTriggerWasPressed() to add the snap code
    {
        base.OnTriggerWasPressed(controller); //Call the base method

        if (hideControllerModel) //If the hideControllerModel flag was set, hide the controller model
        {
            controller.HideControllerModel();
        }

        ConnectToController(controller); //Connect this object to the controller
    }

    public override void OnTriggerWasReleased(InteractionController controller) //Override OnTriggerWasReleased() to add the release code
    {
        base.OnTriggerWasReleased(controller); //Call the base method

        if (hideControllerModel) //If the hideControllerModel flag was set, show the controller model again
        {
            controller.ShowControllerModel(); //Release this object to the controller
        }

        ReleaseFromController(controller);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
