using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMConfiner : MonoBehaviour
{
    PolygonCollider2D polygon;
    public Color color;
    Vector2[] points = new Vector2[4];
    private void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
        points = polygon.GetPath(0);
    }
    private void Update()
    {
        Debug.DrawLine(points[0], points[1],color);
         Debug.DrawLine(points[1], points[2],color);
        Debug.DrawLine(points[2], points[3],color);
        Debug.DrawLine(points[3], points[0],color);
    }
}
