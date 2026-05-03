using UnityEngine;

public class VirtualBelt : MonoBehaviour
{
    [Header("References")]
    public Transform headTransform; // Asigna la Main Camera de tu XR Origin en el Inspector

    [Header("Settings of the Belt")]
    public float heightOffset = 0.6f; // Distancia hacia abajo desde la cabeza
    public float forwardOffset = 0.15f; // Distancia hacia adelante
    public float rotationSpeed = 5f; // Velocidad de rotacion del cinturon

    private void Update()
    {
        if (headTransform == null) return;

        // 1. Aislar la direccion hacia adelante de la cam, ignorando si miras arriba/abajo (Eje Y)
        Vector3 forwardFlat = new Vector3(headTransform.forward.x, 0, headTransform.forward.z).normalized;

        // 2. Calcular la posicion: Altura de la cabeza MENOS el offset, MÁS un margen hacia adelante
        Vector3 targetPosition = headTransform.position - new Vector3(0, heightOffset, 0) + (forwardFlat * forwardOffset);

        // 3. Aplicar la posicion instantaneamente (el cinturon no se despega del cuerpo al caminar)
        transform.position = targetPosition;

        // 4. Aplicar la rotacion suavemente (el cinturon tarda un poco en girar)
        if (forwardFlat != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(forwardFlat);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }
}