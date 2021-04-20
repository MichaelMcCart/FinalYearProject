using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiHoleTrigger : MonoBehaviour {

    public SkiBallScore ScoreBoard;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InteractionObject") {
            if (collision.gameObject.name == "Ball(Clone)")
            {
                Destroy(collision.gameObject);
                if (this.gameObject.tag == "PlusOne")
                {
                    ScoreBoard.PlusOne();
                    Debug.Log("PlusOne");
                }
                else if (this.gameObject.tag == "PlusTwo")
                {
                    ScoreBoard.PlusTwo();
                    Debug.Log("PlusTwo");
                }
                else if (this.gameObject.tag == "PlusFour")
                {
                    ScoreBoard.PlusFour();
                    Debug.Log("PlusFour");
                }
            }
        }
    }
}
