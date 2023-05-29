using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
public class LoadPoints : MonoBehaviour
{
    public struct Point
    {
        public Vector3 Vertices;
        public Color Colors;
    
        public Point(Vector3 vertices, Color colors)
        {
            this.Vertices = vertices;
            this.Colors = colors;
        }
    }
    public static void Load(TextAsset TextFile, ref List<Point> Points, ref float max_x, ref float min_x, ref float max_y, ref float min_y, ref float max_z, ref float min_z)
    {
        string text = TextFile.ToString();
        string[] points = text.Split('\n');
        string[] t = points[0].Split(' ');
        max_x = float.Parse(t[0], CultureInfo.InvariantCulture.NumberFormat);
        min_x = float.Parse(t[0], CultureInfo.InvariantCulture.NumberFormat);
        max_y = float.Parse(t[1], CultureInfo.InvariantCulture.NumberFormat);
        min_y = float.Parse(t[1], CultureInfo.InvariantCulture.NumberFormat);
        max_z = float.Parse(t[2], CultureInfo.InvariantCulture.NumberFormat);
        min_z = float.Parse(t[2], CultureInfo.InvariantCulture.NumberFormat);
        foreach (var item in points)
        {
            string[] point = item.Split(' ');
            float x = float.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat);
            float z = float.Parse(point[2], CultureInfo.InvariantCulture.NumberFormat);
            if (x < min_x){ min_x = x; }
            else if (x > max_x){ max_x = x; }
            if (y < min_y){ min_y = y; }
            else if (y > max_y){ max_y = y; }
            if (z < min_z){ min_z = z; }
            else if (z > max_z){ max_z = z; }
            Vector3 loc = new Vector3(x, y, z);
            Color col = new Color(float.Parse(point[3], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[4], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[5], CultureInfo.InvariantCulture.NumberFormat));
            Points.Add(new Point(loc, col));
        }
    }
}