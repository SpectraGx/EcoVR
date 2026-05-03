using System.Collections.Generic;
using UnityEngine;

public class PhotoCaptureSystem : MonoBehaviour
{
    [Header("References Photo Capture")]
    public Camera lensCamera;
    public RenderTexture visorRenderTexture;

    [Header("Anti-Selfie")]
    public Transform playerHead;

    [Header("Memory Management")]
    private int maxRandomPhotos = 3;

    // Diccionario para guardar UNA foto por species 
    public Dictionary<string, Texture2D> photoSpecies = new Dictionary<string, Texture2D>();

    // Cola para las fotos random
    public Queue<Texture2D> photosRandom = new Queue<Texture2D>();

    public UnityEngine.UI.RawImage visorDisplay;

    /// Cuando el jugador presiona el gatillo para tomar la foto
    public void TakePhotograph(string speciesDetected)
    {
        // 1. ¿Es una selfie?
        // Calculamos hacia donde apunta la lente vs hacia donde mira el jugador
        float dotProduct = Vector3.Dot(lensCamera.transform.forward, playerHead.forward);

        // Si el resultado es menor a -0.5, significa que las camaras se estan mirando de frente
        if (dotProduct < -0.5f)
        {
            Debug.LogWarning("¡Lente apuntando al jugador! Foto bloqueada para mantener inmersión.");
            return;
        }

        // 2. CREACION DE LA TEXTURA 
        Texture2D newPhoto = ExtractTexture(visorRenderTexture);

        if (visorDisplay != null)
        {
            visorDisplay.texture = newPhoto;
        }

        // 3. GESTION DE LA MEMORIA (Sobrescritura)
        if (string.IsNullOrEmpty(speciesDetected) || speciesDetected == "Ninguno")
        {
            SaveRandomPhoto(newPhoto);
        }
        else
        {
            SavePhotoSpecies(speciesDetected, newPhoto);
        }
    }

    private Texture2D ExtractTexture(RenderTexture rt)
    {
        // Activamos el RenderTexture para leer sus pixels
        RenderTexture.active = rt;

        // Creamos una textura 2D en blanco con las mismas dimensiones
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        // Copiamos los pixels del RenderTexture a la Textura2D
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply(); // Guardamos los cambios

        // Desactivamos para evitar errores gráficos
        RenderTexture.active = null;

        Debug.Log("Foto capturada y convertida a textura. Tamaño: " + tex.width + "x" + tex.height);

        return tex;
    }

    private void SavePhotoSpecies(string species, Texture2D newPhoto)
    {
        // Si ya habiamos tomado una foto a esta species, DESTRUIMOS la vieja para liberar RAM
        if (photoSpecies.ContainsKey(species))
        {
            Destroy(photoSpecies[species]);
            photoSpecies[species] = newPhoto;
            Debug.Log("Foto de " + species + " actualizada.");
        }
        else
        {
            photoSpecies.Add(species, newPhoto);
            Debug.Log("Primera foto de " + species + " guardada.");
        }
    }

    private void SaveRandomPhoto(Texture2D newPhoto)
    {
        // Si ya tenemos 3 fotos random, sacamos la más vieja y la DESTRUIMOS de la RAM
        if (photosRandom.Count >= maxRandomPhotos)
        {
            Texture2D oldPhoto = photosRandom.Dequeue();
            Destroy(oldPhoto);
        }

        photosRandom.Enqueue(newPhoto);
        Debug.Log("Foto del entorno guardada. Total randoms: " + photosRandom.Count);
    }
}