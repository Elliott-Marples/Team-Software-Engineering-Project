using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 100f);
        float volume = Mathf.Log10(value / 100f) * 20f;
        mixer.SetFloat("MasterVolume", volume);
    }

    public float GetVolumePercent()
    {
        mixer.GetFloat("MasterVolume", out float dB);

        float value = Mathf.Pow(10f, dB / 20f) * 100f;

        return value;
    }
}