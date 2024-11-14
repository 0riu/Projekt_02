using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocketPrefab; // Prefab rakiety do instancjonowania
    public float launchInterval = 0.2f; // Czas pomiędzy wystrzałami przy przytrzymaniu
    public Vector3[] rocketOffsets = { new Vector3(0.23f, 0.323f, 1), new Vector3(0.23f, -0.336f, 1) }; // Przesunięcia pozycji rakiet
    public float rocketRotation = 270f; // Kąt obrotu rakiety w stopniach

    private float lastLaunchTime = 0f; // Czas ostatniego wystrzału

    private void Update()
    {
        // Sprawdź, czy spacja jest przytrzymana i czy minął czas od ostatniego wystrzału
        if (Input.GetKey(KeyCode.Space) && Time.time > lastLaunchTime + launchInterval)
        {
            LaunchRocket();
            lastLaunchTime = Time.time; // Zaktualizuj czas ostatniego wystrzału
        }
    }

    private void LaunchRocket()
    {
        // Tworzenie rakiet w różnych pozycjach w tym samym czasie
        Quaternion rotation = Quaternion.Euler(0, 0, rocketRotation); // Ustawienie kąta obrotu rakiety

        foreach (Vector3 offset in rocketOffsets)
        {
            Instantiate(rocketPrefab, transform.position + offset, rotation);
        }
    }
}
