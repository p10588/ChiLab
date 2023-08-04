using System;
using UnityEditor;
using UnityEngine;
using Chi.Utilities.Timeline;

[CustomEditor(typeof(SubtitleAssetTrack))]
public class SubtitleTrackEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        SubtitleAssetTrack track = (SubtitleAssetTrack)target;

        CreateButtons(track);
    }

    private void CreateButtons(SubtitleAssetTrack track) {
        OnButtonClick("ReadScript", track.ReadScript);
        OnButtonClick("CreateClips", track.CreateSubtitleClips);
        OnButtonClick("ClearAllClips", track.ClearAllClips);
    }

    private  void OnButtonClick(string btnText, Action action) {
        if (GUILayout.Button(btnText)) {
            action.Invoke();
        }
    }
}
