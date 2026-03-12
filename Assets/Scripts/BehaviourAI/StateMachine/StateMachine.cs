using UnityEngine;

public class StateMachine<TEntity>
{
    // Indicamos a quien le pertenece la instancia de esta máquina
    TEntity _Owner;
    //El estado actual de la maquina
    State<TEntity> _CurrentState;
    //El estado anterior en el que se encontraba
    State<TEntity> _PreviousState;

    //Establecemos a quien le pertenece la maquina e inicializamos sus variables
    public StateMachine(TEntity owner)
    {
        _Owner = owner;
        _CurrentState = null;
        _PreviousState = null;
    }

    // Usamos este metodo para actualizar o mas bien realizar las acciones del estado actual
    public void Updating()
    {
        // Si hay un estado, ejecutalo
        _CurrentState?.Execute(_Owner);
    }


    
    //Metodo encargado de cambiar el estado de la maquina
    private void ChangeState(State<TEntity> newState)
    {
        //Checamos si el nuevo estado al que se intenta cambiar es valido o existe O si esta queriendo reiniciarse el esatado actual.
        if (newState == null)
        {
            Debug.Log("<StateMachine::ChangeState>: trying to change to a null state");
        }
        else if(newState == _CurrentState)
        {
            Debug.LogError("Trying to run the same State");
        }

        //Luego asignamos el estado actual como el estado previo si hay un estado actual
        if(_CurrentState)
        {
            _PreviousState = _CurrentState;
            //Corremos la logica que finaliza el estado
            _CurrentState.Exit(_Owner);
        }
        // Cambiamos el estado actual al nuevo estado
        _CurrentState = newState;
        // Iniciamos la logica de inicio del estado
        _CurrentState.Enter(_Owner);
    }

    //Metodo publico que usamos cuando queremos cambiar el estadoz
    public void SetCurrentState(State<TEntity> targetState)
    {
        ChangeState(targetState);
    }

    //Usamos este metodo cuando se necesite regresar al estado anterior, sin inportar cual fuera.
    public void RevertToPreviousState()
    {
        ChangeState(_PreviousState);
    }


    //Los siguientes metodos estan en caso que ocupemos acceder a las variables de los estados guardados

    public State<TEntity> CurrentState()
    {
        return _CurrentState;
    }
    public State<TEntity> PreviousState()
    {
        return _PreviousState;
    }
}
