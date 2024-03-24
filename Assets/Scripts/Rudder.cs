using System;
using UnityEngine;

public class Rudder : MonoBehaviour
{
    private float _maxAngle;
    private float _minAngle;
    private float _rotationSpeed;
    private float _localX;
    private float _defaultLocalY;
    private float _localY;
    private float _localZ;

    public void Initialize(float speed, float angle)
    {
        _localX = transform.localEulerAngles.x;
        
        _localY = transform.localEulerAngles.y;
        _defaultLocalY = _localY;
        
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
        
        if (direction > 0 && _localY < _maxAngle)
        {
            YawRightTo(_maxAngle, step);
        }

        if (direction < 0 && _localY > _minAngle)
        {
            YawLeftTo(_minAngle, step);
        }
    }

    private void RotateToDefault(float step)
    {
        if (_localY == _defaultLocalY)
        {
            return;
        }
        
        if (_localY >= _minAngle && _localY < _defaultLocalY)
        {
            YawRightTo(_defaultLocalY, step);
        }

        if (_localY <= _maxAngle && _localY > _defaultLocalY)
        {
            YawLeftTo(_defaultLocalY, step);
        }
    }
    
    private void YawRightTo(float point, float step)
    {
        _localY += step;

        if (_localY > point)
        {
            _localY = point;
        }

        transform.localRotation = Quaternion.Euler(_localX, _localY, _localZ);
    }
    
    private void YawLeftTo(float point, float step)
    {
        _localY -= step;

        if (_localY < point)
        {
            _localY = point;
        }
            
        transform.localRotation = Quaternion.Euler(_localX, _localY, _localZ);
    }
}