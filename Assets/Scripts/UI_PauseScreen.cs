using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_PauseScreen : MonoBehaviour
{
    [SerializeField] TMP_Text count;
    [SerializeField] Slider volume;
    [SerializeField] AudioMixer mixer;

    private void OnEnable()
    {
        UpdateCount();
        volume.onValueChanged.AddListener(OnVolumeChange);
        if (mixer != null && mixer.GetFloat("Master", out float masterVol))
        {
            volume.SetValueWithoutNotify(masterVol);
        }
        
    }

    private void OnDisable()
    {
        volume.onValueChanged.RemoveListener(OnVolumeChange);
    }

    public void OnResumePress()
    {
        UI.Instance.TogglePauseScreen();
    }

    public void OnExitPress()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateCount()
    {
        count.text = Grid.OnCount.ToString() + "/25";
    }

    public void OnVolumeChange(float newValue)
    {
        if (mixer == null)
        {
            Debug.LogError("Missing mixer referance");
        }

        mixer.SetFloat("Master", newValue);
    }
}
