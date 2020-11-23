using System.Diagnostics.SymbolStore;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SingleInstance;

    [SerializeField, Tooltip("AudioSource para la música de fondo")]
    private AudioSource _bgm;

    [SerializeField, Tooltip("AudioSource para los efectos de sonidos")]
    private AudioSource _sfx;

    void Awake()
    {
        if (SingleInstance == null)
            SingleInstance = this;
    }

    // Reproducir música de fondo
    public void PlayBGM(AudioClip clip)
    {
        _bgm.clip = clip;
        _bgm.Play();
    }

    // Reproducir efecto de sonido
    public void PlaySFX(AudioClip clip)
    {
        _sfx.PlayOneShot(clip);
    }


    public bool MusicPlaying()
    {
        return _bgm.isPlaying;
    }
}