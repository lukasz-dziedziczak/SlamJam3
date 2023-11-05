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
    [SerializeField] TMP_Text count;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        UpdateCount();
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

    public static void UpdateCount()
    {
        Instance.count.text = Grid.OnCount.ToString();
    }

}
