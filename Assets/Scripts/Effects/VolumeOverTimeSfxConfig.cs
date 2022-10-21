using UnityEngine;

namespace SinkingShips.Effects
{
    [CreateAssetMenu(fileName = "VolumeOverTimeSfxConfig", menuName = "Audio/VolumeOverTimeSfx")]
    public class VolumeOverTimeSfxConfig : ScriptableObject
    {
        [field: Header("Play Options")]
        [field: SerializeField, Tooltip("PlayFromRandomSecond")]
        public bool PlayFromRandomSecond { get; private set; } = true;

        [field: SerializeField, Tooltip("time to reach max volume (linearly) when playing sound, seconds")]
        public float TimeToMaxVolume { get; private set; } = 3f;
        [field: SerializeField, Tooltip("time to stop sfx while silencing it (linearly) over time, seconds")]
        public float TimeToStopSfx { get; private set; } = 1f;

        ////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
