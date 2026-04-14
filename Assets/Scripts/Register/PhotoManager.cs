using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public static PhotoManager instance;

    // Registro de fotos tomadas
    // Relaciona la especie con un booleano que indica si se ha fotografiado o no
    private Dictionary<Species, bool> photoRegister = new Dictionary<Species, bool>();

    void Awake()
    {
        // Configuracion del singleton
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }

        photoRegister[Species.Coati] = false;
        photoRegister[Species.Perico] = false;
        photoRegister[Species.Iguana] = false;
        photoRegister[Species.Rana] = false;
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
                Debug.Log("NUEVO DESCUBRIMIENTO: ¡Has fotografiado a " + speciesDiscovered.ToString() + " por primera vez!");
            }
            else
            {
                Debug.Log("Ya has fotografiado a " + speciesDiscovered.ToString());
            }
        }
    }
}
