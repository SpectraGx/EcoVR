using UnityEngine;

public abstract class State<TEntity> : MonoBehaviour
{
    //Se llama cuando entra al estado
    public abstract void Enter(TEntity entity);

    //Las acciones que ejecuta mientras esta en el estado
    public abstract void Execute(TEntity entity);

    //Se llama cuando sale del estado
    public abstract void Exit(TEntity entity);
}
