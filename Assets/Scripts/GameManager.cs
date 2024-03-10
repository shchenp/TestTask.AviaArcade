using Extensions;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlaneController _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private AudioSource _backgroundAudio;
    [Space]
    [SerializeField] private ScreenManager _screenManager;

    protected override void Awake()
    {
        base.Awake();
        
        Instance._screenManager.Start += OnGameStarted;
    }

    private void OnDestroy()
    {
        Instance._screenManager.Start -= OnGameStarted;
    }

    private void OnGameStarted()
    {
       Instance._enemy.OnGameStarted();
       Instance._player.OnGameStarted();
       
       Instance._backgroundAudio.Play();
    }

    public void SetPlayer(PlaneController player)
    {
        _player = player;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
}