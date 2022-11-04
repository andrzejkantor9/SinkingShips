using UnityEngine;

using SinkingShips.Effects;

namespace SinkingShips.Audio
{
    public abstract class AudioPlayer : EffectPlayer
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////

        public abstract void Play(AudioClip audioClip);
    }
}
