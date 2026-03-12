public abstract class State<TEntity> 
{
    /// <summary>
    /// Acciones a ejecutar cuando la entidad entra al estado
    /// </summary>
    /// <param name="entity"></param>
    //Se llama cuando entra al estado
    public abstract void Enter(TEntity entity);
    /// <summary>
    /// Acciones a ejecutar mientras está en el estado
    /// </summary>
    /// <param name="entity"></param>
    //Las acciones que ejecuta mientras esta en el estado
    public abstract void Execute(TEntity entity);
    /// <summary>
    /// Acciones a ejecutar cuando sale del estado
    /// </summary>
    /// <param name="entity"></param>
    //Se llama cuando sale del estado
    public abstract void Exit(TEntity entity);
}
