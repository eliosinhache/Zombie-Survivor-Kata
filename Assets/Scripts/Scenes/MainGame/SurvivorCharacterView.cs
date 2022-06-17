using System;
using Classes;
using ScriptableObject;
using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class SurvivorCharacterView: CharacterView, IObserver
    {
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private CharacterData _selectedSurvivorData;
        public void SetExperience(float checkExperience)
        {
            _experience.text = $"Exp: {checkExperience}";
        }

        public void SetNameView(string name)
        {
            _selectedSurvivorData.characterName.Value = name;
        }

        public void ReceiveUpdate()
        {
            if (_selectedSurvivorData.characterName.Value == name)
            {
                DecreaseLife(_lifes.Count - _selectedSurvivorData.lifes);
            }
        }
    }
}