namespace ProiectMPDA.Command
{
    public class Invoker
    {
        public required ICommand Command {  get; set; }

        public void ExecuteCommand()
        {
            Command.Execute();
        }
    }
}
