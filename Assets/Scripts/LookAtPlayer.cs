using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
   private Transform playerHead;

    void Start()
    {
        if (Camera.main != null)
        {
            playerHead = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró la cámara principal. Asegúrate de que tu cámara tenga la etiqueta 'MainCamera'.");
        }
    }

    void Update()
    {
        if (playerHead != null)
        {
            transform.LookAt(playerHead);
        }
    }
}
