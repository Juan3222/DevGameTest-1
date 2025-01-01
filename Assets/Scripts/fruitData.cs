using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitData : MonoBehaviour
{
    public fruitsSO fruitsData; // Referencia al Scriptable Object
    public static fruitsSO[] allFruits; // Lista de todas las frutas


    void Start()
    {

        if (fruitsData != null)
        {
            // Configurar la fruta usando los datos del Scriptable Object
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.sprite = fruitsData.fruitPNG;

            transform.localScale = new Vector3(fruitsData.size.x, fruitsData.size.y, 1);
            gameObject.name = fruitsData.fruit;
        }

    }


    // Método para obtener la siguiente fruta en nivel
    private fruitsSO GetNextFruit(fruitsSO currentFruit)
    {
        if (fruitsData == null)
        {
            Debug.Log("fruitsData es null en " + gameObject.name);

        }
        Debug.Log("Buscando siguiente nivel para: " + currentFruit.level);
        foreach (fruitsSO fruit in allFruits)
        {

            Debug.Log("Revisando fruta de nivel: " + fruit.level);
            if (fruit.level == currentFruit.level + 1)
            {
                Debug.Log("Siguiente fruta encontrada: " + fruit.level + fruit.fruit);
                return fruit;
            }
        }
        Debug.Log("No se encontró fruta de nivel superior");
        return null; // Devuelve null si no hay un nivel superior
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Obtener el componente Fruit del objeto con el que chocaste
        FruitData otherFruit = collision.gameObject.GetComponent<FruitData>();

        if (otherFruit != null)
        {
            // Verificar si las frutas tienen el mismo nivel
            if (this.fruitsData.level == otherFruit.fruitsData.level)
            {
                Debug.Log("Frutas del mismo nivel detectadas: " + this.fruitsData.level);

                // Obtener el nivel siguiente
                fruitsSO nextFruitData = GetNextFruit(fruitsData);

                if (nextFruitData != null)
                {
                    // Crear la nueva fruta en la posición promedio
                    Vector2 spawnPosition = (transform.position + otherFruit.transform.position) / 2;

                    // Obtener el prefab de la fruta siguiente (con base en el ScriptableObject)
                    GameObject nextFruitPrefab = nextFruitData.fruitPrefab; // Suponiendo que tienes un campo en tu FruitsSO para el prefab

                    // Instanciar la nueva fruta utilizando el prefab de la fruta siguiente
                    GameObject newFruit = Instantiate(nextFruitPrefab, spawnPosition, Quaternion.identity);

                    // Asignar correctamente los datos de la siguiente fruta
                    FruitData newFruitData = newFruit.GetComponent<FruitData>();
                    if (newFruitData != null)
                    {
                        newFruitData.fruitsData = nextFruitData;  // Asignar los datos correctos de la siguiente fruta
                    }
                    else
                    {
                        Debug.LogError("El prefab no tiene el componente FruitData.");
                    }

                    // Copiar el CircleCollider2D del prefab de la fruta siguiente
                    CircleCollider2D newCollider = newFruit.GetComponent<CircleCollider2D>();
                    if (newCollider != null)
                    {
                        // Obtener el CircleCollider2D del prefab de la fruta siguiente
                        CircleCollider2D prefabCollider = nextFruitPrefab.GetComponent<CircleCollider2D>();

                        if (prefabCollider != null)
                        {
                            // Copiar el radius y el offset directamente del prefab
                            newCollider.radius = prefabCollider.radius;
                            newCollider.offset = prefabCollider.offset;
                        }
                        else
                        {
                            Debug.LogError("El prefab de la fruta siguiente no tiene un CircleCollider2D configurado.");
                        }

                        newCollider.enabled = true;  // Asegúrate de que el collider esté habilitado
                    }
                    else
                    {
                        Debug.LogError("El prefab de la fruta siguiente no tiene un CircleCollider2D.");
                    }

                    Debug.Log("Nueva fruta creada con los componentes activados.");
                    // Destruir ambas frutas
                    Destroy(gameObject); // Destruye la fruta actual
                    Destroy(otherFruit.gameObject); // Destruye la fruta con la que colisionaste
                }




            }
        }

    }

}
