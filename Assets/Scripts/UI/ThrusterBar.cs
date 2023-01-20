using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handling logic for thruster bar
/// </summary>
public class ThrusterBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetMaxThrust(int thrust)
    {
        _slider.maxValue = thrust;
        _slider.value = thrust;
    }

    public void SetThrust(float thrust)
    {
        _slider.value = thrust;
    }
}
