using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour 
{
	public GameObject playButton;
	public GameObject quitButton;

	public Transform cam;

	//Called when the mouse enters a button. Enlarges it.
	public void HoverEnter (GameObject obj)
	{
		obj.transform.localScale = new Vector3(1.1f, 1.1f, 1);
		Shake(0.15f, 0.05f, 30.0f);
	}

	//Called when the mouse exits a button. Shrinks it back to default size.
	public void HoverExit (GameObject obj)
	{
		obj.transform.localScale = new Vector3(1, 1, 1);
	}

	//Called when the "Play" button gets pressed. Loads the game.
	public void Play ()
	{
		Application.LoadLevel(1);
	}

	//Called when the "Quit" button gets pressed. Quits the application.
	public void Quit ()
	{
		Application.Quit();
	}

	//Same as other shake function.
	public void Shake (float duration, float amount, float intensity)
	{
		StartCoroutine(ShakeCam(duration, amount, intensity));
	}

	//Shakes the camera.
	IEnumerator ShakeCam (float dur, float amount, float intensity)
	{
		float t = dur;
		Vector3 originalPos = cam.position;
		Vector3 targetPos = Vector3.zero;

		while(t > 0.0f)
		{
			if(targetPos == Vector3.zero)
			{
				targetPos = Random.insideUnitCircle * amount;
				targetPos = new Vector3(targetPos.x, targetPos.y, -10);
			}

			cam.position = Vector3.Lerp(cam.position, targetPos, intensity * Time.deltaTime);

			if(Vector3.Distance(cam.position, targetPos) < 0.02f)
			{
				targetPos = Vector3.zero;
			}

			t -= Time.deltaTime;
			yield return null;
		}

		cam.position = originalPos;
	}
}
