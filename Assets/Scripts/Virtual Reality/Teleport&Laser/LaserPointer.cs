using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    public GameObject laserPrefab; //This is a reference to the Laser’s prefab
    private GameObject laser; //laser stores a reference to an instance of the laser
    private Transform laserTransform; //The transform component is stored for ease of use
    private Vector3 hitPoint; //This is the position where the laser hits

    public Transform cameraRigTransform; //Is the transform of [CameraRig]
    public GameObject teleportReticlePrefab; //Stores a reference to the teleport reticle prefab
    private GameObject reticle; //A reference to an instance of the reticle
    private Transform teleportReticleTransform; //Stores a reference to the teleport reticle transform for ease of use
    public Transform headTransform; //Stores a reference to the player’s head (the camera)
    public Vector3 teleportReticleOffset; //Is the reticle offset from the floor, so there’s no “Z-fighting” with other surfaces
    public LayerMask teleportMask; //Is a layer mask to filter the areas on which teleports are allowed
    private bool shouldTeleport; //Is set to true when a valid teleport location is found

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    void Awake()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true); //Show the laser
        laserTransform.position = Vector3.Lerp(trackedObject.transform.position, hitPoint, .5f); //Position the laser between the controller and the point where the raycast hits
        laserTransform.LookAt(hitPoint); //Point the laser at the position where the raycast hit
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance); //Scale the laser so it fits perfectly between the two positions
    }

    void Start()
    {
        laser = Instantiate(laserPrefab); //Spawn a new laser and save a reference to it in laser
        laserTransform = laser.transform; //Store the laser’s transform component

        reticle = Instantiate(teleportReticlePrefab); //Spawn a new reticle and save a reference to it in reticle
        teleportReticleTransform = reticle.transform; //Store the reticle’s transform component
    }

    void Update () {
		if(Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) //If the touchpad is held down do this
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObject.transform.position, transform.forward, out hit, 100, teleportMask)) //Shoot a ray from the controller. If it hits something, make it store the point where it hit and show the laser
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                reticle.SetActive(true); //Show the teleport reticle
                teleportReticleTransform.position = hitPoint + teleportReticleOffset; //Move the reticle to where the raycast hit with the addition of an offset to avoid Z-fighting
                shouldTeleport = true; //Set shouldTeleport to true to indicate the script found a valid position for teleporting
            }
        }
        else
        {
            laser.SetActive(false); //Hide the laser when the player released the touchpad
            reticle.SetActive(false); //Hides the reticle in the absence of a valid target
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) //This teleports the player if the touchpad is released and there’s a valid teleport position
        {
            Teleport();
        }
	}

    private void Teleport()
    {
        shouldTeleport = false; //Set the shouldTeleport flag to false when teleportation is in progress
        reticle.SetActive(false); //Hide the reticle
        Vector3 difference = cameraRigTransform.position - headTransform.position; //Calculate the difference between the positions of the camera rig’s center and the player’s head
        difference.y = 0; //Reset the y-position for the above difference to 0
        cameraRigTransform.position = hitPoint + difference; //Move the camera rig to the position of the hit point and add the calculated difference
    }
}
