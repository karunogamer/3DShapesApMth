using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float sphereRadius;
    public Vector3 sphereCenter;
    public Material sphereMaterial;
    public float focalLength;

    private void OnPostRender()
    {
        DrawSphere();
    }

    private void OnDrawGizmos()
    {
        DrawSphere();
    }

    public void DrawSphere()
    {
        if (sphereMaterial == null)
        {
            return;
        }

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        sphereMaterial.SetPass(0);

        for (int lat = -90; lat < 90; lat += 10)
        {
            for (int lon = 0; lon < 360; lon += 10)
            {
                float lat1 = Mathf.Deg2Rad * lat;
                float lon1 = Mathf.Deg2Rad * lon;
                float lat2 = Mathf.Deg2Rad * (lat + 10);
                float lon2 = Mathf.Deg2Rad * (lon + 10);

                Vector3 point1 = GetPointOnSphere(lat1, lon1);
                Vector3 point2 = GetPointOnSphere(lat1, lon2);
                Vector3 point3 = GetPointOnSphere(lat2, lon1);
                Vector3 point4 = GetPointOnSphere(lat2, lon2);

                GL.Color(sphereMaterial.color);
                GL.Vertex3(point1.x, point1.y, 0);
                GL.Vertex3(point2.x, point2.y, 0);

                GL.Color(sphereMaterial.color);
                GL.Vertex3(point1.x, point1.y, 0);
                GL.Vertex3(point3.x, point3.y, 0);

                GL.Color(sphereMaterial.color);
                GL.Vertex3(point2.x, point2.y, 0);
                GL.Vertex3(point4.x, point4.y, 0);
            }
        }

        GL.End();
        GL.PopMatrix();
    }

    private Vector3 GetPointOnSphere(float lat, float lon)
    {
        float x = sphereCenter.x + sphereRadius * Mathf.Sin(lat) * Mathf.Cos(lon);
        float y = sphereCenter.y + sphereRadius * Mathf.Sin(lat) * Mathf.Sin(lon);
        return new Vector3(x, y, sphereCenter.z + sphereRadius * Mathf.Cos(lat));
    }
}
