using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : MonoBehaviour
{
    public float pyramidSideLength;
    public Vector3 pyramidCenter;
    public Material pyramidMaterial;
    public float focalLength;

    public Vector3[] GetBaseSquare()
    {
        var halfLength = pyramidSideLength * 0.5f;

        return new[]
        {
            new Vector3(pyramidCenter.x + halfLength, pyramidCenter.y + halfLength, 0),
            new Vector3(pyramidCenter.x - halfLength, pyramidCenter.y + halfLength, 0),
            new Vector3(pyramidCenter.x - halfLength, pyramidCenter.y - halfLength, 0),
            new Vector3(pyramidCenter.x + halfLength, pyramidCenter.y - halfLength, 0),
        };
    }

    private void OnPostRender()
    {
        DrawLines();
    }

    private void OnDrawGizmos()
    {
        DrawLines();
    }

    public void DrawLines()
    {
        if (pyramidMaterial == null)
        {
            return;
        }

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        pyramidMaterial.SetPass(0);

        var baseSquareVectors = GetBaseSquare();
        var baseScale = focalLength / ((pyramidCenter.z - pyramidSideLength * 0.5f) + focalLength);
        var apex = new Vector3(pyramidCenter.x, pyramidCenter.y, -pyramidSideLength * 0.5f);

        // Draw lines for the base square
        for (int i = 0; i < baseSquareVectors.Length; i++)
        {
            GL.Color(pyramidMaterial.color);
            var point1 = baseSquareVectors[i] * baseScale;
            GL.Vertex3(point1.x, point1.y, 0);
            var point2 = baseSquareVectors[(i + 1) % baseSquareVectors.Length] * baseScale;
            GL.Vertex3(point2.x, point2.y, 0);

            // Draw lines from apex to base vertices
            GL.Color(pyramidMaterial.color);
            GL.Vertex3(apex.x, apex.y, apex.z);
            GL.Vertex3(point1.x, point1.y, 0);
        }

        GL.End();
        GL.PopMatrix();
    }
}
