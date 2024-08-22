using System;
using Sound;
using UnityEngine;

public class Fire : MonoBehaviour, IMover
{
    public event Action<Fire> OnHit;
    public event Action<Fire> OnFireDestroyed;

    private float _speed;

    public virtual void Initialize(float speed)
    {
        _speed = speed;
    }

    public void StopMove()
        => _speed = 0;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            SoundManagerGame.Instance.PlaySoundBurningFlashLight();
            
            OnHit?.Invoke(this);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("DeathLineFire"))
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnFireDestroyed?.Invoke(this);
    }
}