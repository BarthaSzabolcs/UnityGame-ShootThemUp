using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MovementPatterns/PointToPoint")]
public class PointToPoint_Movement : MovingPattern
{
	[SerializeField] Vector2[] checkpoints;
	[SerializeField] float distance;
	[SerializeField] float rotateSpeed;
	[SerializeField] float speed;

	#region Instance Variables
	Vector2[] relativeCheckpoints;
	int checkpointIndex = 0;
	Rigidbody2D self;
	#endregion

	public override void SetUp(Rigidbody2D rb)
	{
		self = rb;

		relativeCheckpoints = new Vector2[checkpoints.Length];

		//Vector2 difference = checkpoints[0] - self.position;

		for (int i = 0; i < checkpoints.Length; i++)
		{
			relativeCheckpoints[i] = self.position + checkpoints[i];
		}
	}

	public override void Move()
	{
		if(checkpointIndex < checkpoints.Length)
		{
			NextCheckPoint(relativeCheckpoints[checkpointIndex]);
		}
		else
		{
			//self.velocity *= 0;
			//self.angularVelocity *= 0;
			Destroy(self.gameObject);
		}
	}

	void NextCheckPoint( Vector2 checkpoint)
	{
		Vector2 direction = checkpoint - self.position;

		direction.Normalize();

		float rotateAmmount = Vector3.Cross(direction, self.transform.up).z;

		self.angularVelocity = -rotateAmmount * rotateSpeed;

		self.velocity = self.transform.up * speed;

		if (Vector2.Distance(checkpoint, self.position) < distance)
		{
			checkpointIndex++;
			Debug.Log(checkpointIndex + " time: " + Time.time + " Position: "+self.position);
		}
	}

}
