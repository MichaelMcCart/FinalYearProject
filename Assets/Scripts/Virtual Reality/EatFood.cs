using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour {

    public AudioClip EatingFood;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "InteractionObject")
        {
            if (collision.gameObject.name == "food")
            {
                Destroy(collision.gameObject);
                AudioSource.PlayClipAtPoint(EatingFood, transform.position);
            }
        }
    }

}
