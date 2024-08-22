using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IMover
{
    public event Action<Item> DeathOnLine;
    public event Action<Item> OnItemDestroyed;
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
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }

    public void MoveTo(Vector3 position) => transform.position = position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeathLineItem"))
        {
            DeathOnLine?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnItemDestroyed?.Invoke(this);
    }
}