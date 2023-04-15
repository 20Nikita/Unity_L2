using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Globalization;
public class Batton : MonoBehaviour
{

    bool state = false;
    public string textFile = "";
    public GameObject mesh_point;
    List<GameObject>Meshs=new List<GameObject>();
    List<Point>Points=new List<Point>();
    public float probability = 0.01f;
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

        Debug.Log(Directory.GetCurrentDirectory());
        string text = File.ReadAllText(textFile);
        // Debug.Log(text);
        string[] points = text.Split('\n');
        foreach (var item in points)
        {
            string[] point = item.Split(' ');
            Vector3 loc = new Vector3(float.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[2], CultureInfo.InvariantCulture.NumberFormat));
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
            string text = File.ReadAllText(textFile);
            Debug.Log(text);
            foreach (Point item in Points)
            {
                if (UnityEngine.Random.Range(0.0f, 1.0f) < probability){
                    GameObject mesh = Instantiate(mesh_point, item.Vertices, Quaternion.identity);
                    var t = mesh.GetComponent<SetColor>();
                    t.set_color(item.Colors);
                    Meshs.Add(mesh);
                }
            }
        }
    }
}
