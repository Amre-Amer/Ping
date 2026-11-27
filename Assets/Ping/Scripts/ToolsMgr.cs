using UnityEngine;

public class ToolsMgr : MonoBehaviour
{
    public GlobalsMgr g;

    public void AddLine(Vector3 posFrom, Vector3 posTo, string nam)
    {
        LineMgr line = Instantiate(g.linePrefab, g.parentLines.transform);
        line.name = nam;
        line.g = g;
        line.Init(posFrom, posTo);
        g.lines.Add(line);
    }

     public void AddPoint(Vector3 pos, float yaw, string nam)
    {
        PointMgr point = Instantiate(g.pointPrefab, g.parentPoints.transform);
        point.name = nam;
        point.g = g;
        point.Init(pos, yaw, g.scalePoint, g.lines.Count);
        g.points.Add(point);
    }

    public void CheckCollisionPoint(int nPoint, int nPointOther)
    {
        
    }
    
    public void CheckCollisionLine(int nPoint, int nLine)
    {
        PointMgr point = g.points[nPoint];
        LineMgr line = g.lines[nLine];
        Vector3 posLeft = line.pointA.transform.position;
        Vector3 posLeftL = point.body.transform.InverseTransformPoint(posLeft);
        Vector3 posRight = line.pointB.transform.position;
        Vector3 posRightL = point.body.transform.InverseTransformPoint(posRight);
        GameObject intersection = line.intersection;
        GameObject normal = line.normal;
        GameObject forward = line.forward;
        if (posLeftL.x > posRightL.x)
        {
            Vector3 tmp = posLeft;
            Vector3 tmpL = posLeftL;
            posLeftL = posRightL;
            // posLeft = posRight;
            posRightL = tmpL;
            posRight = tmp;
        }
        intersection.SetActive(true);        
        if (posLeftL.x > g.scalePoint / 2 || posRightL.x < -g.scalePoint / 2)
        {
            if (nLine == 2) Debug.Log("nLine: " + nLine + " outside \n");
            intersection.SetActive(false);
            return;
        }
        float dz = posRightL.z - posLeftL.z;
        float dx = posRightL.x - posLeftL.x;
        float slope = dz / dx;
        float yIntercept = posLeftL.z - slope * posLeftL.x;
        if (yIntercept < 0 && point.GetYInterceptLast(nLine) > 0)
        {
            // intersection
            intersection.transform.position = point.transform.position;
            intersection.transform.eulerAngles = point.transform.eulerAngles;
            intersection.transform.Translate(0, 0, yIntercept);
            
            // normal
            normal.transform.position = intersection.transform.position;
            normal.transform.LookAt(posRight);
            normal.transform.Rotate(0, -90, 0);
            normal.transform.Translate(0, 0, 5);
            intersection.transform.eulerAngles = normal.transform.eulerAngles;

            // forward
            forward.transform.position = intersection.transform.position;
            forward.transform.eulerAngles = point.transform.eulerAngles;
            forward.transform.Translate(0, 0, 5);

            float angNormal = normal.transform.eulerAngles.y;
            float yaw = point.transform.eulerAngles.y;
            float diff = NormAng(yaw - angNormal);
            yaw = NormAng(yaw);
            // 
            if (diff > 0)
            {
                diff = 90 - diff;
                yaw = yaw + diff * 2;
            } else
            {
                diff = 90 + diff;
                yaw = yaw - diff * 2; 
            }
            yaw = NormAng(yaw);
            Vector3 eul = point.transform.eulerAngles;
            eul.y = yaw;
            point.transform.eulerAngles = eul;
            // g.ynPause = true;
        } else
        {
            intersection.SetActive(false);
        } 
        point.SetYInterceptLast(nLine, yIntercept);       
    }

    float NormAng(float ang)
    {
        if (ang > 180) ang -= 360;
        if (ang < -180) ang += 360;
        return ang;
    }
}
