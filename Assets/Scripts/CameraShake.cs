using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmount;
    public float ShakeDuration;

    private float _startAmount;
    private float _startDuration;
    private float _currentAmount;
    private float _currentDuration;

    private bool _isRunning;

    public void ShakeCamera()
    {
        _startAmount = ShakeAmount;
        _startDuration = ShakeDuration;
        _currentAmount = ShakeAmount;
        _currentDuration = ShakeDuration;

        if (!_isRunning)
            StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        _isRunning = true;

        while (_currentDuration > 0.1f)
        {
            var rotationAmount = Random.insideUnitSphere * _currentAmount;
            rotationAmount.z = 0;
            var shakePercentage = _currentDuration / _startDuration;

            _currentAmount = _startAmount * shakePercentage;
            _currentDuration = Mathf.Lerp(_currentDuration, 0, Time.deltaTime);

            transform.rotation = Quaternion.Euler(rotationAmount);

            yield return null;
        }

        transform.rotation = Quaternion.identity;
        _isRunning = false;
    }
}
