using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float speed = 2.0f; // Prędkość ruchu rakiety
    public GameObject explosionPrefab; // Prefab eksplozji

    private void Update()
    {
        // Poruszaj rakietą w kierunku do góry (zgodnie z jej rotacją, czyli w rzeczywistości w prawo)
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);

        // Sprawdzenie, czy rakieta opuściła ekran
        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * Camera.main.aspect || 
            Mathf.Abs(transform.position.y) > Camera.main.orthographicSize)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Tworzenie efektu eksplozji
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        
        // Usuwanie rakiety po eksplozji
        Destroy(gameObject);
    }
}
