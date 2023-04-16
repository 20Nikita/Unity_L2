using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Globalization;
public class Batton : MonoBehaviour
{

    bool state = false;
    public GameObject mesh_point;
    public TextAsset TextFile;
    List<GameObject>Meshs=new List<GameObject>();
    List<Point>Points=new List<Point>();
    public float MaxPoint = 10000;

    public float probability = 0.1f;
    public float Size = 1;
    public float SizePoint = 1;
    float SizePointAlf = 0.01f;
    float Probability;
    float max_x;
    float min_x;
    float max_y;
    float min_y;
    float min_z;
    float max_z;
    struct Point
    {
        public Vector3 Vertices;
        public Color Colors;
    
        public Point(Vector3 vertices, Color colors)
        {
            this.Vertices = vertices;
            this.Colors = colors;
        }
    }
    private void Start()
    {
        state = false;
        GetComponent<Renderer>().material.color = Color.red;

        string text = TextFile.ToString();
        // Debug.Log(text);
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
    public void up(){
        if (state) 
        {
            state = false;
            GetComponent<Renderer>().material.color = Color.red;
            foreach (GameObject item in Meshs)
            {
                Destroy(item);
            }
        }
        else
        {
            state = true;
            GetComponent<Renderer>().material.color = Color.green;
            float center_x = (max_x + min_x)/2;
            float center_y = (max_y + min_y)/2;
            float center_z = (max_z + min_z)/2;
            float alf = Size / Math.Max(max_x - min_x, max_z - min_z);
            Probability = Math.Min(MaxPoint / Points.Count,1) * probability;
            mesh_point.transform.localScale = new Vector3(SizePointAlf*SizePoint,SizePointAlf*SizePoint,SizePointAlf*SizePoint);
            foreach (Point item in Points)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < Probability){
                    float x = (item.Vertices[0] - center_x)*alf + transform.position[0];
                    float y = (item.Vertices[1] - min_y)*alf + transform.position[1] + 0.5f;
                    float z = (item.Vertices[2] - center_z)*alf + transform.position[2];

                    GameObject mesh = Instantiate(mesh_point, new Vector3(x, y, z), Quaternion.identity);
                    mesh.GetComponent<Renderer>().material.color = item.Colors;
                    Meshs.Add(mesh);
                }
            }
        }
    }
}
