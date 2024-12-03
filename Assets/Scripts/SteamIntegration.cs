using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    private void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(3324230);
            PrintYourName();
        }
        catch (System.Exception e)
        {
            //Something is not right
            Debug.Log(e);
        }
    }
    private void PrintYourName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }
    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }
    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
    public void InvokeCreditsAchievement()
    {
        var ach = new Steamworks.Data.Achievement("credits_visited");
        ach.Trigger(true);
    }
}
