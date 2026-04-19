using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhotoManager : MonoBehaviour
{
    // SINGLERTON PARA FACILITAR EL REGISTRO DE FOTOS
    public static PhotoManager instance;

    // Registro de fotos tomadas
    // Relaciona la especie con un booleano que indica si se ha fotografiado o no
    [Header("Data Base")]
    private Dictionary<Species, bool> photoRegister = new Dictionary<Species, bool>();

    [Header("Connection to UI")]
    public TextMeshProUGUI coatiStatus;
    public TextMeshProUGUI pericoStatus;
    public TextMeshProUGUI iguanaStatus;
    public TextMeshProUGUI ranaStatus;
    

    void Awake()
    {
        // Configuracion del singleton
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        // Inicializacion de especies
        photoRegister[Species.Coati] = false;
        photoRegister[Species.Perico] = false;
        photoRegister[Species.Iguana] = false;
        photoRegister[Species.Rana] = false;
        photoRegister[Species.Hormiga] = false;
        photoRegister[Species.Mariposa] = false;
    }

    // Metodo para registrar una foto tomada
    public void RegisterPhoto(Species speciesDiscovered)
    {
        if (photoRegister.ContainsKey(speciesDiscovered))
        {
            if (photoRegister[speciesDiscovered] == false)
            {
                photoRegister[speciesDiscovered] = true;
                MarkBook(speciesDiscovered);
                Debug.Log("NUEVO DESCUBRIMIENTO: ¡Has fotografiado a " + speciesDiscovered.ToString() + " por primera vez!");
            }
            else
            {
                Debug.Log("Ya has fotografiado a " + speciesDiscovered.ToString());
            }
        }
    }

    public void MarkBook(Species speciesToMark)
    {
        switch (speciesToMark)
        {
            case Species.Coati:
                coatiStatus.text = "Fotografiado";
                break;
            case Species.Perico:
                pericoStatus.text = "Fotografiado";
                break;
            case Species.Iguana:
                iguanaStatus.text = "Fotografiado";
                break;
            case Species.Rana:
                ranaStatus.text = "Fotografiado";
                break;
            case Species.Hormiga:
                // Hormiga no tiene texto asignado, pero se puede agregar si es necesario
                break;
            case Species.Mariposa:
                // Mariposa no tiene texto asignado, pero se puede agregar si es necesario
                break;
        }
    }
}
