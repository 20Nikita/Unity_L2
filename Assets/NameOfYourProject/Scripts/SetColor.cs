using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    // Start is called before the first frame update
    public void set_color(Color col)
    {
        GameObject t = transform.GetChild(0).gameObject;
        GetComponent<Renderer>().material.color = col;
        t.GetComponent<Renderer>().material.color = col;
    }
}
