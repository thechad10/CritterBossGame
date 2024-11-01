using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
	Up,
	Down,
	Left,
	Right
}

public class Pin : MonoBehaviour
{
	[Header("Options")] //
	public bool IsAutomatic;
	public bool HideIcon;
	public string SceneToLoad;
	
	[Header("Pins")] //
	public Pin UpPin;
	public Pin DownPin;
	public Pin LeftPin;
	public Pin RightPin;

	private Dictionary<Direction, Pin> _pinDirections;
	[SerializeField] private GameObject _levelStartScreen;
	[SerializeField] private Character _playerCharacter;
	[SerializeField] PlayerInput pInput;
	[SerializeField] private bool _enabled;
	
	/// <summary>
	/// Use this for initialisation
	/// </summary>
	private void Start()
	{
		// Load the directions into a dictionary for easy access
		_pinDirections = new Dictionary<Direction, Pin>
		{
			{ Direction.Up, UpPin },
			{ Direction.Down, DownPin },
			{ Direction.Left, LeftPin },
			{ Direction.Right, RightPin }
		};
		
		// Hide the icon if needed
		if (HideIcon)
		{
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
	private void OnEnable()
	{
		pInput = new PlayerInput();
		pInput.Enable();
    }
	private void OnTriggerEnter2D(Collider2D collision)
    {
		_enabled = true;
		Debug.Log("calling");
		
    }
	private void OnTriggerExit2D(Collider2D collision)
	{
		_enabled = false;
	}
	/// <summary>
	/// Get the pin in a selected direction
	/// Using a switch statement rather than linq so this can run in the editor
	/// </summary>
	/// <param name="direction"></param>
	/// <returns></returns>
	public Pin GetPinInDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
				return UpPin;
			case Direction.Down:
				return DownPin;
			case Direction.Left:
				return LeftPin;
			case Direction.Right:
				return RightPin;
			default:
				throw new ArgumentOutOfRangeException("direction", direction, null);
		}
	}

	
	/// <summary>
	/// This gets the first pin thats not the one passed 
	/// </summary>
	/// <param name="pin"></param>
	/// <returns></returns>
	public Pin GetNextPin(Pin pin)
	{
		return _pinDirections.FirstOrDefault(x => x.Value != null && x.Value != pin).Value;
	}
	
	
	/// <summary>
	/// Draw lines between connected pins
	/// </summary>
	private void OnDrawGizmos()
	{
		if(UpPin != null) DrawLine(UpPin);
		if(RightPin != null) DrawLine(RightPin);
		if(DownPin != null) DrawLine(DownPin);
		if(LeftPin != null) DrawLine(LeftPin);
	}


	/// <summary>
	/// Draw one pin line
	/// </summary>
	/// <param name="pin"></param>
	protected void DrawLine(Pin pin)
	{   
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pin.transform.position);
	}
	private void Update()
	{
		if (_enabled && pInput.Player.Interact.IsPressed())
		{ 
			_levelStartScreen.SetActive(true);
			_playerCharacter.Speed = 0f;
		}
    }
}
