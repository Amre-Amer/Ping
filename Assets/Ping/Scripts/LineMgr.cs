using UnityEngine;

public class LineMgr : MonoBehaviour
{
    public GlobalsMgr g;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject intersection;
    public GameObject normal;
    public GameObject forward;
    public GameObject body;

    public void Init(Vector3 posA, Vector3 posB)
    {
        Vector3 mid = (posA + posB) / 2;
        transform.position = mid;
        pointA = Instantiate(g.markerPrefab, transform);
        pointA.name = "pointA";
        pointA.transform.position = posA;
        pointB = Instantiate(g.markerPrefab, transform);
        pointB.name = "pointB";
        pointB.transform.position = posB;
        intersection = Instantiate(g.markerPrefab, transform);
        intersection.name = "intersection";
        intersection.transform.position = mid;
        normal = Instantiate(g.markerPrefab, transform);
        normal.name = "normal";
        normal.transform.position = mid;
        forward = Instantiate(g.markerPrefab, transform);
        forward.name = "forward";
        forward.transform.position = mid;
        Adjust();
    }

    void Adjust()
    {
        Vector3 posA = pointA.transform.position;
        Vector3 posB = pointB.transform.position;
        Vector3 mid = (posA + posB) / 2;
        float dist = Vector3.Distance(posA, posB);
        body.transform.position = mid;
        body.transform.LookAt(posB);
        Vector3 sca = new(g.widthLine, g.widthLine, dist);
        body.transform.localScale = sca;
    }
}
