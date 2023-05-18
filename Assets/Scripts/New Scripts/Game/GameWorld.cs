using Models;
public class GameWorld
{
    private Player _player;
    public Player Player => _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
