using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace Chi.Utilities.Timeline
{
    public class SubtitleAssetBehaviour : PlayableBehaviour
    {
        public string subtitle;

        public override void OnPlayableCreate(Playable playable) {
            base.OnPlayableCreate(playable);
            
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
        }
        public override void OnBehaviourPlay(Playable playable, FrameData info) {
            base.OnGraphStart(playable);
            Debug.Log(subtitle);
            //Trigger to show subtitle on Subtitle UI
            
        }
        
        public override void OnGraphStop(Playable playable)
        {
            //Trigger to close Subtitle UI
        }
    }
}