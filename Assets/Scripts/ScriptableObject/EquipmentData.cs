using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Data/Equipment")]
public class EquipmentData : UnityEngine.ScriptableObject, ISubject
{
    private Equipment[] _equipment = new Equipment[5];
    
    public void Attach(IObserver observer)
    {
        throw new System.NotImplementedException();
    }

    public void Detach(IObserver observer)
    {
        throw new System.NotImplementedException();
    }

    public void Notify()
    {
        throw new System.NotImplementedException();
    }
}
