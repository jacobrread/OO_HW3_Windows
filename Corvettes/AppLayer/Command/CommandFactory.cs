using AppLayer.DrawingComponents;

namespace AppLayer.Command
{
    /// <summary>
    /// CommandFactory
    /// 
    /// Creates standard commands, but can be specialized to create custom commands.  This class is the base
    /// class in a factory method pattern.
    /// </summary>
    public class CommandFactory
    {
        public Drawing TargetDrawing { get; set; }
        public CorvetteFactory CorvetteFactory { get; set; }
        public Invoker Invoker { get; set; }

        /// <summary>
        /// CreateAndDo -- a factory method for creating commands and queuing theme for execution
        /// 
        /// This method can be overridden to generate different or custom commands.
        /// </summary>
        /// <param name="commandType">type of command to Create:
        ///             New
        ///             Add
        ///             Remove
        ///             Select
        ///             Deselect
        ///             Load
        ///             Save</param>
        /// <param name="commandParameters">An array of optional parametesr whose sementics depedent on the command type
        ///     For new, no additional parameters needed
        ///     For add, 
        ///         [0]: Type       reference type for assembly containing the corvette type resource
        ///         [1]: string     corvette type -- a fully qualified resource name
        ///         [2]: Point      center location for the corvette, defaut = top left corner
        ///         [3]: float      scale factor</param>
        ///     For remove, no additional parameters needed
        ///     For select,
        ///         [0]: Point      Location at which a corvette could be selected
        ///     For deselect, no additional parameters needed
        ///     For load,
        ///         [0]: string     filename of file to load from  
        ///     For save,
        ///         [0]: string     filename of file to save to  
        /// <returns></returns>
        public virtual void CreateAndDo(string commandType, params object[] commandParameters)
        {
            if (string.IsNullOrWhiteSpace(commandType)) return;

            if (TargetDrawing == null) return;

            Command command=null;
            switch (commandType.Trim().ToUpper())
            {
                case "NEW":
                    command = new NewCommand();
                    break;
                case "ADD":
                    command = new AddCommand(commandParameters);
                    break;
                case "REMOVE":
                    command = new RemoveSelectedCommand();
                    break;
                case "SELECT":
                    command = new SelectCommand(commandParameters);
                    break;
                case "DESELECT":
                    command = new DeselectAllCommand();
                    break;
                case "LOAD":
                    command = new LoadCommand(commandParameters);
                    break;
                case "SAVE":
                    command = new SaveCommand(commandParameters);
                    break;
                case "SCREENSHOT":
                    command = new ScreenshotCommand(commandParameters);
                    break;
            }

            if (command == null) return;

            command.TargetDrawing = TargetDrawing;
            Invoker.EnqueueCommandForExecution(command);
        }
    }
}

