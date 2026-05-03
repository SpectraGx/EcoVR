using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlbumController : MonoBehaviour
{
    [Header("Memory Connection")]
    public PhotoCaptureSystem memorySystem;

    [Header("Left Page (UI)")]
    public TextMeshProUGUI textNameSpecies;
    public TextMeshProUGUI textStatus;

    [Header("Right Page (UI)")]
    public RawImage rawPhoto;

    [Header("Navigation Buttons")]
    public Button btnPrevious;
    public Button btnNext;

    // Lista interna para navegar por n de página
    private List<string> speciesDiscovered = new List<string>();
    private int currentPage = 0;

    void OnEnable()
    {
        // Se llama cuando el libro se activa/aparece
        UpdateBook();
    }

    public void UpdateBook()
    {
        // 1. Extraer los nombres de los animales fotografiados
        speciesDiscovered.Clear();
        if (memorySystem != null)
        {
            foreach (var especie in memorySystem.photoSpecies.Keys)
            {
                speciesDiscovered.Add(especie);
            }
        }

        // 2. Reiniciar la pag y mostrar
        currentPage = 0;
        ShowPage();
    }

    public void NextPage()
    {
        if (currentPage < speciesDiscovered.Count - 1)
        {
            currentPage++;
            ShowPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage();
        }
    }

    private void ShowPage()
    {
        // Si no has tomado fotos a ningun animal
        if (speciesDiscovered.Count == 0)
        {
            textNameSpecies.text = "Álbum Vacío";
            textStatus.text = "ES HORA DE EXPLORAR";
            
            rawPhoto.color = new Color(0, 0, 0, 0); // Vuelve la foto transparente
            rawPhoto.texture = null;

            btnPrevious.interactable = false;
            btnNext.interactable = false;
            return;
        }

        // Caso: Mostrar la info del animal actual
        string currentSpecies = speciesDiscovered[currentPage];
        textNameSpecies.text = currentSpecies;
        textStatus.text = "Especie documentada";
        
        rawPhoto.color = Color.white; // Vuelve la foto visible
        rawPhoto.texture = memorySystem.photoSpecies[currentSpecies];

        // Logica para apagar botones si estás en la primera o última página
        btnPrevious.interactable = (currentPage > 0);
        btnNext.interactable = (currentPage < speciesDiscovered.Count - 1);
    }
}
