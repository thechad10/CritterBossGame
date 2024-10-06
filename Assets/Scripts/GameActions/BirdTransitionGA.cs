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
    public override void Action()
    {
        peacock.SetActive(false);
        pheonix.SetActive(true);
        peacockScene.SetActive(false);
        pheonixScene.SetActive(true);
        ui.RefillBar();
    }
}
