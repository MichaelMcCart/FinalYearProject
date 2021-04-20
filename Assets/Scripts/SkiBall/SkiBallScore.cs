using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkiBallScore : MonoBehaviour {
     
    [HideInInspector]
    public int StartSkiBallScore = 0;
    public int PlayerSkiBallScore;
    public Text SkiBallScoreText;

	// Use this for initialization
	void Start () {
        PlayerSkiBallScore = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SkiBallScoreText.text = "" + PlayerSkiBallScore; 
	}

    public void PlusOne () {
        PlayerSkiBallScore = PlayerSkiBallScore + 1;
    }

    public void PlusTwo() {
        PlayerSkiBallScore = PlayerSkiBallScore + 2;
    }

    public void PlusFour()
    {
        PlayerSkiBallScore += 4;
    }

    public void ResetScore()
    {
        PlayerSkiBallScore = StartSkiBallScore;
    }
}
