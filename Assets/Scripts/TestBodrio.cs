using Unity.Mathematics;
using UnityEngine;

public class TestBodrio : MonoBehaviour
{
    [SerializeField] GameObject cheese;
    //Literal este script spawnea un queso, que mas digo. SOlo es para testear que jale lo que sea, era un queso o una calaca
    public void Cheese()
    {
        Instantiate(cheese, transform.position, quaternion.identity);
    }
}
