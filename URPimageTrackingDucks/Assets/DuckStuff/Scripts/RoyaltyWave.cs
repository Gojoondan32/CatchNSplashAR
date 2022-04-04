using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyaltyWave : MonoBehaviour
{
    [SerializeField] private GameObject waveSpawner;
    [SerializeField] private float royaltyTimer = 16f;

    [SerializeField] private GameObject CrystalDuck;
    [SerializeField] private GameObject royalGuardDuck;
    private int royalCount = 4;
    public List<GameObject> royalList;
    

    [Header("Booleans")]
    private bool canSpawn = true;
    private bool royaltyOver = false;
    private void Start()
    {
        canSpawn = false;
        royaltyOver = false;
        royalList = new List<GameObject>();
    }

    private void Update()
    {
        
        if(Global.instance.royaltyWaveBeginning == false && GameStates.instance.TrueGameStates == GameStates._GameStates.Game)
        {
            royaltyTimer -= Time.deltaTime;
        }


        
        if(royaltyTimer <= 0f)
        {
            royaltyTimer = 16f;
            int index = Random.Range(0, 3);
            Debug.Log(index.ToString());
            if (index == 1 && !canSpawn)
            {
                //Stop the main wave spawning 
                Global.instance.royaltyWaveBeginning = true;
                canSpawn = true;
                //Begin the royal wave
                BeginRoyalWave();
                
            }

        }

        if (RoyaltyOver())
        {
            Global.instance.royaltyWaveBeginning = false;
            
        }
        UpdateRoyalList();

    }
    private void BeginRoyalWave()
    {
        Debug.Log("Running");
        GameObject[] duckAlive = GameObject.FindGameObjectsWithTag("Duck");
        foreach (GameObject duck in duckAlive)
        {
            //Destory all the ducks currently in the scene
            Destroy(duck);
        }
        //Call duck list funciton to remove all the ducks from the list
        waveSpawner.GetComponent<WaveSpawner>().UpdateDuckList();

        canSpawn = false;
        SpawnRoyalDucks();
        
    }

    private void SpawnRoyalDucks()
    {
        
        for(int i = 0; i < royalCount; i++)
        {
            GameObject tempRoyal = Instantiate(royalGuardDuck, ReturnPosition(), Quaternion.identity);
            royalList.Add(tempRoyal);
        }
        GameObject tempCrystal = Instantiate(CrystalDuck, ReturnPosition(), Quaternion.identity);
        royalList.Add(tempCrystal);
    }

    private Vector3 ReturnPosition()
    {
        Vector3 position;
        //Get a random value between the boundries
        float x = Random.Range(Global.instance.startBoundryX.position.x, Global.instance.endBoundryX.position.x);
        float z = Random.Range(Global.instance.startBoundryZ.position.z, Global.instance.endBoundryZ.position.z);

        position = new Vector3(x, transform.position.y, z);
        return position;
    }

    private bool RoyaltyOver()
    {
        if(royalList.Count <= 0)
        {
            return true;
        }
        return false;
    }

    private void UpdateRoyalList()
    {
        for(int i = royalList.Count - 1; i >= 0; i--)
        {
            if(royalList[i] == null)
            {
                royalList.Remove(royalList[i]);
            }
        }
    }
}
