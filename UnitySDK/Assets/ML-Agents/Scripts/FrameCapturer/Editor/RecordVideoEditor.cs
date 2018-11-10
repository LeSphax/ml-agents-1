using System;
using UnityEditor;
using UnityEngine;
using UTJ.FrameCapturer;

[CustomEditor(typeof(RecordVideos))]
public class RecordVideoEditor : Editor
{


    public virtual void AudioConfig()
    {
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();

        var recorder = target as RecordVideos;
        var so = serializedObject;
        EditorGUILayout.PropertyField(so.FindProperty("outputDir"), true);
        EditorGUILayout.PropertyField(so.FindProperty("IntervalInAcademySteps"), true);
        EditorGUILayout.PropertyField(so.FindProperty("DurationInAcademySteps"), true);
        if (recorder.DurationInAcademySteps > recorder.IntervalInAcademySteps){
            EditorGUILayout.HelpBox("The Duration of the video shouldn't be longer than the Interval !", MessageType.Error);
        }

        EditorGUILayout.PropertyField(so.FindProperty("resolutionWidth"));
        EditorGUILayout.PropertyField(so.FindProperty("resolutionHeight"));
        EditorGUILayout.PropertyField(so.FindProperty("captureAudio"));
        so.ApplyModifiedProperties();
    }
}
