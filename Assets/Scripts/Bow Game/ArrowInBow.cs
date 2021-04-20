using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInBow : InteractionObject {

    public float minimumPosition; //The minimum position on the Z-axis
    public float maximumPosition; //The maximum position on the Z-axis

    private Transform attachedBow; //A reference to the Bow this arrow is attached to
    private const float arrowCorrenction = 0.3f; //Correct the arrow position relative to the bow

    public override void Awake() //This calls the base class’ Awake() method to cache the transform and stores a reference to the bow in the attachedBow variable
    {
        base.Awake();
        attachedBow = transform.parent;
    }

    public override void OnTriggerIsBeingPressed(InteractionController controller) //Override OnTriggerIsBeingPressed() and get the controller that’s interacting with this arrow as a parameter
    {
        base.OnTriggerIsBeingPressed(controller); //Call the base method

        Vector3 arrowInBowSpace = attachedBow.InverseTransformPoint(controller.transform.position); //Get the arrow’s new position relative to the position of the bow
        cachedTransform.localPosition = new Vector3(0, 0, arrowInBowSpace.z + arrowCorrenction); //Move the arrow to its new position and add the arrowCorrection to it on its Z-axis to get the correct value
    }

    public override void OnTriggerWasReleased(InteractionController controller) //Override the OnTriggerWasReleased() method and get the controller that’s interacting with this arrow as a parameter
    {
        attachedBow.GetComponent<Bow>().ShootArrow(); //Shoot the arrow
        currentController.Vibrate(3500); //Vibrate the controller
        base.OnTriggerWasReleased(controller); //Call the base method to clear the currentController
    }

    private void LateUpdate()
    {
        float zPos = cachedTransform.localPosition.z; //Store the Z-axis position of the arrow in zPos
        zPos = Mathf.Clamp(zPos, minimumPosition, maximumPosition); //Clamp zPos between the minimum and maximum allowed position
        cachedTransform.localPosition = new Vector3(0, 0, zPos); //Reapply the position of the arrow with the clamped value

        cachedTransform.localRotation = Quaternion.Euler(Vector3.zero); //Limit the rotation of the arrow to Vector3.zero

        if (currentController)
        {
            currentController.Vibrate(System.Convert.ToUInt16(500 * -zPos)); //Vibrate the controller. The more the arrow is pulled back, the more intense the vibration will be
        }
    }


}