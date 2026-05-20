using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [Header("SoundEffects")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] SFXList;

    /// <summary>
    /// Reproduce el audio dentro del Audio Source, utiul cuando solo se tiene un sonido
    /// </summary>
    public void PlaySingleSFX()
    {
        source.Play();
    }

    public void PlayStopSFX()
    {
        source.Stop();
    }
    
    /// <summary>
    /// Reproduce un sonido aleatorio de la la lista de AudioClips
    /// </summary>
    public void PlayRandomSFX()
    {
        source.clip = SFXList[Random.Range(0, SFXList.Length)];
        source.Play();
    }
}
