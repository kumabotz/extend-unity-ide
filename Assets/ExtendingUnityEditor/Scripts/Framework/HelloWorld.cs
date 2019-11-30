using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomDataStructure
{
    public string name;
    public GameObject target;
    public Vector3 position;
}

public class HelloWorld : MonoBehaviour
{
    [Range(0f, 10f)]
    public float speed;
    public Vector3 startPos;

    [Tooltip("What the GameObject should follow")]
    public HelloWorld target;
    public string message = "Hello World";

    [Header("Array Tests")]
    public string[] strings;
    public int[] numbers;
    public Vector3[] positions;
    public GameObject[] gameObjects;

    public CustomDataStructure[] customTarget;

    [HideInInspector]
    public List<string> stringList;

    [Header("Dictionary and Property Tests")]
    public Dictionary<string, Vector3> childrenPositions;
    public int life { get; set; }
}