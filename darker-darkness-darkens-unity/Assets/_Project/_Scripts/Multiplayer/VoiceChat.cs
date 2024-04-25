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

		private void Start()
		{
			_chatListener = GetComponent<AudioSource>();
			if (!isOwned)
				return;

			// TODO: Capture microphone from client not server.
			// CaptureMicrophone();
		}

		private void Update()
		{
			if (!isOwned)
				return;

			// SendAudioData();
		}


		// [ClientRpc]
		// private void CaptureMicrophone()
		// {
		// 	if (!isOwned)
		// 		return;
		//
		// 	_microphone = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
		//
		// 	if (_microphone == null)
		// 	{
		// 		Debug.LogError("No microphone devices found.");
		// 		return;
		// 	}
		//
		// 	_microphoneClip = Microphone.Start(_microphone,
		// 	                                   true,
		// 	                                   1,
		// 	                                   _frequency);
		// 	Debug.Log("Microphone started capturing.");
		// }

		// [ClientRpc]
		// private void SendAudioData()
		// {
		// 	if (!isOwned)
		// 		return;
		// 	
		//
		// 	var microphonePosition = Microphone.GetPosition(_microphone);
		//
		// 	if (microphonePosition < _lastSample)
		// 		_lastSample = 0;
		//
		// 	var sampleCount = microphonePosition - _lastSample;
		// 	if (sampleCount <= 0)
		// 		return;
		//
		// 	var sampleBuffer = new float[sampleCount];
		//
		// 	_microphoneClip.GetData(sampleBuffer, _lastSample);
		//
		// 	var byteData = new byte[sampleBuffer.Length * 4];
		// 	Buffer.BlockCopy(sampleBuffer,
		// 	                 0,
		// 	                 byteData,
		// 	                 0,
		// 	                 byteData.Length);
		//
		// 	_lastSample = microphonePosition;
		//
		// 	SendAudioToClientsData(byteData);
		// }

		// [Server]
		// private void SendAudioToClientsData(byte[] data) => ReadAudioFromServerData(data);
		//
		// [ClientRpc]
		// private void ReadAudioFromServerData(byte[] data)
		// {
		// 	if (isOwned)
		// 		return;
		//
		// 	Debug.Log("Received microphone data.");
		// 	var samples = new float[data.Length / 4];
		// 	Buffer.BlockCopy(data,
		// 	                 0,
		// 	                 samples,
		// 	                 0,
		// 	                 data.Length);
		// 	var receivedAudioSource = _chatListener;
		// 	receivedAudioSource.clip = AudioClip.Create("ReceivedClip",
		// 	                                            samples.Length,
		// 	                                            1,
		// 	                                            _frequency,
		// 	                                            false);
		// 	receivedAudioSource.clip.SetData(samples, 0);
		//
		// 	if (!receivedAudioSource.isPlaying)
		// 		receivedAudioSource.Play();
		// }
	}
}