using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotValue
{
    Gold,
    Hat,
    Fan,
    Flashlights,
    Firecrackers
}

public class Slot : MonoBehaviour
{
    public SlotValue RandItem;
    public SlotValue StoppedSlot;
    
    [SerializeField] private SlotMachine _slotMachine;
    [Space] 
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedReduction;
    [SerializeField] private float _rotationСompletionValue;
    [SerializeField] private float _maxSpeedForStopping;
    [Space]
    [SerializeField] private float _minYSlotPosition;
    [SerializeField] private float _maxYSlotPosition;
    [Space]
    [SerializeField] private float _yPositionForHat;
    [SerializeField] private float _yPositionForGold;
    [SerializeField] private float _yPositionFirecrackers;
    [SerializeField] private float _yPositionForFlashlights;
    [SerializeField] private float _yPositionForFan;

    private float _timeInterval;
    private float _speed;
    private RectTransform _reacTransform;

    private void Start()
    {
        _reacTransform = GetComponent<RectTransform>();
    }
public IEnumerator Spin()
{
    _timeInterval = _slotMachine.timeInterval;
    _speed = Random.Range(_minSpeed, _maxSpeed);

    while (_speed >= _rotationСompletionValue)
    {
        _speed /= _speedReduction;
        _reacTransform.Translate(Vector2.up * (Time.deltaTime * -_speed));

        if (_reacTransform.localPosition.y < _minYSlotPosition)
            _reacTransform.localPosition = new Vector2(_reacTransform.localPosition.x, _maxYSlotPosition);

        yield return new WaitForSeconds(_timeInterval);
    }

    StartCoroutine(EndSpin());
    yield return null;
}

private IEnumerator EndSpin()
{
    Vector2[] targetPositions = new Vector2[]
    {
        new Vector2(_reacTransform.localPosition.x, _yPositionForHat),
        new Vector2(_reacTransform.localPosition.x, _yPositionForGold),
        new Vector2(_reacTransform.localPosition.x, _yPositionFirecrackers),
        new Vector2(_reacTransform.localPosition.x, _yPositionForFlashlights),
        new Vector2(_reacTransform.localPosition.x, _yPositionForFan)
    };

    Vector2 closestTarget = GetClosestTarget(targetPositions);

    while (Vector2.Distance(_reacTransform.localPosition, closestTarget) > 0.01f)
    {
        _reacTransform.localPosition = Vector2.MoveTowards(_reacTransform.localPosition, closestTarget, _speed * Time.deltaTime);
        yield return null;
    }

    _reacTransform.localPosition = closestTarget;
    CheckResults();
}

private Vector2 GetClosestTarget(Vector2[] targetPositions)
{
    Vector2 closestTarget = targetPositions[0];
    float closestDistance = Vector2.Distance(_reacTransform.localPosition, closestTarget);

    foreach (var target in targetPositions)
    {
        float distance = Vector2.Distance(_reacTransform.localPosition, target);
        if (distance < closestDistance)
        {
            closestTarget = target;
            closestDistance = distance;
        }
    }

    return closestTarget;
}

private void CheckResults()
{
    Dictionary<float, SlotValue> positionToSlotMap = new Dictionary<float, SlotValue>
    {
        { _yPositionForHat, SlotValue.Hat },
        { _yPositionForGold, SlotValue.Gold },
        { _yPositionFirecrackers, SlotValue.Firecrackers },
        { _yPositionForFlashlights, SlotValue.Flashlights },
        { _yPositionForFan, SlotValue.Fan }
    };

    if (positionToSlotMap.TryGetValue(_reacTransform.localPosition.y, out SlotValue slotValue))
    {
        StoppedSlot = slotValue;
    }

    _slotMachine.WaitResults();
}
}