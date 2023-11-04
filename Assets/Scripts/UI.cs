using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    [field: SerializeField] public Image Pointer { get; private set; }
    [SerializeField] Color grayPointer;
    [SerializeField] Color greenPointer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        //UpdatePointer(false);
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


}
