using redd096;

public class PlayerState : State
{
    protected Player player;

    public PlayerState(StateMachine stateMachine) : base(stateMachine)
    {
        //get player reference
        player = stateMachine as Player;
    }
}
