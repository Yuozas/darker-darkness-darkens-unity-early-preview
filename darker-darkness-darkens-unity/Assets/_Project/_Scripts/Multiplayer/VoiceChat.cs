using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoiceChat : NetworkBehaviour
{
    private AudioSource _chatListener;
    
    private AudioClip _microphoneClip;
    private string _microphone;
    private int _lastSample;
    
    [SerializeField] private int _frequency = 44100;
    
    private void Start()
    {    
        _chatListener = GetComponent<AudioSource>();
        if (authority) {
            CaptureMicrophone();
        }
    }

    private void CaptureMicrophone() {
        // Select the first available microphone
        _microphone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null; 
        if (_microphone == null)
        {
            Debug.LogError("No microphone devices found.");
            return;
        }
        // Start recording with looping
        _microphoneClip = Microphone.Start(_microphone, true, 1, _frequency); 
    }

    private void Update() 
    {
        if (!authority)
            return;
        
        SendAudioData();
    }

    private void SendAudioData() {
        // Check if the microphone is currently recording
        if (!Microphone.IsRecording(_microphone))
            return;
    
        // Get the current position of the microphone
        var microphonePosition = Microphone.GetPosition(_microphone);
    
        // If microphonePosition is less than _lastSample, it means the recording has looped
        if (microphonePosition < _lastSample)
            _lastSample = 0;

        // Calculate the number of samples to read
        var sampleCount = microphonePosition - _lastSample;
        if (sampleCount <= 0)
            return;  // No new samples to read

        // Allocate a buffer to hold the samples
        var sampleBuffer = new float[sampleCount];
    
        // Extract data starting from _lastSample up to the current position
        _microphoneClip.GetData(sampleBuffer, _lastSample);

        // Convert float array to byte array
        var byteData = new byte[sampleBuffer.Length * 4];
        Buffer.BlockCopy(sampleBuffer, 0, byteData, 0, byteData.Length);

        // Update _lastSample to the current microphone position
        _lastSample = microphonePosition;

        // Send the byte data via a Command (assuming CmdSendAudioData is correctly implemented)
        CmdSendAudioData(byteData);
    }


    [Command]
    private void CmdSendAudioData(byte[] data) 
    {
        RpcReceiveAudioData(data, netId);
    }

    [ClientRpc]
    private void RpcReceiveAudioData(byte[] data, uint callerId) 
    {
        if (callerId == netId) {
            return; // Ignore data from self
        }
        var samples = new float[data.Length / 4];
        Buffer.BlockCopy(data, 0, samples, 0, data.Length);
        var receivedAudioSource = _chatListener;
        receivedAudioSource.clip = AudioClip.Create("ReceivedClip", samples.Length, 1, 44100, false);
        receivedAudioSource.clip.SetData(samples, 0);
        if (!receivedAudioSource.isPlaying) {
            receivedAudioSource.Play();
        }
    }
}
