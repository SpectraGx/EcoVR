using System;
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
    public TextMeshProUGUI textProgress; 

    [Header("Right Page (UI)")]
    public RawImage rawPhoto;

    [Header("Navigation Buttons")]
    public Button btnPrevious;
    public Button btnNext;

    // Lista que contiene el nombre de TODAS las especies posibles
    private List<string> allSpeciesInGame = new List<string>();
    private int currentPage = 0;

    void Awake()
    {
        // Extraemos automáticamente los nombres de enum 'Species'
        foreach (string speciesName in Enum.GetNames(typeof(Species)))
        {
            allSpeciesInGame.Add(speciesName);
        }
    }

    void OnEnable()
    {
        UpdateBook();
    }

    public void UpdateBook()
    {
        // Regresamos a la primera página cada vez que el jugador abre el libro
        currentPage = 0;
        ShowPage();
    }

    public void NextPage()
    {
        if (currentPage < allSpeciesInGame.Count - 1)
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
        string currentSpecies = allSpeciesInGame[currentPage];

        // Contamos cuantas fotos tenemos realmente guardadas
        int photosTaken = 0;
        if (memorySystem != null) photosTaken = memorySystem.photoSpecies.Count;
        
        if (textProgress != null)
        {
            textProgress.text = "Progreso: " + photosTaken + " / " + allSpeciesInGame.Count;
        }

        // VERIFICAR SI TENEMOS LA FOTO DE LA ESPECIE ACTUAL
        if (memorySystem != null && memorySystem.photoSpecies.ContainsKey(currentSpecies))
        {
            // EL JUGADOR YA DESCUBRIO ESTA ESPECIE
            textNameSpecies.text = currentSpecies;
            textStatus.text = "Especie documentada";
            
            rawPhoto.color = Color.white; 
            rawPhoto.texture = memorySystem.photoSpecies[currentSpecies];
        }
        else
        {
            // EL JUGADOR AUN NO ENCUENTRA ESTA ESPECIE
            textNameSpecies.text = "???";
            textStatus.text = "Sin documentar";
            
            // Ponemos el cuadro negro transparente para simular vacío
            rawPhoto.color = new Color(0, 0, 0, 0.2f); 
            rawPhoto.texture = null;
        }

        btnPrevious.interactable = (currentPage > 0);
        btnNext.interactable = (currentPage < allSpeciesInGame.Count - 1);
    }
}