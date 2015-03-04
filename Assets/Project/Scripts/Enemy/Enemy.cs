﻿using UnityEngine;
using System.Collections;
using Excelsion.GameManagers;

namespace Excelsion.Enemies
{
	[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
	public class Enemy : MonoBehaviour 
	{
		private bool DO_DEBUG = true;
		private bool faceHeading = true;
		private float damping = 0.4f;
		float speed = 5.0f;
		public float Speed{ get{ return this.speed; } set{ this.speed = Mathf.Max(0f, value); } }
		private float defaultSpeed; //We use this to remove status effects.

		private Vector3 targetPosition;
		private Vector3 targetInRangePosition;
		private Vector3 targetHeading;
		private Vector3 currentHeading;
		public virtual void Start () 
		{
			defaultSpeed = speed;
			GetComponent<Rigidbody>().isKinematic = true;
			currentHeading = transform.forward;
		}

		public virtual void FixedUpdate()
		{
			DoMovement();


		}
		public virtual void DoMovement()
		{
			targetPosition = DefenseController.Get().enemyObjective.transform.position;
			targetInRangePosition = VectorExtras.OffsetPosInPointDirection( new Vector3(targetPosition.x, transform.position.y, targetPosition.z), 
			                        transform.position, 8.0f );
			if( Vector3.Distance( transform.position, targetInRangePosition ) <= 1.0f )
			{
				if( faceHeading )
					transform.LookAt( new Vector3(targetPosition.x, transform.position.y, targetPosition.z) );
				return;
			}
			else
			{

				/*DEBUG*/ if( DO_DEBUG ) Debug.DrawLine(transform.position, targetInRangePosition, Color.yellow, 0.25f);

				targetHeading = VectorExtras.Direction( transform.position, targetInRangePosition );

				/*DEBUG*/ if( DO_DEBUG ) Debug.DrawRay(transform.position, targetHeading, Color.blue, 0.25f);

				currentHeading = Vector3.Lerp( currentHeading, targetHeading, Time.deltaTime * damping );

				/*DEBUG*/ if( DO_DEBUG ) Debug.DrawRay(transform.position, currentHeading, Color.red, 0.25f);

				transform.position += ( currentHeading * speed * Time.deltaTime );
				
				RaycastHit data;
				if( Physics.Raycast(transform.position, Vector3.down, out data, 250.0f ) )
				{
					transform.position = new Vector3( transform.position.x, data.point.y + 1.0f, transform.position.z );
					currentHeading.y = 0.0f;
				}
				if( faceHeading )
					transform.LookAt( transform.position + currentHeading );
			}
		}

		public virtual void Update () 
		{

		}












	}
}