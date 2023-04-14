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
    struct Point
    {
        public Vector3 Vertices;
        public Color Colors;
        public bool Visible;
    
        public Point(Vector3 vertices, Color colors, bool visibles)
        {
            this.Vertices = vertices;
            this.Colors = colors;
            this.Visible = visibles;
        }
    }
    private void Start()
    {
        state = false;
        GetComponent<Renderer>().material.color = Color.red;

        List<Point>Points=new List<Point>();
        Debug.Log(Directory.GetCurrentDirectory());
        string text = File.ReadAllText(textFile);
        // Debug.Log(text);
        string[] points = text.Split('\n');
        foreach (var item in points)
        {
            string[] point = item.Split(' ');
            Vector3 loc = new Vector3(float.Parse(point[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[1], CultureInfo.InvariantCulture.NumberFormat), float.Parse(point[2], CultureInfo.InvariantCulture.NumberFormat));
            Color col = new Color(float.Parse(point[3]) / 255, float.Parse(point[4]) / 255, float.Parse(point[5]) / 255);
            Points.Add(new Point(loc, col, true));
        }

    }
    public void up(){
        if (state) 
        {
            state = false;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            state = true;
            GetComponent<Renderer>().material.color = Color.green;
            string text = File.ReadAllText(textFile);
            Debug.Log(text);
        }
    }
}
