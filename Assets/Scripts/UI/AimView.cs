using UnityEngine;
using UnityEngine.UI;

public class AimView : MonoBehaviour
{
    [SerializeField]
    private Transform _aim;
    [SerializeField]
    private Image _image;

    private Color _defaultColor;
    private Color _onEnemyColor = Color.red;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _defaultColor = _image.color;
    }

    private void Update()
    {
        transform.position = _aim.position;
        transform.rotation = _aim.rotation;

        var screenPosition = _camera.WorldToScreenPoint(transform.position);
        var ray = _camera.ScreenPointToRay(screenPosition);
        
        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.CompareTag(GlobalConstants.EnemyTag))
            {
                _image.color = _onEnemyColor;
                return;
            }
        }

        _image.color = _defaultColor;
    }
}