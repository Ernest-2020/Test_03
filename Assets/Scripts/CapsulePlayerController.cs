public class CapsulePlayerController : IExecute
{
    private readonly BasePlayer _basePlayer;

    public CapsulePlayerController(BasePlayer basePlayer)
    {
        _basePlayer = basePlayer;
    }

    public void Execute()
    {
        _basePlayer.CmdLunge();
        _basePlayer.CmdCheckDistanceSkill();
    }
}
