using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDuck : DuckMovement
{
    RoyaltyWave royaltyWave;
    public GameObject forceField;
    private void Awake()
    {
        royaltyWave = GameObject.Find("RoyalWave").GetComponent<RoyaltyWave>();

        forceField = gameObject.transform.GetChild(1).gameObject;
    }
    public override void Update()
    {
        base.Update();
        //Override update to call another method
        ShieldDown();
    }


    public override void MoveDuck()
    {
        base.MoveDuck();
    }

    private void ShieldDown()
    {
        //Used to check if all the guards have been caught
        if (royaltyWave.royalList.Count <= 1)
        {
            forceField.SetActive(false);
        }
        else
        {
            forceField.SetActive(true);
        }
    }


}
