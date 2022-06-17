using UniRx;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu (fileName = "Character", menuName = "Data/Character")]
    public class CharacterData : UnityEngine.ScriptableObject
    {
        public StringReactiveProperty characterName = new StringReactiveProperty();
        public StringReactiveProperty characterLevel = new StringReactiveProperty();
    }
}
