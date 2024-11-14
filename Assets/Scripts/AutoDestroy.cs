using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        // Pobiera długość animacji i ustawia usunięcie obiektu po jej zakończeniu
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
        }
        else
        {
            // W razie braku animatora, usuwa obiekt po określonym czasie
            Destroy(gameObject, 1f); // Domyślnie 1 sekunda
        }
    }
}
