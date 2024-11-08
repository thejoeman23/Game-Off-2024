public class AngryCharacter : Character
{
    protected override void GetDialog(GameState gs)
    {
        MyDialog ??= Dialog.AngryDialog();
    }
}