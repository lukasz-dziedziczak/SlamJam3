using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WinScreen : MonoBehaviour
{
    [SerializeField] TMP_Text time;

    private void OnEnable()
    {
        time.text = Grid.TimeString;
    }

    public void OnMainMenuButtonPress()
    {
        SceneManager.LoadScene(0);
    }
}
