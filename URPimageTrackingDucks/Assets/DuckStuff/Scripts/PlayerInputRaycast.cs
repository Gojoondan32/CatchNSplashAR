using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlayerInputRaycast : MonoBehaviour
{
    //private ARRaycastManager raycastManager;
    [SerializeField] private Camera arCamera;
    private float damageMultiplyer = 1f;

    private float cooldown = 0.5f;

    private void Awake()
    {
        //raycastManager = GetComponent<ARRaycastManager>();
        //InvokeRepeating("testSound", 10f, 4f);
    }

    private bool GetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private Vector2 PlayerTouch()
    {
        if(Input.touchCount > 0)
        {
            return Input.GetTouch(0).position;
        }
        return default;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0f)
        {
            cooldown = 0f;
            //Get the player touch position
            Ray ray = arCamera.ScreenPointToRay(PlayerTouch());
            RaycastHit hit;
            if (GameStates.instance.TrueGameStates == GameStates._GameStates.Game)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Duck")))
                {
                    cooldown = 0.5f;
                    CaptureDuck(hit);
                }
            }
            else if (GameStates.instance.TrueGameStates == GameStates._GameStates.Menu)
            {
                if (Physics.Raycast(ray, Mathf.Infinity, LayerMask.GetMask("PlayCube")))
                {
                    cooldown = 0.5f;
                    //Start the game
                    GameStates.instance.TrueGameStates = GameStates._GameStates.Game;
                    SceneSystem.instance.CheckGameStates();
                }
            }
        }


    }


    private void CaptureDuck(RaycastHit hit)
    {
        Debug.Log("Trying to hit ducks");
        //Player has hit a duck
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
    }

    private void testSound()
    {

        GameObject tempDuck = GameObject.FindGameObjectWithTag("Duck");
        tempDuck.GetComponent<AttemptCapture>().CaptureDuck(10, tempDuck);

    }
}
