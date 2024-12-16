using NaughtyAttributes;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        [BoxGroup("Money")] public int startMoney = 1100;
        [BoxGroup("Gems")] public int startGems = 100;
        [BoxGroup("Energy")] public int startEnergy = 15;

        [BoxGroup("Money")] public int addMoney = 100;
        [BoxGroup("Gems")] public int addGems = 10;
        [BoxGroup("Energy")] public int addEnergy = 1;
    }
}