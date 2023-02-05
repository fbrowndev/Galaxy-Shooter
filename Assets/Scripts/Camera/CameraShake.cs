using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handling behaviour for camera shake
/// </summary>
public class CameraShake : MonoBehaviour
{
    #region Camera Variables
    private Transform _camTransform;

    //Total shake time
    [Header("Shaker Duration")]
    [SerializeField] private float _shakeTime;

    //Amplitude of camera shake
    [Header("Amplitude Settings")]
    [SerializeField] private float _shakeStrength = 0.5f;
    [SerializeField] private float _decreaseFactor = 1.0f;

    Vector3 originalPos;

    private bool _shakeEnabled = false;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _camTransform = GetComponent<Transform>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DamageShake();
    }


    void OnEnable()
    {
        originalPos = transform.localPosition;
    }

    void DamageShake()
    {

        if (_shakeEnabled)
        {
            if (_shakeTime > 0)
            {
                _camTransform.localPosition = originalPos + Random.insideUnitSphere * _shakeStrength;

                _shakeTime -= Time.deltaTime * _decreaseFactor;
            }
            else
            {
                _shakeEnabled = false;
                _camTransform.localPosition = originalPos;
                _shakeTime = 0.5f;
                
            }
        }
    }

    public void ShakeEnabled()
    {
        _shakeEnabled = true;
    }
}
