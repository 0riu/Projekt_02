using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject background1; // Pierwszy obraz tła
    public GameObject background2; // Drugi obraz tła
    public float scrollSpeed; // Prędkość przesuwania tła

    private float backgroundWidth; // Szerokość tła

    void Start()
    {
        // Ustal szerokość tła na podstawie pierwszego obrazu (zakładając, że oba są tej samej szerokości)
        backgroundWidth = background1.GetComponent<SpriteRenderer>().bounds.size.x;   

        // Dopasuj skalę tła do kamery
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = Vector3.one;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 newScale = transform.localScale;
        newScale.x = worldScreenWidth / width;
        newScale.y = worldScreenHeight / height;

        transform.localScale = newScale;
    }

    void Update()
    {
        // Przesuwaj oba obrazy tła w lewo
        background1.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        background2.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Resetuj pozycję pierwszego tła, gdy przesunie się całkowicie poza ekran
        if (background1.transform.position.x <= -backgroundWidth)
        {
            background1.transform.position = new Vector3(background2.transform.position.x + backgroundWidth, background1.transform.position.y, background1.transform.position.z);
        }

        // Resetuj pozycję drugiego tła, gdy przesunie się całkowicie poza ekran
        if (background2.transform.position.x <= -backgroundWidth)
        {
            background2.transform.position = new Vector3(background1.transform.position.x + backgroundWidth, background2.transform.position.y, background2.transform.position.z);
        }
    }
}
