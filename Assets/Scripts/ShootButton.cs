using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// todo добавить звук стрельбы и смену камеры с сильной тряской при нажатии на кнопку
[RequireComponent(typeof(Image))]
public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private float _timeUntilNextShot;
    
    [SerializeField] private UnityEvent _fireAllGuns;

    private float _time;

    private Color _defaultButtonColor;
    private Color _pressedButtonColor;
    private bool _isButtonPressed;

    private void Awake()
    {
        _defaultButtonColor = _image.color;
        _pressedButtonColor = _image.color;
        _pressedButtonColor.a /= 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = _pressedButtonColor;
        
        _isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _defaultButtonColor;
        
        _isButtonPressed = false;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        var isCanFire = _time > _timeUntilNextShot;
        if ((_isButtonPressed || Input.GetKeyDown(KeyCode.Space)) && isCanFire)
        {
            _fireAllGuns?.Invoke();
            _time = 0;
        }
    }
}