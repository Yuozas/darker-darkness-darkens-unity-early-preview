using System;
using Mirror;
using UnityEngine;

namespace Euphelia.Multiplayer
{
	[RequireComponent(typeof(AudioSource))]
	public class VoiceChat : NetworkBehaviour
	{
		[SerializeField] private int         _frequency = 44100;
		private                  AudioSource _chatListener;
		private                  int         _lastSample;
		private                  string      _microphone;

		private AudioClip _microphoneClip;

		private void Start() => _chatListener = GetComponent<AudioSource>();

		private void Update()
		{
			if (!isOwned) // Check for hasAuthority instead of authority
				return;

			SendAudioData();
		}

		public override void OnStartAuthority()
		{
			if (!isOwned)
				return;

			// No need to check if it's the server, OnStartAuthority is only called for the client that has authority.
			CaptureMicrophone();
		}

		private void CaptureMicrophone()
		{
			_microphone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

			if (_microphone == null)
			{
				Debug.LogError("No microphone devices found.");
				return;
			}

			_microphoneClip = Microphone.Start(_microphone,
			                                   true,
			                                   1,
			                                   _frequency);
		}

		private void SendAudioData()
		{
			if (!Microphone.IsRecording(_microphone))
				return;

			var microphonePosition = Microphone.GetPosition(_microphone);

			if (microphonePosition < _lastSample)
				_lastSample = 0;

			var sampleCount = microphonePosition - _lastSample;
			if (sampleCount <= 0)
				return;

			var sampleBuffer = new float[sampleCount];

			_microphoneClip.GetData(sampleBuffer, _lastSample);

			var byteData = new byte[sampleBuffer.Length * 4];
			Buffer.BlockCopy(sampleBuffer,
			                 0,
			                 byteData,
			                 0,
			                 byteData.Length);

			_lastSample = microphonePosition;

			CmdSendAudioData(byteData);
		}


		[Command]
		private void CmdSendAudioData(byte[] data) => RpcReceiveAudioData(data, netId);

		[ClientRpc]
		private void RpcReceiveAudioData(byte[] data, uint callerId)
		{
			if (callerId == netId)
				return;

			var samples = new float[data.Length / 4];
			Buffer.BlockCopy(data,
			                 0,
			                 samples,
			                 0,
			                 data.Length);
			var receivedAudioSource = _chatListener;
			receivedAudioSource.clip = AudioClip.Create("ReceivedClip",
			                                            samples.Length,
			                                            1,
			                                            _frequency,
			                                            false);
			receivedAudioSource.clip.SetData(samples, 0);

			if (!receivedAudioSource.isPlaying)
				receivedAudioSource.Play();
		}
	}
}