using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingScript : MonoBehaviour
{
    [SerializeField] private Image fishingPoint;
    [SerializeField] private GameObject duck;
    [SerializeField] private Transform duckParent;

    [SerializeField] private float tempTimer = 4f;

    private float damageMultiplyer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CatchDuckTest());
    }

    /*
    private void Update(){
        tempTimer -= Time.deltaTime;
        if(tempTimer <= 0){
            StartCoroutine(CatchDuckTest());
            tempTimer = 4f;
        }
    }
    */

    private IEnumerator CatchDuckTest(){
        yield return new WaitForSeconds(0f);
        GameObject tempDuck = GameObject.FindGameObjectWithTag("Duck");
        AttemptCapture attemptCapture = tempDuck.GetComponent<AttemptCapture>();

        if(tempDuck.gameObject.name == "CrystalDuckProper(Clone)"){
            attemptCapture.CaptureCrystalDuck(damageMultiplyer, tempDuck);
        }
        else{
            
            attemptCapture.CaptureDuck(damageMultiplyer, tempDuck);
        }

        
    }

    private void Catch(Transform duckCaught){
        DuckMovement duckMovement = duckCaught.GetComponent<DuckMovement>();
        //Stop the duck from moving to the next waypoint
        //duckMovement.canMove = false;
        
    }

    //Called from the onClick event for the fish button
    public void FishDuck(){
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(fishingPoint.transform.position.x, fishingPoint.transform.position.y, 0f));
        RaycastHit hit;
        LayerMask duckMask = LayerMask.GetMask("Duck");

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, duckMask))
        {
            AttemptCapture attemptCapture = hit.collider.gameObject.GetComponent<AttemptCapture>();
            DuckHealth duckHealth = hit.collider.gameObject.GetComponent<DuckHealth>();

            if (!duckHealth.IsDead)
            {
                if (hit.collider.gameObject.name == "CrystalDuckProper(Clone)")
                {
                    attemptCapture.CaptureCrystalDuck(damageMultiplyer, hit.collider.gameObject);
                }
                else
                {
                    attemptCapture.CaptureDuck(damageMultiplyer, hit.collider.gameObject);
                }
            }



            //DuckMovement duckMovement = hit.collider.gameObject.GetComponent<DuckMovement>();
            //duckMovement.AttemptCapture(damageMultiplyer, hit.collider.gameObject);

        }
    }
}
