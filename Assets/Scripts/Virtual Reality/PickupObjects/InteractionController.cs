using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

    public GameObject ControllerModel; //his is the visual representation of the controller

    [HideInInspector]
    public Vector3 velocity; //This is the speed and direction of the controller
    [HideInInspector]
    public Vector3 angularVelocity; //This is the rotation spped of the controller
    protected InteractionObject objectBeingInteractedWith; //The InteractionObject this controller is currently interacting with

    public InteractionObject InteractionObject //This returns the InteractionObject this controller is currently interacting with
    {
        get { return objectBeingInteractedWith; }
    }

    public Transform snapColliderOrigin; //Save a reference to the tip of the controller

    private SteamVR_TrackedObject trackedObject; //SteamVR_TrackedObject can be used to get a reference to the actual controller

    private SteamVR_Controller.Device Controller //handy shortcut to the actual SteamVR controller class
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    void Awake() //Reference to the TrackedObject component attached to this controller
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private void CheckForInteractionObject()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(snapColliderOrigin.position, snapColliderOrigin.lossyScale.x / 2f); //Creates a new array of colliders and fills it with all colliders found by OverlapSphere()

        foreach (Collider overlappedCollider in overlappedColliders) //Iterates over the array
        {
            if (overlappedCollider.CompareTag("InteractionObject") && overlappedCollider.GetComponent<InteractionObject>().IsFree()) //If any of the found colliders has an InteractionObject tag then continue
            {
                objectBeingInteractedWith = overlappedCollider.GetComponent<InteractionObject>(); //Saves a reference to the InteractionObject
                objectBeingInteractedWith.OnTriggerWasPressed(this); //Calls OnTriggerWasPressed on objectBeingInteractedWith and gives it the current controller as a parameter
                return; //Breaks out of the loop once an InteractionObject is found
            }
        }
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown()) //When the trigger is pressed, call CheckForInteractionObject() to prepare for a interaction
        {

            CheckForInteractionObject();
        }

        if (Controller.GetHairTrigger()) //While the trigger is down and an object is being interacted with it call that object
        {
            if (objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnTriggerIsBeingPressed(this);
            }
        }
        if (Controller.GetHairTriggerUp()) //When the trigger is released and there’s an object that’s being interacted with, call that object’s OnTriggerWasReleased() and stop interacting with it
        {
            if (objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnTriggerWasReleased(this);
                objectBeingInteractedWith = null;
            }
        }
    }

    private void UpdateVelocity() //FixedUpdate() calls UpdateVelocity() every frame at the fixed framerate, which updates the velocity and angularVelocity variables
    {
        velocity = Controller.velocity;
        angularVelocity = Controller.angularVelocity;
    }

    private void FixedUpdate()
    {
        UpdateVelocity();
    }

    //Methods enable or disable the GameObject representing the controller

    public void Vibrate(ushort strength) //Makes the controller Vibrate
    {
        Controller.TriggerHapticPulse(strength);
    }

    public void SwitchInteractionObjectTo(InteractionObject interactionObject) //Switches the active InteractionObject to the one specified in the parameter
    {
        objectBeingInteractedWith = interactionObject; //This makes the specified InteractionObject the active one
        objectBeingInteractedWith.OnTriggerWasPressed(this); //Call OnTriggerWasPressed() on the newly assigned InteractionObject and pass this controller
    }

    public void HideControllerModel()
    {
        ControllerModel.SetActive(false);
    }

    public void ShowControllerModel()
    {
        ControllerModel.SetActive(true);
    }

}

