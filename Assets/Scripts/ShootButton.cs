using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// todo добавить звук стрельбы при нажатии на кнопку
[RequireComponent(typeof(Image))]
public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private float _timeUntilNextShot;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private AudioSource _audio;
    
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
        _virtualCamera.enabled = false;
        _isButtonPressed = true;
        
        _audio.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _defaultButtonColor;
        _virtualCamera.enabled = true;
        _isButtonPressed = false;
        
        _audio.Stop();
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