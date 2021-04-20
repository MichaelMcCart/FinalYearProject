using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject; //A reference to the object being tracked (the controller)
    private SteamVR_Controller.Device Controller //A device property to provide easy access to the controller
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    private void Awake()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void Update (){

        if (Controller.GetAxis() != Vector2.zero) //Get the position of the finger when it’s on the touchpad and write it to the Console
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }
        if (Controller.GetHairTriggerDown()) //When you squeeze the hair trigger, this line writes to the Console
        {
            Debug.Log(gameObject.name + "Trigger Press");
        }
        if (Controller.GetHairTriggerUp()) //If you release the hair trigger, this if statement writes to the Console
        {
            Debug.Log(gameObject.name + "Trigger Released");
        }
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) //If you press a grip button, this section writes to the Console
        {
            Debug.Log(gameObject.name + "Grip Press");
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) //When you release one of the grip buttons, this writes that action to the Console
        {
            Debug.Log(gameObject.name + "Grip Released");
        }
    }
}
