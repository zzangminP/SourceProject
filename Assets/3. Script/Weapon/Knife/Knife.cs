public class Knife : Weapon
{
    public Knife()
    {
        leftClick = new KnifeLeft();
        rightClick = new KnifeRight();
        reloadClick = new RClickNothing(); // Only return; -> Do Nothing
        wasdMove = new WASDMove();
    }
   
}
