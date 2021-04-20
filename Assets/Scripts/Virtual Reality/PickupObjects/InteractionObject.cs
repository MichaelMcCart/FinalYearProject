using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{

    protected Transform cachedTransform; //cache the value of the transform to improve performance
    [HideInInspector] //This attribute makes the variable underneath invisible in the Inspector window
    public InteractionController currentController; //This is the controller this object is currently interacting with

    public virtual void OnTriggerWasPressed(InteractionController controller)
    {
        currentController = controller;
    }

    public virtual void OnTriggerIsBeingPressed(InteractionController controller)
    {
    }

    public virtual void OnTriggerWasReleased(InteractionController controller)
    {
        currentController = null;
    }
    //These methods will be called by the controller when its trigger is either pressed, held or released
    public virtual void Awake()
    {
        cachedTransform = transform; //Cache the transform for better performance
        if (!gameObject.CompareTag("InteractionObject")) //Check to see if this InteractionObject has the proper tag assigned
        {
            Debug.LogWarning("This InteractionObject does not have the correct tag, setting it now.", gameObject); //Loggs a warning to let me know if I've forgotten to add a tag
            gameObject.tag = "InteractionObject"; //Assign the quickly so this object functions as expected
        }
    }

    public bool IsFree() //This is a public Boolean that indicates whether or not this object is currently in use by a controller
    {
        return currentController == null;
    }

    public virtual void OnDestroy() //When this object gets destroyed, you release it from the current controller
    {
        if (currentController)
        {
            OnTriggerWasReleased(currentController);
        }
    }
}
