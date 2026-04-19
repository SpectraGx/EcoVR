using UnityEngine;

public enum Species
{
    Coati,
    Perico,
    Iguana,
    Rana,
    Mariposa,
    Hormiga
}

public class AnimalID : MonoBehaviour
{
    [Header("Animal Information")]
    public Species currentSpecies;
}
