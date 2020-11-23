using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public AudioClip backgroundClip;

    private void Update()
    {
        if (AudioManager.SingleInstance.MusicPlaying())
            return;
        AudioManager.SingleInstance.PlayBGM(backgroundClip);
    }
}