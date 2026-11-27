using System.Collections.Generic;
using UnityEngine;

public class GlobalsMgr : MonoBehaviour
{
    public ToolsMgr toolsMgr;
    public PingMgr ping;
    public float widthLine;
    public PointMgr pointPrefab;
    public float scalePoint;
    public LineMgr linePrefab;
    public GameObject markerPrefab;
    public List<LineMgr>lines;
    public List<PointMgr>points;
    public GameObject parentLines;
    public GameObject parentPoints;
    public bool ynPause;

    void Awake()
    {
        widthLine = .5f;
        scalePoint = 1; //3;
    }
}
