using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderScript : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] public TextMeshProUGUI volumeValuetext;
    [SerializeField] public VolumeController audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float currentVolume = audioManager.GetVolumePercent();
        slider.value= currentVolume;
        volumeValuetext.text = currentVolume.ToString("0") + "%";

        slider.onValueChanged.AddListener((v) => {
            volumeValuetext.text = v.ToString("0") + "%";
            audioManager.SetVolume(v);
        });
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Slider: " + slider.value);
        Debug.Log("Mixer: " + audioManager.GetVolumePercent());
    }
}
