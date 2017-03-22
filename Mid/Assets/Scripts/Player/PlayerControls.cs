﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	PlayerControls: Handles player state in N-gon										*/
/*			Functions:																	*/
/*					public:																*/
/*																						*/
/*					private:															*/
/*						void Start ()													*/
/*						void Move (float dx, float dy)									*/
/*						void ReversePolarity ()											*/
/*						IEnumerator NormalizePolarity()									*/
/*						void OnCollisionEnter2D(Collision2D other)						*/
/*						void Update () 													*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class PlayerControls : MonoBehaviour 
{
	//	Public Const Variables
	public const string PLAYER1 = "Player1";		//	Tag for player 1
	public const string PLAYER2 = "Player2";		//	Tag for player 2

	//	Public Variabels
	public float moveSpeed = 10.0f;					//	Default movement speed of character
	public KeyCode reversePolarity = KeyCode.Space;	//	Key for shifting polarity
	public KeyCode upKey = KeyCode.W;				//	Key for moving up
	public KeyCode downKey = KeyCode.S;				//	Key for moving down
	public KeyCode leftKey = KeyCode.A;				//	Key for moving left
	public KeyCode rightKey = KeyCode.D;			//	Key for moving right

	//	Private Variables
	private float x = 0;							//	Float to store from Input.GetAxis
	private float y = 0;							//	Float to store from Input.GetAxis
	private SpriteRenderer _MyRenderer;				//	Reference to this SpriteRenderer
	private Rigidbody2D _Rigidbody2D;				//	Reference to player's rigidbody
	private TrailRenderer _MyTrail;					//	Reference to TrailRenderer
	private Particle _ThisParticle;					//	Reference to player's Particle component

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Start () 
	{
		_Rigidbody2D = GetComponent<Rigidbody2D> ();
		_ThisParticle = GetComponent<Particle>();
		_MyRenderer = GetComponent<SpriteRenderer>();
		_MyTrail = GetComponent<TrailRenderer>();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Move: moves the player in a direction x and/or y based on axis input				*/
	/*		param:																			*/
	/*			float dx - horizontal axis input											*/
	/*			float dy - vertical axis input												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Move (KeyCode key,float dx, float dy)
	{
		if(Input.GetKey(key))
		{
			_Rigidbody2D.velocity = new Vector2(dx * moveSpeed, dy * moveSpeed);
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	ReversePolarity: Pushes the moving particles away									*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void ReversePolarity ()
	{
		StartCoroutine(NormalizePolarity());
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	NormalizePolarity: Returns Polarity to normal 										*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	IEnumerator NormalizePolarity()
	{
		_ThisParticle.charge = _ThisParticle.charge * -1.0f;
		_MyRenderer.color = _ThisParticle.nodeDischarged;
		_MyTrail.startColor = _ThisParticle.nodeDischarged;
		_MyTrail.endColor = _ThisParticle.nodeDischarged;
		yield return new WaitForSeconds(0.25f);
		_MyRenderer.color = _ThisParticle.nodeCharged;
		_MyTrail.startColor = _ThisParticle.nodeCharged;
		_MyTrail.endColor = _ThisParticle.nodeCharged;
		_ThisParticle.charge = _ThisParticle.charge * -1.0f;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnCollisionEnter2D: Function runs when collider enter collides with anything		*/
	/*			param:																		*/
	/*				Collision2D other - the thing we collided with							*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnCollisionEnter2D(Collision2D other)
	{
		//	TODO: Can add effetcs based on collision
		if (other.gameObject.tag == "MovingParticle")
		{
			
		}
	}
	
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Update () 
	{
		switch(gameObject.tag)
		{
			case PLAYER1:
					x = Input.GetAxis ("Horizontal");
					y = Input.GetAxis ("Vertical");
				break;
			case PLAYER2:
					x = Input.GetAxis ("Player2_Horizontal");
					y = Input.GetAxis ("Player2_Vertical");
				break;
		}	

		Move (upKey, x, y);
		Move (downKey, x, y);
		Move (leftKey, x, y);
		Move (rightKey, x ,y);	

		if (Input.GetKeyDown(reversePolarity))
		{
			ReversePolarity ();
		}
	}
}