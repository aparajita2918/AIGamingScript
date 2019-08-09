using UnityEngine;
using System.Collections;
//
using System;
using System.Linq;
/*  notes of all available methods 
      *  void moveUp()
      *  void moveDown() 
      *  void push()
      *  float getCharacterLocation()
      *  float getOpponentLocation()
      *  float[] getButtonLocations() --Returns an array of floats for representing the position of each button on YOUR SIDE
      *  float[] getButtonCooldowns()  --time remaining before button can be pressed again
      *  bool[] getBeltDirection()
      *  float[] getBombDistances()
      *  float getPlayerSpeed()  -- speed at which char moves
      *  float getBombSpeed()
      */


public class AIScript_AparajitaPathak : MonoBehaviour {

    public CharacterScript mainScript;

    public float[] bombSpeeds;
    public float[] buttonCooldowns;
    public float playerSpeed;
    public int[] beltDirections;
    public float[] buttonLocations;
      //public float getOpponentLocation();
    public float[] bombDistances;
    public float playerLoc;
    public float score;
    public int targetAction;
    float[] distancePtoB = new float[8];
    public float[] scores = new float[8];
   

    // Use this for initialization
    void Start () {
        mainScript = GetComponent<CharacterScript>();

        if (mainScript == null)
        {
            print("No CharacterScript found on " + gameObject.name);
            this.enabled = false;
        }

        buttonLocations = mainScript.getButtonLocations();

        playerSpeed = mainScript.getPlayerSpeed();
	}

	// Update is called once per frame
	void Update () {

        buttonCooldowns = mainScript.getButtonCooldowns();
        beltDirections = mainScript.getBeltDirections();
       



        //Your AI code goes here
        /************************************************************************/
        bombSpeeds = mainScript.getBombSpeeds();
        bombDistances = mainScript.getBombDistances();
        playerLoc = mainScript.getCharacterLocation();
       
        for (int i = 0; i < 8; i++)
        {       
            distancePtoB[i] = Math.Abs((buttonLocations[i] - playerLoc) / 2); 
        }
        score = 0;
        for (int i = 0; i < 8; i++)
        {   
            if (beltDirections[i] == -1)
                score += (10 - bombDistances[i] / 2);    
            else if (beltDirections[i] == 0)
                score += 1;
            else if (buttonCooldowns[i] >= 1)
                score -= 2;
            else if (bombSpeeds[i] >= 3 && beltDirections[i] == -1)  //	-1 means its moving toward my side
                score += 3;
            score /= (distancePtoB[i] / 2); 

            scores[i] = score;
            score = 0;
        }
        targetAction = Array.IndexOf(scores, scores.Max());   
        if (buttonLocations[targetAction] + .1 >=playerLoc)
        {
            if (distancePtoB[targetAction] <= 1)   
                mainScript.push();
            mainScript.moveUp();
        }
        else if (buttonLocations[targetAction] - .1 <=playerLoc)
        {
            if (distancePtoB[targetAction] <= 1)
                mainScript.push();
            mainScript.moveDown();
        }

        /**************************************************************************************/

    }


}
