using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Prędkość poruszania się statku
    public AudioClip engineSound; // Dźwięk silnika
    public AudioMixerGroup engineMixerGroup; // Grupa Audio Mixer dla silnika

    private AudioSource audioSource;

    private float xMin, xMax, yMin, yMax; // Granice ruchu statku

    void Start()
    {
        // Ustal granice ruchu statku na podstawie rozmiaru kamery
        float camDistance = Camera.main.transform.position.z - transform.position.z;
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        xMin = bottomLeft.x;
        xMax = topRight.x;
        yMin = bottomLeft.y;
        yMax = topRight.y;

        // Dodaj komponent AudioSource i przypisz dźwięk silnika
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = engineSound;
        audioSource.loop = true; // Zapętlony dźwięk
        audioSource.playOnAwake = false; // Dźwięk nie gra automatycznie
        audioSource.outputAudioMixerGroup = engineMixerGroup; // Przypisz grupę Audio Mixer do AudioSource
        audioSource.volume = 0.3f; // Ustaw głośność dźwięku silnika
    }

    void Update()
    {
        // Pobierz wejście na osi poziomej (A/D lub strzałki w lewo/prawo)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Pobierz wejście na osi pionowej (W/S lub strzałki góra/dół)
        float verticalInput = Input.GetAxis("Vertical");

        // Oblicz wektor ruchu
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        // Przenieś statek z ograniczeniami granic
        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
        transform.position = newPosition;

        // Obsługa dźwięku silnika
        if (movement != Vector3.zero)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Odtwórz dźwięk, gdy statek się porusza
            }
        }
        else
        {
            audioSource.Stop(); // Zatrzymaj dźwięk, gdy statek się zatrzyma
        }
    }
}
