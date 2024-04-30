using System;
using Mirror;
using UnityEngine;

namespace Euphelia.Multiplayer
{
	public class VoiceChat : NetworkBehaviour
	{
		[SerializeField] private int         _frequency = 44100;
		private                  AudioSource _chatListener;
		private                  Transform   _followTransform;
		private                  int         _lastSample;
		private                  string      _microphone;

		private AudioClip _microphoneClip;

		private void Update()
		{
			if (!isOwned)
				return;

			transform.position = _followTransform.position;
			SendAudioData();
		}

		public override void OnStartClient()
		{
			if (isOwned)
			{
				CaptureMicrophone();
				_followTransform = NetworkClient.localPlayer.transform;
				return;
			}

			_chatListener = gameObject.AddComponent<AudioSource>();
		}

		public override void OnStopClient()
		{ }

		private void CaptureMicrophone()
		{
			if (!isOwned)
				return;

			_microphone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

			if (_microphone == null)
				return;

			_microphoneClip = Microphone.Start(_microphone,
			                                   true,
			                                   1,
			                                   _frequency);
		}

		private void SendAudioData()
		{
			if (!isOwned)
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

			SendAudioToClientsData(byteData);
		}

		[Command]
		private void SendAudioToClientsData(byte[] audioData) => PlayAudio(audioData);

		[ClientRpc(includeOwner = false)]
		private void PlayAudio(byte[] data)
		{
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