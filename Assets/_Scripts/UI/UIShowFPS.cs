using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIShowFPS : MonoBehaviour
{
	[SerializeField] TMP_Text fpsText;

	// Variables to calculate FPS
	private int frameCount = 0;
	private float deltaTime = 0.0f;
	private float fps = 0.0f;
	private float updateInterval = 0.2f;

	void Update()
	{
		// Increment frame count and add deltaTime
		frameCount++;
		deltaTime += Time.unscaledDeltaTime;

		// Update FPS calculation at regular intervals
		if (deltaTime > updateInterval)
		{
			fps = frameCount / deltaTime;
			frameCount = 0;
			deltaTime -= updateInterval;

			fpsText.text = string.Format("{0:0.0} FPS", fps);
		}
	}
}
