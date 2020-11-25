using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SingleInstance;

    [SerializeField, Tooltip("AudioSource para la música de fondo")]
    private AudioSource _bgm;

    [SerializeField, Tooltip("AudioSource para los efectos de sonidos")]
    private AudioSource _sfx;

    [SerializeField, Tooltip("AudioSource para los efectos de sonidos en loop")]
    private AudioSource _sfxLoop;

    private float _currentVolumen;

    private AudioClip _bgmClip;


    void Awake()
    {
        if (SingleInstance == null)
            SingleInstance = this;
    }

    void Update()
    {
        ChangeAudio();
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

    public void PlaySFXLoop(AudioClip clip)
    {
        _sfxLoop.clip = clip;
        _sfxLoop.Play();
    }

    public void StopSFXLoop()
    {
        _sfxLoop.Stop();
    }

    public bool MusicPlaying()
    {
        return _bgm.isPlaying;
    }

    public AudioMixer GetAudioMixer()
    {
        return _bgm.outputAudioMixerGroup.audioMixer;
    }

    public void SetBgmClip(AudioClip clip)
    {
        _bgmClip = clip;
    }

    public void ChangeAudio()
    {
        GetAudioMixer().GetFloat("BGMVolumen", out _currentVolumen);
        if (!(_currentVolumen <= -80.0f))
            return;
        StartCoroutine(FadeMixerGroup.StartFade(
            AudioManager.SingleInstance.GetAudioMixer(), "BGMVolumen", 3.5f, 0.38f));
        PlayBGM(_bgmClip);
    }
}
