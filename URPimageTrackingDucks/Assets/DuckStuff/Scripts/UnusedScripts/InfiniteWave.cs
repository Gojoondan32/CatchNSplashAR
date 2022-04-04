using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteWave : MonoBehaviour
{
    public enum SpawnState { waiting, spawning, counting};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] typeOfenemies;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnpoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.counting;

    
    private Wave currentWave;


    private Transform previousSpawn;
    // Start is called before the first frame update
    void Start()
    {
        previousSpawn = null;
        if (spawnpoints.Length == 0)
        {
            Debug.Log("No spawn points");
        }
        Debug.Log("Starting");
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = waves[nextWave];
        if (state == SpawnState.waiting)
        {
            //Check if current wave is alive
            if (!EnemyIsAlive())
            {
                //Begin new wave
                BeginNewWave();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                //Start spawning waves
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void BeginNewWave()
    {
        Debug.Log("Wave complete");

        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Completed all waves! Looping...");
        }
        else
        {
            nextWave++;
        }


    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave" + _wave.name);
        state = SpawnState.spawning;

        //Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy()
    {
        
        GameObject newEnemy = currentWave.typeOfenemies[Random.Range(0, currentWave.typeOfenemies.Length)];
        

        //Choose random spawn
        Transform _sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
        while(previousSpawn == _sp)
        {
            _sp = spawnpoints[Random.Range(0, spawnpoints.Length)];
            
        }

        previousSpawn = _sp;
        GameObject tempEnemy;
        tempEnemy = (GameObject)Instantiate(newEnemy, _sp.position, Quaternion.identity);


    }
}
