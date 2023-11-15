using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    [field: SerializeField] public Image Pointer { get; private set; }
    [SerializeField] Color grayPointer;
    [SerializeField] Color greenPointer;
    
    [field: SerializeField] public UI_Blackscreen Blackscreen { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public UI_PauseScreen PauseScreen { get; private set; }
    [field: SerializeField] public UI_WinScreen WinScreen { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        Player.Input.Pause += TogglePauseScreen;
        CursorActive(false);
        PauseScreen.gameObject.SetActive(false);
        WinScreen.gameObject.SetActive(false);
    }

    public static void UpdatePointer(bool isActive)
    {
        if (isActive)
        {
            Instance.Pointer.color = Instance.greenPointer;
        }
        else
        {
            Instance.Pointer.color = Instance.grayPointer;
        }
    }

    

    public void TogglePauseScreen()
    {
        if (WinScreen.gameObject.activeSelf)
        {
            WinScreen.OnMainMenuButtonPress();
        }
        else
        {
            if (PauseScreen.gameObject.activeSelf)
            {
                PauseScreen.gameObject.SetActive(false);
                CursorActive(false);
                Player.Movement.Enabled = true;
            }
            else
            {
                PauseScreen.gameObject.SetActive(true);
                CursorActive(true);
                Player.Movement.Enabled = false;
            }
        }
    }

    public static void CursorActive(bool isActive)
    {
        if (isActive) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = isActive;
    }

    public static void ShowWinScreen()
    {
        CursorActive(true);
        Instance.Player.Movement.Enabled = false;
        Instance.WinScreen.gameObject.SetActive(true);
    }

}
