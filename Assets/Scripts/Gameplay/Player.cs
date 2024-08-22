using System.Collections;
using System.Collections.Generic;
using Sound;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IMovePlayer, IAttacker
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private MovementHandler _movementHandler;

    private int _currentPosition = 1;

    private int _speedMove;
    private float _speedDamageRate;
    private float _speedAttack;
    private Fire _firePrefab;

    private bool _isMove;

    private Coroutine _spawnFire;

    private List<Fire> _fires = new List<Fire>();

    [Inject]
    private void Construct(PlayerStatsConfig statsConfig)
    {
        _speedMove = statsConfig.SpeedMove;
        _speedDamageRate = statsConfig.SpeedDamageRate;
        _firePrefab = statsConfig.PrefabFire;
        _speedAttack = statsConfig.SpeedAttack;
    }

    private void Start()
    {
        StartWork();
    }

    private void OnEnable()
    {
        _movementHandler.RightMove += MoveRight;
        _movementHandler.LeftMove += MoveLeft;
    }

    private void OnDisable()
    {
        _movementHandler.RightMove -= MoveRight;
        _movementHandler.LeftMove -= MoveLeft;
    }

    private void Update()
    {
        Move();
    }

    public void StartWork()
    {
        StopWork();
        _spawnFire = StartCoroutine(FireAttack());
    }

    public void StopWork()
    {
        if (_spawnFire != null)
            StopCoroutine(_spawnFire);
    }

    private void HandleFireHit(Fire fire)
    {
        UIGameManager.Instance.UpdateScore();
        fire.OnHit -= HandleFireHit;
    }
    
    public void StomMoveFire()
    {
        foreach (var fire in _fires)
            fire.StopMove();
        
        StopWork();
    }

    public void Attack()
    {
        Fire fire = Instantiate(_firePrefab, transform.position, Quaternion.identity);
        fire.Initialize(_speedAttack);
        fire.OnHit += HandleFireHit;
        
        _fires.Add(fire);
        
        SoundManagerGame.Instance.PlaySoundFireShot();
        
        fire.OnFireDestroyed += HandleFireDestroyed; // Подписка на событие уничтожения
    }

    private void HandleFireDestroyed(Fire fire)
    {
        _fires.Remove(fire);
        fire.OnFireDestroyed -= HandleFireDestroyed; // Отписка от события
    }
    
    private void Move()
        => transform.position = Vector3.MoveTowards(transform.position, _positions[_currentPosition].position,
            _speedMove * Time.deltaTime);

    public void MoveLeft()
    {
        if (_currentPosition < _positions.Length - 1)
        {
            _currentPosition++;
        }
    }

    public void MoveRight()
    {
        if (_currentPosition > 0)
        {
            _currentPosition--;
        }
    }

    private IEnumerator FireAttack()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(_speedDamageRate);
        }
    }
}