using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    #region Singleton
    public static Global instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform startBoundryX;
    public Transform endBoundryX;
    public Transform startBoundryZ;
    public Transform endBoundryZ;

    public bool royaltyWaveBeginning;

}
