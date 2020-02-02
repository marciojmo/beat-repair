using UnityEngine;
using System.Collections;
using Prime31.MessageKit;

public class CameraShakerController : MonoBehaviour
{

    private Transform _camera;
    private float _shakeDuration = 0f;
    private float _shakeAmount = 0.7f;
    private float _decreaseFactor = 1.0f;
    private Vector3 _originalPos;


    public void Shake( float duration = 1f, float shakeAmount = 0.5f, float decreaseFactor = 1.0f )
    {
        _shakeDuration = duration;
        _shakeAmount = shakeAmount;
        _decreaseFactor = decreaseFactor;
    }

    void Awake()
    {
        if (_camera == null)
        {
            _camera = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnDamage()
    {
        Shake(0.5f);
    }

    void Start()
    {
        _originalPos = _camera.localPosition;

        MessageKit.addObserver(GameEvents.P1_DAMAGE, OnDamage);
        MessageKit.addObserver(GameEvents.P2_DAMAGE, OnDamage);

    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            _camera.localPosition = _originalPos + Random.insideUnitSphere * _shakeAmount;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            _camera.localPosition = _originalPos;
        }
    }
}