using UnityEngine;

public class WinGA : GameAction
{
    public enum option { coyoteWin, birdWin, bearwin }
    public option winOption;
    public LevelProgression levelProgression;

    public override void Action()
    {
        switch (winOption) {
            case option.coyoteWin:
                levelProgression.CoyoteDefeated();
                break;
            case option.birdWin: 
                levelProgression.BirdDefeated();
                break;
            case option.bearwin: 
                levelProgression.BearDefeated();
                break;
            default: 
                break;

        }
    }
}
