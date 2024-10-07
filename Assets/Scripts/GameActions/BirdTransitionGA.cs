using UnityEngine;

public class BirdTransition : GameAction
{
    [SerializeField]
    private GameObject pheonix;
    [SerializeField]
    private GameObject peacock;
    [SerializeField]
    private GameObject pheonixScene;
    [SerializeField]
    private GameObject peacockScene;
    [SerializeField]
    private UIsetup ui;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform PlayerMoveToPoint;
    public override void Action()
    {
        peacock.SetActive(false);
        pheonix.SetActive(true);
        peacockScene.SetActive(false);
        pheonixScene.SetActive(true);
        player.transform.position = PlayerMoveToPoint.position;
        ui.RefillBar();
    }
}
