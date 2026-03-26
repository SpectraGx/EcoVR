using UnityEngine;

public class ParakeetIdleState : State<ParakeetController>
{
    private float idleTimer;
    private float restDuration;

    public override void Enter(ParakeetController bird)
    {
        Debug.Log("Pio pio: Idle en rama");

        // ANIMACION DE IDLE
        bird.ChangeAnimationState(bird.Anim_Idle);
        
        idleTimer = 0f;
        restDuration = Random.Range(5f, 10f); // Duración aleatoria para el descanso
    }

    public override void Execute(ParakeetController bird)
    {
        // E1: ¿El jugador esta cerca de la rama?
        float distanceToPlayer = Vector3.Distance(bird.transform.position, bird.PlayerTransform.position);
        if (distanceToPlayer < bird.FleeDistance)
        {
            bird.stateMachine.SetCurrentState(bird.S_FlightState);
            return;
        }

        // E2: ¿Ha pasado el tiempo de descanso?
        idleTimer += Time.deltaTime;
        if (idleTimer >= restDuration)
        {
            bird.stateMachine.SetCurrentState(bird.S_FlightState);
        }
    }

    public override void Exit(ParakeetController bird)
    {
        Debug.Log("Pio pio: Despegando de la rama");
    }
}
