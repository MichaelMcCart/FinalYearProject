using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

    [ExecuteInEditMode]

    public Transform attachedArrow; //A reference to the BowArrow
    public SkinnedMeshRenderer BowSkinnedMesh; //Reference to the skinned mesh of the bow

    public float blendMultiplier = 255f; //The distance from the arrow to the bow
    public GameObject realArrowPrefab;//Reference to the RealArrow prefab

    public float maxShootSpeed = 50; //This is the velocity the arrow that gets shot
    public AudioClip fireSound; //The sound that will be played when an arrow gets shot

    bool IsArmed() //eturns true if the arrow is enabled
    {
        return attachedArrow.gameObject.activeSelf;
    }

    void Update () {
        float distance = Vector3.Distance(transform.position, attachedArrow.position); //Calculates the distance between the bow and the arrow
        BowSkinnedMesh.SetBlendShapeWeight(0, Mathf.Max(0, distance * blendMultiplier)); //Sets the bow’s blend shape to the distance calculated above
    }

    private void Arm() //Arm the bow by enabling the attached arrow
    {
        attachedArrow.gameObject.SetActive(true);
    }

    private void Disarm()
    {
        BowSkinnedMesh.SetBlendShapeWeight(0, 0); //Reset the bows bend
        attachedArrow.position = transform.position; //Resert the attached arrow's position
        attachedArrow.gameObject.SetActive(false); //Hide the arrow by disabling it.
    }

    private void OnTriggerEnter(Collider other) //Accept a Collider as a parameter
    {
        if(!IsArmed() && other.CompareTag("InteractionObject") && other.GetComponent<RealArrow>() && !other.GetComponent<InteractionObject>().IsFree()) //returns true if the bow is unarmed and is hit by a RealArrow
        {
            Destroy(other.gameObject); //Destroy the RealArrow
            Arm(); //Arm the bow
        }
    }

    public void ShootArrow()
    {
        GameObject arrow = Instantiate(realArrowPrefab, transform.position, transform.rotation); //Spawn a new RealArrow based on its prefab and set its position and rotation equal to that of the bow

        float distance = Vector3.Distance(transform.position, attachedArrow.position); //Calculate the distance between the bow and the attached arrow and store it in distance

        arrow.GetComponent<Rigidbody>().velocity = arrow.transform.forward * distance * maxShootSpeed; //Give the RealArrow a forward velocity based on distance
        AudioSource.PlayClipAtPoint(fireSound, transform.position); //Play the bow firing sound
        GetComponent<InteractionObject>().currentController.Vibrate(3500); //Vibrate the controller to give tactile feedback
        arrow.GetComponent<RealArrow>().Launch(); //Call the RealArrow’s Launch() method

        Disarm(); //Disarm the bow
    }
}
