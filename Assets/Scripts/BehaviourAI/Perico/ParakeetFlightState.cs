using UnityEngine;

public class ParakeetFlightState : State<ParakeetController>
{
    public override void Enter(ParakeetController bird)
    {
        // Elegimos un nuevo Waypoint aleatorio
        Transform newTarget = bird.CurrentPerch;

        // ANIMACION DE VUELO
        bird.ChangeAnimationState(bird.Anim_Fly);
        
        // Bucle para asegurarnos de no elegir la misma rama
        if (bird.Perches.Length > 1)
        {
            while (newTarget == bird.CurrentPerch)
            {
                int randomIndex = Random.Range(0, bird.Perches.Length);
                newTarget = bird.Perches[randomIndex];
            }
        }
        
        bird.CurrentPerch = newTarget;

        // El pajaro apunte hacia la nueva rama
        bird.transform.LookAt(bird.CurrentPerch.position);
        //Debug.Log("Pio pio: Despegando hacia nueva rama");
    }

    public override void Execute(ParakeetController bird)
    {
        // Movemos al perico hacia el objetivo a la velocidad de vuelo
        bird.transform.position = Vector3.MoveTowards(
            bird.transform.position, 
            bird.CurrentPerch.position, 
            bird.FlightSpeed * Time.deltaTime
        );

        // E1: ¿Ya llegó a la rama objetivo?
        float distanceToTarget = Vector3.Distance(bird.transform.position, bird.CurrentPerch.position);
        
        if (distanceToTarget <= 0.1f)
        {
            //Debug.Log("Pio pio: Llegué a la rama");
            bird.stateMachine.SetCurrentState(bird.S_IdleState);
        }
    }

    public override void Exit(ParakeetController bird)
    {
        bird.transform.position = bird.CurrentPerch.position;
        //Debug.Log("Pio pio: Aterrizaje exitoso");
    }
}