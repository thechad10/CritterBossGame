using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public Text SelectedLevelText;

	private PlayerInput pInput;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		Character.Initialise(this, StartPin);
		pInput = new PlayerInput();
		pInput.Enable();
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
		/*if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			Character.TrySetDirection(Direction.Up);
		}
		else if(Input.GetKeyUp(KeyCode.DownArrow))
		{
			Character.TrySetDirection(Direction.Down);
		}
		else if(Input.GetKeyUp(KeyCode.LeftArrow))
		{
			Character.TrySetDirection(Direction.Left);
		}
		else if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			Character.TrySetDirection(Direction.Right);
		}*/

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
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format("Current Level: {0}", Character.CurrentPin.SceneToLoad);
	}
}
