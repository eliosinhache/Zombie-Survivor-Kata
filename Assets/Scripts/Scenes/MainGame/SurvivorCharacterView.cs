using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class SurvivorCharacterView: CharacterView
    {
        [SerializeField] private TextMeshProUGUI _experience;
        public void SetExperience(float checkExperience)
        {
            _experience.text = $"Experience: {checkExperience}";
        }
    }
}