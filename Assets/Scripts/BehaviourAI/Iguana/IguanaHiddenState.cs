using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IguanaHiddenState : State<IguanaController>
{
    public override void Enter(IguanaController iguana)
    {
        Debug.Log("IGUANA: ME VOY A ESCONDER, A VER SI ME ENCUENTRA");

        iguana.gameObject.SetActive(false); // Apagamos el objeto para ahorrar recursos
    }

    public override void Execute(IguanaController iguana)
    {
        // En este estado no hacemos nada, la iguana se queda escondida
    }

    public override void Exit(IguanaController iguana)
    {
        Debug.Log("IGUANA: YA NO ME SIENTO SEGURO, MEJOR SALGO DE NUEVO");
        iguana.gameObject.SetActive(true); // Volvemos a activar el objeto
    }
}
