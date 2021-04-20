using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialObjectSpawner : InteractionObject {

    public GameObject arrowPrefab;
    public List<GameObject> randomPrefabs = new List<GameObject>();

    private void SpawnObjectInHand(GameObject prefab, InteractionController controller) //prefab is the GameObject that will be spawned, while controller is the controller to which it will attach to
    {
        GameObject spawnedObject = Instantiate(prefab, controller.snapColliderOrigin.position, controller.transform.rotation); //Spawn a new GameObject with the same position and rotation as the controller
        controller.SwitchInteractionObjectTo(spawnedObject.GetComponent<InteractionObject>()); //Switch the controller’s active InteractionObject to the object that was just spawned
        OnTriggerWasReleased(controller); //“Release” the poc to give full focus to the spawned object
    }

    public override void OnTriggerWasPressed(InteractionController controller) //Overrides the base OnTriggerWasPressed() method
    {
        base.OnTriggerWasPressed(controller); //Calls the base OnTriggerWasPressed() method

        if (ControllerManager.Instance.AnyControllerIsInteractingWith<Bow>()) //If any of the controllers are holding a bow, spawns an arrow
        {
            SpawnObjectInHand(arrowPrefab, controller);
        }
        else //If not, spawns a random GameObject from the randomPrefabs list
        {
            SpawnObjectInHand(randomPrefabs[UnityEngine.Random.Range(0, randomPrefabs.Count)], controller);
        }
    }
}
