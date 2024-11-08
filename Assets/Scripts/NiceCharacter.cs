public class NiceCharacter : Character
{
    protected override void GetDialog(GameState gs)
    {
        MyDialog ??= Dialog.NiceDialog();
    }
}