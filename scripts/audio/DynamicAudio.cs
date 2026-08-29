using Godot;
using System;

public partial class DynamicAudio : AudioStreamPlayer {
	private const float NO_VOLUME = -60.0f;

    [Export]
    public float bgmMaxVolume = 0;

	[Export]
	public float transitionSpeed;

	[Export]
	public AudioStreamSynchronized synchronizedStream;

	private int _activeStream = 0;

	public override void _Ready() {
		// Normal Game BGM
        synchronizedStream.SetSyncStreamVolume(0, NO_VOLUME);

		// Frantic Game BGM
        synchronizedStream.SetSyncStreamVolume(1, NO_VOLUME);

		// Menu BGM
		synchronizedStream.SetSyncStreamVolume(2, bgmMaxVolume);
	}

	public override void _Process(double delta) {
		for (int i = 0; i < synchronizedStream.GetLength(); i++) {
			float streamVol = synchronizedStream.GetSyncStreamVolume(i);

			if (i == _activeStream) {
				if (streamVol < bgmMaxVolume)
					synchronizedStream.SetSyncStreamVolume(i, streamVol + transitionSpeed * 1.5f * (float)delta);
			} else {
				if (streamVol > NO_VOLUME)
					synchronizedStream.SetSyncStreamVolume(i, streamVol - transitionSpeed * (float)delta);
			}
		}
	}

	public void SetActiveStream(int activeStream) {
		_activeStream = activeStream;
	}
}
