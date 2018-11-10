using System;
using MLAgents;
using UnityEngine;
using UTJ.FrameCapturer;
using static UTJ.FrameCapturer.RecorderBase;

[RequireComponent(typeof(MovieRecorder))]
public class RecordVideos : MonoBehaviour
{

    [Tooltip("The directory where the videos will be saved. Can also be an absolute path (i.e C:\\Users\\Batman\\Desktop)")]
    public string outputDir = "./Capture";
    public int resolutionWidth = 640;
    public int resolutionHeight = 360;
    [Tooltip("Should the audio be recorded?")]
    public bool captureAudio = true;

    [Tooltip("One new video will be recorded every X academy steps. Note that academy steps will not match with training summary steps if you change the decision frequency of the agent.")]
    public int IntervalInAcademySteps = 10000;
    [Tooltip("Videos will be X academy steps long.")]
    public int DurationInAcademySteps = 1000;
    private bool isRecording = false;
    private int recordingStartStep = -1;
    private int steps = 0;

    private Academy academy;


    private MovieRecorder movieRecorder;

    private void Awake()
    {
        movieRecorder = GetComponent<MovieRecorder>();
        academy = FindObjectOfType<Academy>();
        Camera mCamera = GetComponent<Camera>();
        RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 16, RenderTextureFormat.ARGB32);
        mCamera.targetTexture = rt;
        movieRecorder.targetRT = rt;

        movieRecorder.outputDir = new DataPath(DataPath.Root.Absolute, outputDir);
        movieRecorder.captureAudio = captureAudio;
        movieRecorder.resolutionUnit = RecorderBase.ResolutionUnit.Percent;
        movieRecorder.resolutionPercent = 100;
        movieRecorder.captureControl = CaptureControl.Manual;
    
    }

    private void Start()
    {
        if (DurationInAcademySteps > IntervalInAcademySteps)
        {
            Debug.LogError("The duration of a video should be smaller than the interval !");
        }
    }

    private void FixedUpdate()
    {
        steps = academy.GetTotalCount();
        if (IntervalInAcademySteps != -1 && DurationInAcademySteps != -1)
        {
            if (isRecording && steps - recordingStartStep > DurationInAcademySteps)
            {
                Debug.Log("End Recording " + steps);
                movieRecorder.EndRecording();
                isRecording = false;
            }
            // Order is important in case we want to begin a recording on the same frame we ended one
            else if (!isRecording && steps % IntervalInAcademySteps == 0)
            {
                Debug.Log("Begin Recording " + steps);
                movieRecorder.BeginRecording(DateTime.Now.ToString("dd-MM_HH'h'mm") + "_" + steps);
                isRecording = true;
                recordingStartStep = steps;
            }
        }
    }

}
