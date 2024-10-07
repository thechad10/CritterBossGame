using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "Scriptable Objects/LevelProgression")]
public class LevelProgression : ScriptableObject
{
    public bool coyoteDefeated;
    public bool birdDefeated;
    public bool bearDefeated;

    public void BearDefeated()
    {
        bearDefeated = true;
    }
    public void BirdDefeated()
    {
        birdDefeated = true;
    }
    public void CoyoteDefeated()
    {
        coyoteDefeated = true;
    }
}
