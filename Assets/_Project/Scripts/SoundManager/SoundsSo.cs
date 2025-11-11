using UnityEngine;

namespace CardsAndCaverns.SoundManager
{
    [CreateAssetMenu(menuName = "Sounds", fileName = "Sounds SO")]
    public class SoundsSO : ScriptableObject
    {
        public SoundList[] sounds;
    }
}