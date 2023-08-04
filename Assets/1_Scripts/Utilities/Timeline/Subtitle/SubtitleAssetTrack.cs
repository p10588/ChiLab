using UnityEngine.Timeline;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Playables;
using System.Linq;
using System;

namespace Chi.Utilities.Timeline
{
    public interface ISubtitleAssetTrack {
        float Duration { get; }
        float InitalSpeed { get; }
        float TimeOffset { get; }
        List<TimelineClip> allClips { get; }
        TimelineClip CreateClip();
        void DeleteClip(TimelineClip clip);
    }

    [TrackClipType(typeof(SubtitleAsset), false)]
    public class SubtitleAssetTrack : TrackAsset, ISubtitleAssetTrack
    {
        [SerializeField] private TextAsset subtitleText;
        [SerializeField] private string[] subtitles;
        [SerializeField] private float timeOffset = 0;

        private readonly float _duration = 1;
        private readonly float _initalSpeed = 2;

        private SubtitleTrackController _controller;
        private SubtitleAsset _myAsset;

        protected override Playable CreatePlayable(PlayableGraph graph, GameObject gameObject, TimelineClip clip) {
            _myAsset = clip.asset as SubtitleAsset;
            return base.CreatePlayable(graph, gameObject, clip);
        }

        private void OnEnable() {
            _controller = new SubtitleTrackController(this);
            //AutoSetupSubtitle();
        }

        private void OnValidate() {
            //if(this.subtitleText) AutoSetupSubtitle();
        }

        private void AutoSetupSubtitle() {
            if (this.m_Clips.Count <= 0 && CheckSubtitleTrackData()) {
                ReadScript();
                CreateSubtitleClips();
            }
        }

        private bool CheckSubtitleTrackData() {
            if (this.subtitles.Length <= 0) {
                if (this.subtitleText) return true;
                else return false;
            }
            return true;
        }

        public void ReadScript() {
            this.subtitles = this._controller.ReadScript(this.subtitleText);
        }

        public void CreateSubtitleClips() {
            this._controller.CreateSubtitleClips(this.subtitles);
        }

        public void ClearAllClips() {
            this._controller.ClearAllClips();
        }


        #region ISubtitleAssetTrack Implementation

        float ISubtitleAssetTrack.Duration => this._duration;
        float ISubtitleAssetTrack.InitalSpeed => this._initalSpeed;
        float ISubtitleAssetTrack.TimeOffset => this.timeOffset;
        List<TimelineClip> ISubtitleAssetTrack.allClips => this.m_Clips;

        TimelineClip ISubtitleAssetTrack.CreateClip()  {
            return this.CreateClip<SubtitleAsset>();
        }
        void ISubtitleAssetTrack.DeleteClip(TimelineClip clip) {
            try {
                this.DeleteClip(clip);
            } catch (Exception e){
                Debug.LogError(e);
            }
            
        }

        #endregion

    }

    
}