using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nuevaFruta")]
public class fruitsSO : ScriptableObject
{
     public string fruit;
     public Vector2 size;
     public Sprite fruitPNG;
     public int level;
     public GameObject fruitPrefab;

}
