using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    private void FixedUpdate()
    {
        var direction = (Vector3.right * _joystick.Vertical + Vector3.up * _joystick.Horizontal) * _speed;
        var newRotation = (transform.rotation.eulerAngles + direction);
        
        _rigidbody.MoveRotation(Quaternion.Euler(newRotation));
    }
}
