using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public static SceneSystem instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject background;

    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject gameScene;
    [SerializeField] private GameObject endScene;
    //Called from the on click event
    public void ChangeScene(string scene)
    {
        switch (scene)
        {
            case "MainScene":
                LeanTween.scale(canvasObject, new Vector3(0, 0, 0), 1f).setOnComplete(Transition);
                LeanTween.scaleY(background, 0f, 1f);
                break;
        }
    }

    private void Update()
    {

    }
    private void Start()
    {
        CheckGameStates();
    }

    public void CheckGameStates()
    {
        switch (GameStates.instance.TrueGameStates)
        {
            case GameStates._GameStates.Menu:
                LeanTween.scale(gameScene, new Vector3(0, 0, 0), 0f);
                LeanTween.scale(menuObject, new Vector3(1, 1, 1), 0f);
                LeanTween.scale(endScene, new Vector3(0, 0, 0), 0f);
                break;
            case GameStates._GameStates.Game:
                LeanTween.scale(menuObject, new Vector3(0, 0, 0), 1f).setOnComplete(OpenObject);
                break;
            case GameStates._GameStates.Pause:
                break;
        }
    }
    private void OpenObject()
    {
        LeanTween.scale(gameScene, new Vector3(1, 1, 1), 1f);
    }

    private void Transition()
    {
        SceneManager.LoadScene("MainScene");
    }
}
