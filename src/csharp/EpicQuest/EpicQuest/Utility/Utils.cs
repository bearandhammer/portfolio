using EpicQuest.GameEnums;
using System.Runtime.CompilerServices;
using System.Text;

namespace EpicQuest.Utility
{
    /// <summary>
    /// Static Utility Class for the Epic Quest game.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// String extension that consumes a string and formats it 
        /// as a short debug message (denoting when we enter or exit a method).
        /// </summary>
        /// <param name="methodName">The method name called as a string</param>
        /// <param name="msgType">The message type (have we entered or exited a method for example) as a enum. Defaulted to DebugMsgType.MethodEntered.</param>
        /// <returns></returns>
        public static string MethodDebugMsg(DebugMsgType msgType = DebugMsgType.MethodEntered, [CallerMemberName] string methodName = "")
        {
            //If a valid method name is not provided simply return an empty string
            if (string.IsNullOrEmpty(methodName))
            {
                return string.Empty;
            }

            //Use the DebugMsgType provided to return a formatted string (Have we entered/exited the given method name). Default = string.Empty
            switch (msgType)
            {
                case DebugMsgType.MethodEntered:
                    {
                        return string.Format("Entered the {0} method.", methodName);
                    }
                case DebugMsgType.MethodLeft:
                    {
                        return string.Format("Exited {0} method.", methodName);
                    }
                default:
                    {
                        return string.Empty;
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string WriteGameTitle()
        {
            StringBuilder titleSb = new StringBuilder();

            titleSb.AppendLine("==========================================================");
            titleSb.AppendLine("|  \"\"\"\"  \"\"\"\"  \"  \"\"\"\"    \"\"\"\"  \"  \"  \"\"\"\"  \"\"\"\"  \"\"\"\"\"  |");
            titleSb.AppendLine("|  \"     \"  \"  \"  \"  \"    \"  \"  \"  \"  \"     \"       \"    |");
            titleSb.AppendLine("|  \"\"\"\"  \"\"\"\"  \"  \"       \"\"\"\"  \"  \"  \"\"\"\"  \"\"\"\"    \"    |");
            titleSb.AppendLine("|  \"     \"     \"  \"        \"\"   \"  \"  \"        \"    \"    |");     
            titleSb.AppendLine("|  \"\"\"\"  \"     \"  \"\"\"\"      \"\"  \"\"\"\"  \"\"\"\"  \"\"\"\"    \"    |");
            titleSb.AppendLine("==========================================================");

            return titleSb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static GameAction InterpretUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return GameAction.Invalid;
            }

            switch (input.ToUpperInvariant().Trim())
            {
                case "EXIT":
                    {
                        return GameAction.ExitGame;
                    }
                case "BRW":
                    {
                        return GameAction.BrawlerChosen;
                    }
                case "CLR":
                    {
                        return GameAction.ClericChosen;
                    }
                case "MAG":
                    {
                        return GameAction.MageChosen;
                    }
                case "NEC":
                    {
                        return GameAction.NecromancerChosen;
                    }
                case "THF":
                    {
                        return GameAction.ThiefChosen;
                    }
                default:
                    {
                        return GameAction.Invalid;
                    }
            }
        }
    }
}