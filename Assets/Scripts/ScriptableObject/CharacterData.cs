using System;
using System.Collections.Generic;
using Classes;
using UniRx;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu (fileName = "Character", menuName = "Data/Character")]
    public class CharacterData : UnityEngine.ScriptableObject, ISubject
    {
        public StringReactiveProperty characterName = new StringReactiveProperty();
        public StringReactiveProperty characterLevel = new StringReactiveProperty();
        public int lifes;
        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.ReceiveUpdate();
            }
        }
    }
}
