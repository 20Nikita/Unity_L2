using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Globalization;

namespace Valve.VR.InteractionSystem
{
public class Batton : MonoBehaviour
{

    public GameObject mesh_point;
    public TextAsset TextFile;
    public float MaxPoint = 10000;

    public LinearMapping ProbabilityLinear;
    public LinearMapping SizeСloudLinear;
    public LinearMapping SizePointLinear;

    bool state = false;
    // Точки
    List<GameObject> Meshs=new List<GameObject>();
    List<LoadPoints.Point> Points=new List<LoadPoints.Point>();
    float max_x;
    float min_x;
    float max_y;
    float min_y;
    float min_z;
    float max_z;

    // Задаваемые параметры
    float Probability;
    float SizeСloud;
    float SizePoint;

    // Коэффициенты скалирования параметров
    float SizePointAlf = 0.02f;
    float probability = 0.1f;

    private void Start()
    {
        // Кнопка нажата состояние выкл
        state = false;
        GetComponent<Renderer>().material.color = Color.red;
        // Загрузка точек из файла
        LoadPoints.Load(TextFile, ref Points, ref max_x, ref min_x, ref max_y, ref min_y, ref max_z, ref min_z);
    }
    public void up(){
        if (state) 
        {
            // Кнопка нажата состояние выкл
            state = false;
            GetComponent<Renderer>().material.color = Color.red;
            // Удалить точки
            foreach (GameObject item in Meshs)
            {
                Destroy(item);
            }
        }
        else
        {
            // Получение параметров точек заданных в сцене
            LinearMapping t = ProbabilityLinear.GetComponent<LinearMapping>();
            probability = t.value;
            t = SizeСloudLinear.GetComponent<LinearMapping>();
            SizeСloud = (t.value + 0.01f)*2;
            t = SizePointLinear.GetComponent<LinearMapping>();
            SizePoint = t.value;
            // Кнопка нажата состояние вкл
            state = true;
            GetComponent<Renderer>().material.color = Color.green;
            // Расчет параметров смещения и трансформации по данным со сцены
            float center_x = (max_x + min_x)/2;
            float center_y = (max_y + min_y)/2;
            float center_z = (max_z + min_z)/2;
            float alf = SizeСloud / Math.Max(max_x - min_x, max_z - min_z);
            Probability = Math.Min(MaxPoint / Points.Count,1) * probability;
            mesh_point.transform.localScale = new Vector3(SizePointAlf*SizePoint,SizePointAlf*SizePoint,SizePointAlf*SizePoint);
            // Цикл по точкам
            foreach (LoadPoints.Point item in Points)
            {
                // Рисовать не все точки
                if (UnityEngine.Random.Range(0.0f, 1.0f) < Probability){
                    // Координаты
                    float x = (item.Vertices[0] - center_x)*alf + transform.position[0];
                    float y = (item.Vertices[1] - min_y)*alf + transform.position[1] + 0.5f;
                    float z = (item.Vertices[2] - center_z)*alf + transform.position[2];
                    // Спавн
                    GameObject mesh = Instantiate(mesh_point, new Vector3(x, y, z), Quaternion.identity);
                    // Цвет
                    mesh.GetComponent<Renderer>().material.color = item.Colors;
                    // Сохранить (для удаления)
                    Meshs.Add(mesh);
                }
            }
        }
    }
}
}