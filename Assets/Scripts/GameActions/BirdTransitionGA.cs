using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BirdTransition : GameAction
{
    [SerializeField]
    private GameObject pheonix;
    [SerializeField]
    private GameObject peacockDeath;
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
    [SerializeField]
    private Vignette hotWub;
    public override void Action()
    {
        pheonix.SetActive(true);
        peacockDeath.SetActive(false);
        peacockScene.SetActive(false);
        pheonixScene.SetActive(true);
        player.transform.position = PlayerMoveToPoint.position;
        ui.RefillBar();
    }
}
