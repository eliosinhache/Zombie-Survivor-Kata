using System;
using ScriptableObject;
using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class SurvivorCharacterView: CharacterView
    {
        [SerializeField] private TextMeshProUGUI _experience;
        [SerializeField] private CharacterData _characterData;
        public void SetExperience(float checkExperience)
        {
            _experience.text = $"Exp: {checkExperience}";
        }

        public void SetNameView(string name)
        {
            _characterData.characterName.Value = name;
        }

    }
}