using UnityEngine;

public class PingMgr : MonoBehaviour
{
    public GlobalsMgr g;
    
    void Start()
    {
        LoadCircle();
        LoadPoints();
    }

    void Update()
    {
        if (g.ynPause) return;
        for(int nPoint = 0; nPoint < g.points.Count; nPoint++)
        {
            for(int nPointOther = nPoint + 1; nPointOther < g.points.Count; nPointOther++)
            {
                g.toolsMgr.CheckCollisionPoint(nPoint, nPointOther);
            }
            for(int nLine = 0; nLine < g.lines.Count; nLine++)
            {
                g.toolsMgr.CheckCollisionLine(nPoint, nLine);
            }
            g.points[nPoint].Move();
        }
    }

    void LoadPoints()
    {
        g.toolsMgr.AddPoint(Vector3.zero, 100, "point_" + g.points.Count);
    }

    void LoadCircle()
    {
        g.lines.Clear();
        Vector3 posLast = Vector3.zero;
        float radius = 20;
        float delta = 30;
        for (float ang = -delta; ang < 360; ang += delta)
        {
            float x = radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            float y = 0;
            float z = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            Vector3 pos = new(x, y, z);
            if (ang >= 0)
            {
                g.toolsMgr.AddLine(posLast, pos, "line_" + g.lines.Count);
                return;
            }
            posLast = pos;
        }        
    }
}
