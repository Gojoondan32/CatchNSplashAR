using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttemptCapture : MonoBehaviour
{
    [SerializeField] private int score = 1;

    public void CaptureDuck(float damage, GameObject duck){
        //Get duck health from the duck
        DuckHealth duckHealth = duck.gameObject.GetComponent<DuckHealth>();
        duckHealth.DamageCalculator(damage);
        DuckMovement duckMovement = duck.GetComponent<DuckMovement>();

        //Check if the duck has died
        if (duckHealth.IsDead){
            Debug.Log("Duck is dead");
            //Set the collider to trigger so it doesn't drag other ducks with it
            duck.GetComponent<BoxCollider>().isTrigger = true;
            
            //Set the duck state to captured
            duckMovement.duckStates = DuckStates.captured;

            //Update the score value on the canvas
            ScoreTracker.instance.Score += score;
            FindObjectOfType<AudioManager>().PlaySounds("DuckCaught");
        }
        else
        {
            duckMovement.duckStates = DuckStates.enraged;
        }

    }

    public void CaptureCrystalDuck(float damage, GameObject crystalDuck)
    {
        //Check if the forcefield is innactive 
        if(crystalDuck.GetComponent<CrystalDuck>().forceField.activeSelf == false){
            //Inflict damage
            DuckHealth duckHealth = crystalDuck.GetComponent<DuckHealth>();
            duckHealth.DamageCalculator(damage);

            if(duckHealth.IsDead){
                CrystalDuck newCrystal = crystalDuck.GetComponent<CrystalDuck>();
                newCrystal.duckStates = DuckStates.captured;
                ScoreTracker.instance.Score += score;
            }
        }
        
    }
}


