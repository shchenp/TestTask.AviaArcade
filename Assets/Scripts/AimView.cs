using UnityEngine;

public class AimView : MonoBehaviour
{
    [SerializeField]
    private Transform _aim;
    [SerializeField]
    private Material _material;

    private Color _defaultColor;
    private Color _onEnemyColor = Color.red;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _defaultColor = _material.color;
    }

    void Update()
    {
        transform.position = _aim.position;
        transform.rotation = _aim.rotation;

        var screenPosition = _camera.WorldToScreenPoint(transform.position);
        var ray = _camera.ScreenPointToRay(screenPosition);

        // todo подумать над альтернативной реализацией с меньшей затратой ресурсов
        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.CompareTag(GlobalConstants.EnemyTag))
            {
                _material.color = _onEnemyColor;
                return;
            }
        }

        _material.color = _defaultColor;
    }
    
    
}
