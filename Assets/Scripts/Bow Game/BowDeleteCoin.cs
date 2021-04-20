using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowDeleteCoin : MonoBehaviour
{

    public Transform[] ArrowArray;
    public Rigidbody Arrow;
    //public SkiBallScore ScoreBoard;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InteractionObject")
        {
            if (collision.gameObject.name == "Coin" || collision.gameObject.name == "Coin(Clone)")
            {
                Destroy(collision.gameObject);

                for (int ArrowLoop = 0; ArrowLoop < ArrowArray.Length; ArrowLoop++)
                {
                    Rigidbody Arrowz_ = Instantiate(Arrow, ArrowArray[ArrowLoop].position, ArrowArray[ArrowLoop].rotation) as Rigidbody;
                }

                //ScoreBoard.ResetScore();

            }
        }
    }
}
