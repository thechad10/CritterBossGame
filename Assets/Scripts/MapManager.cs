using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	private PlayerInput pInput;
	
	public List<SpriteRenderer> pins;
	public List<BoxCollider2D> unpins;
    public LevelProgression lProgress;
    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void OnEnable()
    {
        if (lProgress.coyoteDefeated)
        {
            pins[0].color = Color.yellow;
            unpins[0].enabled = false;
        }
        else if (lProgress.birdDefeated)
        {
            pins[1].color = Color.yellow;
            unpins[1].enabled = false;
        }
        else if(lProgress.bearDefeated)
        {
            pins[2].color = Color.yellow;
            unpins[2].enabled = false;
        }

    }
    private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		Character.Initialise(this, StartPin);
		pInput = new PlayerInput();
		pInput.Enable();
	}
	
    private void OnDisable()
    {
        pInput.Disable();
    }
    /// <summary>
    /// This runs once a frame
    /// </summary>
    private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
	}

	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (pInput.Player.Move.ReadValue<Vector2>().x > 0)
		{
            Character.TrySetDirection(Direction.Right);
        }
        if (pInput.Player.Move.ReadValue<Vector2>().x < 0)
        {
            Character.TrySetDirection(Direction.Left);
        }
        if (pInput.Player.Move.ReadValue<Vector2>().y > 0)
        {
            Character.TrySetDirection(Direction.Up);
        }
        if (pInput.Player.Move.ReadValue<Vector2>().y < 0)
        {
            Character.TrySetDirection(Direction.Down);
        }
    }

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
}
