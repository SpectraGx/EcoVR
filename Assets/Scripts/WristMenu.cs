using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenu : MonoBehaviour
{
    [Header("References")]
    public Transform head;

    [Header("Visibility Settings")]
    public float angeTolarence = 45f;
    private Canvas menuCanvas;

    void Start()
    {
        menuCanvas = GetComponent<Canvas>();
    }

    void Update()
    {
        // 1. Calculamos el angulo entre la direccion del menu y la cabeza del jugador
        Vector3 directionalToHead = head.position - transform.position;

        // 2. Si el angulo es menor a la tolerancia, mostramos el menu, si no, lo ocultamos
        float angle = Vector3.Angle(directionalToHead, transform.up);

        if (angle < angeTolarence)
        {
            menuCanvas.enabled = true;
        }
        else
        {
            menuCanvas.enabled = false;
        }
    }
}
