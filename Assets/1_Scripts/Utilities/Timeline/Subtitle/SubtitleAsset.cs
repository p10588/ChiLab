using UnityEngine;
using UnityEngine.Playables;

namespace Chi.Utilities.Timeline
{
    public class SubtitleAsset : PlayableAsset
    {
        public string subtitle;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            ScriptPlayable<SubtitleAssetBehaviour> playable
                = ScriptPlayable<SubtitleAssetBehaviour>.Create(graph);
            playable.GetBehaviour().subtitle = subtitle;
            return playable;
        }
    }
}