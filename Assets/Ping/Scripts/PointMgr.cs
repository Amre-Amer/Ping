using UnityEngine;

public class PointMgr : MonoBehaviour
{
    public GlobalsMgr g;
    float distMove = .05f;
    float[] yInterceptLasts;
    public GameObject body;

    public void Init(Vector3 pos, float yaw, float scale, int countLines)
    {
        transform.position = pos;
        body.transform.position = pos;
        SetScale(scale);
        SetYaw(yaw);
        yInterceptLasts = new float[countLines];    
    }

    public void Move()
    {
        body.transform.Translate(0, 0, distMove);        
    }

    void SetScale(float scale)
    {
        body.transform.localScale = Vector3.one * scale;
    }

    void SetYaw(float yaw)
    {
        Vector3 eul = transform.eulerAngles;
        eul.y = yaw;
        transform.eulerAngles = eul;
    }

    public float GetYInterceptLast(int nLine)
    {
        return yInterceptLasts[nLine];
    }

    public void SetYInterceptLast(int nLine, float value)
    {
        yInterceptLasts[nLine] = value;
    }
}
