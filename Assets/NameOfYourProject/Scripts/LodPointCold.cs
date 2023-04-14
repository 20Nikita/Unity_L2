using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class Lod : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    string GetConfig(string textFile) 
    {
        string text = File.ReadAllText(textFile);
        Console.WriteLine(text);
        return text;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
