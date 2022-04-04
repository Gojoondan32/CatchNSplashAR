using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckKing : MonoBehaviour
{
    private void Awake()
    {
        transform.position += new Vector3(transform.position.x, -1.5f, 0.5f);
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f, 0f));
        //transform.position = new Vector3(transform.position.x, -0.2f, 0.5f);
        //transform.position = new Vector3(transform.position.x, -1f, 0.5f);
    }
}
