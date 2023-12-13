using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnFixedUpdate();
public class FarmGrownController : MonoBehaviour
{
    private static FarmGrownController controller;
    public static FarmGrownController Controller => controller;

    public event OnFixedUpdate OnFixedUpdateEvent;
    private void Awake()
    {
        if (Controller == null)
            controller = this;
    }

    private void FixedUpdate()
    {
        OnFixedUpdateEvent?.Invoke();
    }
}
