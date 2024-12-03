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
                var co_complete = new Steamworks.Data.Achievement("coyote_slain");
                co_complete.Trigger(true);
                break;
            case option.birdWin: 
                levelProgression.BirdDefeated();
                var bi_complete = new Steamworks.Data.Achievement("bird_slain");
                bi_complete.Trigger(true);
                break;
            case option.bearwin: 
                levelProgression.BearDefeated();
                var be_complete = new Steamworks.Data.Achievement("bear_slain");
                be_complete.Trigger(true);
                break;
            default: 
                break;

        }
    }
}
