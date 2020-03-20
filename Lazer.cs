using UnityEngine;

public class Lazer : MonoBehaviour
{
	[Header ("Lazer Length")]
	[SerializeField] float LazerLength;

	[Header ("Lazer Colors")]
	[SerializeField] Gradient LazerColorIdle;
	[SerializeField] Gradient LazerColorDetectFriend;
	[SerializeField] Gradient LazerColorDetectEnemy;

	[Header ("Lazer Lamp")]
	[SerializeField] SpriteRenderer LazerLamp;

	LineRenderer LazerLine;

	void Start ()
	{
		LazerLine = GetComponent <LineRenderer> ();
		ResetLazer ();
	}

	void ResetLazer ()
	{
		LazerLine.SetPosition (1, new Vector3 (0f, LazerLength, 0f));
		LazerLine.colorGradient = LazerColorIdle;
		LazerLamp.color = LazerColorIdle.colorKeys [0].color;
	}

	void Update ()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, LazerLength);
		if (hit.collider != null) {
			//hit something with collider
			if (hit.collider.tag.Equals ("enemy")) {
				//hit enemy
				LazerLine.colorGradient = LazerColorDetectEnemy;
				LazerLamp.color = LazerColorDetectEnemy.colorKeys [0].color;

			} else {
				//hit something else
				LazerLine.colorGradient = LazerColorDetectFriend;
				LazerLamp.color = LazerColorDetectFriend.colorKeys [0].color;
			}
			//resize Lazer
			LazerLine.SetPosition (1, new Vector3 (0f, Vector3.Distance (transform.position, hit.point), 0f));
		} else {
			ResetLazer ();
		}
	}
}
