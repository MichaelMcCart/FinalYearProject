using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {

    public static ControllerManager Instance; //A public static reference to this script so it can be called from all other scripts

    public InteractionController leftController; //Reference to the left controller
    public InteractionController rightController; //A reference to the right controller

    private void Awake()
    {
        Instance = this;
    }

    public bool AnyControllerIsInteractingWith<T>() //A generic method that accepts any type
    {
        if (leftController.InteractionObject && leftController.InteractionObject.GetComponent<T>() != null) //Left controller is interacting with an object and it has the requested component type attached, return true
        {
            return true;
        }

        if (rightController.InteractionObject && rightController.InteractionObject.GetComponent<T>() != null) //Right controller is interacting with an object and it has the requested component type attached, return true
        {
            return true;
        }

        return false; //If neither controller has the requested component attached, return false
    }
}
