using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates instance;
    public enum _GameStates { Menu, Game, Pause}

    private _GameStates gameStates;

    public _GameStates TrueGameStates { get { return gameStates; } set { gameStates = value; } }

    private void Awake()
    {
        gameStates = _GameStates.Menu;
        instance = this;
        //StartCoroutine(testC());
    }

    private IEnumerator testC()
    {
        yield return new WaitForSeconds(5f);
        gameStates = _GameStates.Game;
        SceneSystem.instance.CheckGameStates();
    }
}
