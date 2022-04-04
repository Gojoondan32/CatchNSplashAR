using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject pond;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator test()
    {
        yield return new WaitForSeconds(0f);
        Instantiate(pond, transform.position, Quaternion.identity);
    }
}
