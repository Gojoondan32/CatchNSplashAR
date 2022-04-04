using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveState { spawning, waiting, counting, royalty}

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] prefab;
        public string name;

    }

    public Wave wave;
    protected WaveState waveState;

    public List<GameObject> duckList;
    [SerializeField] private int maxDuckValue = 5;

    [SerializeField] private float timeBetweenWaves = 4f;



    // Start is called before the first frame update
    void Start()
    {
        Global.instance.royaltyWaveBeginning = false;
        duckList = new List<GameObject>();
        if(duckList.Count <= maxDuckValue)
        {
            waveState = WaveState.waiting;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDuckList();
        if (waveState == WaveState.waiting && !Global.instance.royaltyWaveBeginning && GameStates.instance.TrueGameStates == GameStates._GameStates.Game)
        {
            AttemptToSpawn();
            return;
        }
        
    }

    private IEnumerator SpawnDuck()
    {
        waveState = WaveState.counting;
        yield return new WaitForSeconds(timeBetweenWaves);
        GameObject tempDuck = Instantiate(ChooseDuckType(), ReturnPosition(), Quaternion.identity);
        duckList.Add(tempDuck);
        
        

        waveState = WaveState.waiting;
        //yield break;
    }

    private void AttemptToSpawn()
    {
        if (!DuckLimitReached())
        {
            Debug.Log("In for loop");
            StartCoroutine(SpawnDuck());
        }
    }

    private bool DuckLimitReached()
    {
        if(duckList.Count >= maxDuckValue)
        {

            return true;
        }
        return false;
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

    public void UpdateDuckList()
    {
        //Loop backwards through the list and remove any ducks that are no longer there
        for (int i = duckList.Count - 1; i >= 0; i--)
        {
            if (duckList[i].gameObject == null)
            {
                duckList.Remove(duckList[i]);
            }

        }
    }

    private GameObject ChooseDuckType()
    {
        int index = Random.Range(0,21);

        if (index <= 15)
            return wave.prefab[0];
        else if (index > 15 && index <= 20)
            return wave.prefab[1];
        else
            return null;
        
    }


}
