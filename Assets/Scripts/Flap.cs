using System;
using UnityEngine;

public class Flap : MonoBehaviour
{
    private float _maxAngle;
    private float _minAngle;
    private float _rotationSpeed;
    private float _localX;
    private float _defaultLocalX;
    private float _localY;
    private float _localZ;

    public void Initialize(float speed, float angle)
    {
        _localX = transform.localEulerAngles.x;
        _defaultLocalX = _localX;
        
        _localY = transform.localEulerAngles.y;
        _localZ = transform.localEulerAngles.z;
        
        _rotationSpeed = speed;
        _maxAngle = angle;
        _minAngle = -angle;
    }

    public void RotateTo(float direction)
    {
        var step = _rotationSpeed * Time.fixedDeltaTime;
        
        if (direction == 0)
        {
            RotateToDefault(step);
            return;
        }
        
        step *= Math.Abs(direction);
        
        if (direction > 0 && _localX < _maxAngle)
        {
            PitchUpTo(_maxAngle, step);
        }

        if (direction < 0 && _localX > _minAngle)
        {
            PitchDownTo(_minAngle, step);
        }
    }

    private void RotateToDefault(float step)
    {
        if (_localX == _defaultLocalX)
        {
            return;
        }
        
        if (_localX >= _minAngle && _localX < _defaultLocalX)
        {
            PitchUpTo(_defaultLocalX, step);
        }

        if (_localX <= _maxAngle && _localX > _defaultLocalX)
        {
            PitchDownTo(_defaultLocalX, step);
        }
    }
    
    private void PitchUpTo(float point, float step)
    {
        _localX += step;

        if (_localX > point)
        {
            _localX = point;
        }

        transform.localRotation = Quaternion.Euler(_localX, _localY, _localZ);
    }
    
    private void PitchDownTo(float point, float step)
    {
        _localX -= step;

        if (_localX < point)
        {
            _localX = point;
        }
            
        transform.localRotation = Quaternion.Euler(_localX, _localY, _localZ);
    }
}