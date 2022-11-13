using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";

    private float musicFloat, soundFXfloat;

    public AudioSource musicAudio;
    public AudioSource[] soundFXAudio;

    void Awake()
    {
        ContinueSettings();
    }

    private void ContinueSettings()
    {
        musicFloat = PlayerPrefs.GetFloat(MusicPref);
        soundFXfloat = PlayerPrefs.GetFloat(SFXPref);

        musicAudio.volume = musicFloat; ;

        for (int i = 0; i < soundFXAudio.Length; i++)
        {
            soundFXAudio[i].volume = soundFXfloat;
        }
    }
}
