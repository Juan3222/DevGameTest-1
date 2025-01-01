using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public fruitsSO[] allFruits; // Asigna en el Inspector

    private void Awake()
    {
        // Inicializar el array estático en FruitData
        FruitData.allFruits = allFruits;


    }
}

