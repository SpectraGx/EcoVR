using UnityEngine;

public class BaseStateMachine<Entity_Type>
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public interface I_State
{
    public void Enter();

    public void Execute();

    public void Exit();
}
