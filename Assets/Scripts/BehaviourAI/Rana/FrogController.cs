using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [Header ("Maquina de Estados")]
    public StateMachine<FrogController> stateMachine;

    [Header("Detections")]
    public Transform PlayerTransform;
    public Transform hideoutPoint;

    [Header("Settings & Jump")]
    public float FleeDistance = 3f; // Rango para asustarse
    public float JumpDuration = 1f;
    public float JumpHeight = 2f;
    public AnimationCurve JumpCurve; // Curva para controlar el movimiento del salto

    // Instancia de los estados
    public FrogIdleState S_IdleState;
    public FrogJumpState S_JumpState;

    void Start()
    {
        stateMachine = new StateMachine<FrogController>(this);

        S_IdleState = new FrogIdleState();
        S_JumpState = new FrogJumpState();

        if (JumpCurve.keys.Length == 0)
        {
            // Si no se ha asignado una curva, usar una curva de salto simple
            JumpCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));
        }

        stateMachine.SetCurrentState(S_IdleState); // En el charco
    }

    void Update()
    {
        stateMachine.Updating();
    }

    // CORRUTINA QUE CALCULA UN SALTO PERFECTO EN PARABOLA
    // USANDO LA CURVA DE ANIMACION PARA CONTROLAR EL MOVIMIENTO
    public IEnumerator ParabolicJump()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = hideoutPoint.position;
        float timePassed = 0f;

        transform.LookAt(endPos); // Mirar hacia el punto de salto

        while (timePassed < JumpDuration)
        {
            timePassed += Time.deltaTime;
            // CALCULAR EL TIEMPO NORMALIZADO (0 a 1) PARA USAR EN LA CURVA DE ANIMACION
            float normalizedTime = timePassed / JumpDuration;

            // 1. Moviimiento horizontal lineal
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, normalizedTime);

            // 2. Movimiento vertical controlado por la curva de animacion
            currentPos.y += JumpCurve.Evaluate(normalizedTime) * JumpHeight;

            // 3. Actualizar la posicion de la rana
            transform.position = currentPos;

            yield return null; // Esperar al siguiente frame
        }

        transform.position = endPos; // Asegurar que la rana llegue exactamente al punto final

        //Debug.Log("RANA: ADIOS TONTULES");
        gameObject.SetActive(false); // Desactivar la rana después de saltar al escondite
    }

}
