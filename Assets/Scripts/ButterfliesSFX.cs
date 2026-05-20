using UnityEngine;

public class ButterfliesSFX : MonoBehaviour
{
    [Header("SoundSettings")]
    SFXPlayer playerSFX;
    float sfxTimer;

    // Start is called before the first frame update
    void Start()
    {
        //Buscamos el Script de los SFX
        playerSFX = GetComponent<SFXPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReproduceRandomSound();
    }

    void ReproduceRandomSound()
    {
        //Reproduces un sonido cada tanto tiempo aleatotrio entre 10 a 20 segundos.
        if(sfxTimer <= 0)
        {
            playerSFX.PlayRandomSFX();
            sfxTimer = Random.Range(10, 20);
        }
        else
        {
            sfxTimer -= 1 * Time.deltaTime;
        }
    }
}
