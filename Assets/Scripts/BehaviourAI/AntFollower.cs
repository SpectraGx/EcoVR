using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntFollower : MonoBehaviour
{
    [Header("Spiness Path")]
    public Transform[] pathNodes;
    
    [Header("Settings")]
    public float speed = 0.3f; // Las hormigas caminan despacio

    private int currentNodeIndex = 0; 

    void Update()
    {
        // Si no hay camino, no hacemos nada
        if (pathNodes.Length == 0) return;

        // 1. Identificamos nuestro destino actual
        Transform targetNode = pathNodes[currentNodeIndex];

        // 2. Caminamos hacia él
        transform.position = Vector3.MoveTowards(transform.position, targetNode.position, speed * Time.deltaTime);
        
        // 3. Miramos hacia donde caminamos
        transform.LookAt(targetNode);

        // 4. Comprobamos si ya llegamos al nodo (con un margen de error)
        if (Vector3.Distance(transform.position, targetNode.position) < 0.02f)
        {
            currentNodeIndex++; // Pasamos al siguiente nodo

            // 5. Si llegamos al final del camino
            if (currentNodeIndex >= pathNodes.Length)
            {
                // Bucle infinito
                currentNodeIndex = 0;
                transform.position = pathNodes[0].position;
            }
        }
    }
}