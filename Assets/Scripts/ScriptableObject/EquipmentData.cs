using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using NSubstitute;
using ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Data/Equipment")]
public class EquipmentData : UnityEngine.ScriptableObject, ISubject
{
    private Equipment[] _equipment = new Equipment[5];
    private List<IObserver> _observers = new List<IObserver>();
    [SerializeField] private CharacterData _survivorSelected;

    public Equipment[] ReturnEquipments()
    {
        return _equipment;
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (IObserver observer in _observers)
        {
            observer.ReceiveUpdate();
        }
    }

    public void RefreshEquipment(List<Equipment> returnAllEquipment)
    {
        _equipment = new Equipment[5];
        for (int index = 0; index < returnAllEquipment.Count; index++)
        {
            _equipment[index] = returnAllEquipment[index];
        }
        Notify();
    }
}
