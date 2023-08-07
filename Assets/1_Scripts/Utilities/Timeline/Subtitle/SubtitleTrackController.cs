using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

namespace Chi.Utilities.Timeline
{
    public class SubtitleTrackController
    {
        private ISubtitleAssetTrack _subtitleAssetTrack;

        public SubtitleTrackController(ISubtitleAssetTrack subtitleTrack) {
            this._subtitleAssetTrack = subtitleTrack;
        }

        public void AutoSetupSubtitle(TextAsset subtitleText, ref string[] subtitles) {
            if (_subtitleAssetTrack.allClips.Count <= 0 &&
                CheckSubtitleTrackHasData(subtitleText, subtitles)) {
                subtitles = ReadScript(subtitleText);
                CreateSubtitleClips(subtitles);
            }
        }

        private bool CheckSubtitleTrackHasData(TextAsset subtitleText, string[] subtitles) {
            if (subtitles?.Any() == false) {
                if (subtitleText) return true;
                else return false;
            }

            return true;
        }


        public string[] ReadScript(TextAsset textAsset) {
            //Read file and turn to sting list
            if (!CheckTextAsset(textAsset)) return null;

            string allText = textAsset.text;
            string[] splitString = SeparateLine(allText);

            return splitString;
        }

        private bool CheckTextAsset(TextAsset textAsset) {
            try {
                if (!textAsset) throw new ArgumentNullException("TextAsset");
                return true;
            } catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }

        private string[] SeparateLine(string allText) {
            if (!CheckTextIsValid(allText)) return null;

            string[] result = allText.Split('\n');

            return result;
        }


        public void CreateSubtitleClips(string[] subtitles) {
            if (!TryCheckSubtitleIsValid(subtitles)) return;

            ClearAllClips();

            for (int i=0;i< subtitles.Length; i++) {
                CreateSingleClip(i, subtitles[i]);
            }
        }

        private bool TryCheckSubtitleIsValid(string[] subtitle) {
            try {
                CheckStringAryIsValid(ref subtitle);
                return true;
            } catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }

        private void CheckStringAryIsValid(ref string[] strAry) {
            if (strAry?.Any() == false)
                throw new ArgumentNullException("subtitles list is null or Empty");

            if (strAry.Any(x => x == null))
                throw new ArgumentNullException("subtitles have null elements");
        }

        private void CreateSingleClip(int index, string subtitle) {

            TimelineClip clip = this._subtitleAssetTrack.CreateClip();

            HandleSubtitleString(clip, index, subtitle);
        }

        private void HandleSubtitleString(TimelineClip clip, int index, string subtitle) {
            if (!this.CheckTextIsValid(subtitle)) return;

            string[] splitedSubtitle = subtitle.Split(',');

            clip.displayName = ClipName(index, splitedSubtitle[2]);
            clip.duration = this._subtitleAssetTrack.Duration;
            clip.start = SetStartTime(index, splitedSubtitle[0]);
            SubtitleAsset asset = clip.asset as SubtitleAsset;
            asset.subtitle = SetSubTitle(splitedSubtitle[1], splitedSubtitle[2]);
        }

        private float SetStartTime(int index, string startTimeText) {
            float startTime = 0;
            if (!string.IsNullOrEmpty(startTimeText)) {
                startTime = float.Parse(startTimeText);
            } else {
                startTime = this._subtitleAssetTrack.InitalSpeed * index;
            }

            return startTime + this._subtitleAssetTrack.TimeOffset;
        }

        private string ClipName(int index, string subtitle) {
            int length = Mathf.Min(5, subtitle.Length);
            return index + subtitle.Substring(0, length);
        }

        private string SetSubTitle(string name, string subtitle) {
            return name + ":" + subtitle;
        }


        public void ClearAllClips() {

            List<TimelineClip> allClips = this._subtitleAssetTrack.allClips;

            if (allClips.Count <= 0) return;

            for(int i = 0; i < allClips.Count; i++) {
                this._subtitleAssetTrack.DeleteClip(allClips[i]);
                i--;
            }
        }

        //------------//
        private bool CheckTextIsValid(string text) {
            if (string.IsNullOrEmpty(text)) {
                Debug.LogWarning("String is Empty or Null");
                return false;
            }
            return true;
        }

    }
}
