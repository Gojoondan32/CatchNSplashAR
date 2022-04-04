using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DuckStates { swimming, idle, enraged, captured}
[RequireComponent(typeof(Rigidbody))]
public class DuckMovement : MonoBehaviour
{
    //-----------------------------------------------------
    /* Notes
     * Create an enum here - public enum DuckStates{moving, running, idle}
     * Use a state machine to determine which state 
     * 
     * Fix traffic light system
     */

    public DuckStates duckStates = DuckStates.swimming;

    private Rigidbody rb;
    [SerializeField] private float force = 1f;
    [SerializeField] private float turnSpeed = 180f;
    protected Vector3 position = Vector3.zero;
    public bool canMove = true;

    [SerializeField] private float decelerationModifier = 5f;
    private Transform duckParent;

    private float duckTimer = 3f;

    [SerializeField] private int scoreValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Destroy(gameObject, 10f);
        canMove = true;

        duckParent = GameObject.FindGameObjectWithTag("DuckParent").transform;

        RandomizePosition();

        //StartCoroutine(DestroyDuck());
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if ((position - transform.position).magnitude <= 0.1f)
        {
            RandomizePosition();
        }
    }

    private void FixedUpdate()
    {
        switch (duckStates)
        {
            case DuckStates.swimming:
                MoveDuck();
                break;
            case DuckStates.idle:
                Debug.Log("Waiting");
                DuckWaitingTime();
                break;
            case DuckStates.enraged:
                StartCoroutine(EnragedDuck());
                Debug.Log("Enraged");
                break;
            case DuckStates.captured:
                DuckCaptured();
                break;
            default:
                Debug.Log("No valid states given");
                break;
        }
    }

    public Vector3 RandomizePosition()
    {
        //Get a random value between the boundries

        float x = Random.Range(Global.instance.startBoundryX.position.x, Global.instance.endBoundryX.position.x);
        float z = Random.Range(Global.instance.startBoundryZ.position.z, Global.instance.endBoundryZ.position.z);

        return position = new Vector3(x, transform.position.y, z);

    }

    public virtual void MoveDuck()
    {
        Vector3 angle = position - transform.position;
        if (angle.y != 0)
            angle.y = 0;

        if (position != Vector3.zero)
        {
            //Move the duck to the target position
            transform.position = Vector3.MoveTowards(transform.position, position, force * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(angle), turnSpeed * Time.deltaTime);
        }
    }

    private IEnumerator EnragedDuck()
    {
        force += 0.2f;
        duckStates = DuckStates.swimming;
        yield return new WaitForSeconds(4f);
        force -= 0.2f;
        
    }

    private void DuckWaitingTime()
    {
        duckTimer -= Time.deltaTime;
        if(duckTimer <= 0f)
        {
            //Move the ducks underneath 
            duckTimer = 3f;
            
            duckStates = DuckStates.swimming;
        }
    }



    private void DuckCaptured()
    {
        rb.useGravity = false;
        //Get the direction to the parent
        Vector3 directionToParent = duckParent.transform.position - transform.position;
        transform.SetParent(duckParent);
        Debug.Log("Parent set by: " + gameObject.name.ToString());
        rb.AddForce(directionToParent * (force * 2), ForceMode.Impulse);

    }



    
    private void OnTriggerEnter(Collider other){
        Debug.Log("Collision detected");
        if(other.gameObject.CompareTag("DuckParent"))
        {
            //Destroy the duck
            Destroy(gameObject);
        }
    }

}
