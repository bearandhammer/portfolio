using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;          //To gain access to the SpeechSynthesizer Class (add a reference to System.Speech)
using System.Diagnostics;               //For the debugging

namespace SpeechSynthTestApp
{
    /// <summary>
    /// Program Class Definition.
    /// </summary>
    class Program
    {
        #region Private Static Data Fields

        //Static SpeechSynthesizer object (initialised fully with the PrepareSpeechSynthObject method)
        private static SpeechSynthesizer speechSynth;

        #endregion Private Static Data Fields

        #region Constructor

        /// <summary>
        /// SpeechSynthTestApp Program Class Constructor.
        /// </summary>
        /// <param name="args">Incoming arguments for this Console Application from external sources (if supplied).</param>
        static void Main(string[] args)
        {
            //Prepare the static SpeechSynthesizer object ready to receive input from the user (for speech) and then start the main application loop
            PrepareSpeechSynthObject();
            RunApplication();
        }

        #endregion Constructor

        #region Private Static Methods

        /// <summary>
        /// Private static method that prepares the private SpeechSynthesizer object
        /// ready for the user to start interacting with.
        /// </summary>
        private static void PrepareSpeechSynthObject()
        {
            //Initialise the speechSynth object
            speechSynth = new SpeechSynthesizer();

            //For testing purposes tie up some of the SpeechSynthesizer classes event handlers (in this instance to retrieve debug information)
            speechSynth.SpeakStarted += ((sender, e) => 
            {
                SpeechSynthesizer synth = sender as SpeechSynthesizer;

                if (synth != null)
                {
                    Debug.WriteLine("Speech Synth object has started talking with the voice '{0}' at volume '{1}'.",
                        synth.Voice.Description, synth.Volume);
                }               
            });
            speechSynth.SpeakProgress += ((sender, e) =>
            {
                SpeechSynthesizer synth = sender as SpeechSynthesizer;

                if (synth != null)
                {
                    Debug.WriteLine("Speech Progressing. Saying '{0}'; Char Count '{1}'; Audio Pos '{2}'; Voice '{3}'; Volume '{4}'.",
                        e.Text, e.CharacterCount, e.AudioPosition, synth.Voice.Description, synth.Volume);
                }  
            });
            speechSynth.SpeakCompleted += ((sender, e) =>
            {
                SpeechSynthesizer synth = sender as SpeechSynthesizer;

                if (synth != null)
                {
                    Debug.WriteLine("Speech completed. Voice '{0}'; Volume '{1}'.", synth.Voice.Description, synth.Volume);
                }
            });
        }

        /// <summary>
        /// Private static method that represents the main application loop (which
        /// consists of gathering user input and calling a method to make a
        /// SpeechSynthesizer object do it's thing!).
        /// </summary>
        private static void RunApplication()
        {
            //Just keep a' loopin'
            while (true)
            {
                //Gather user input
                Console.Write("Type something for the SpeechSynthesizer to say then press enter ('E' to exit): ");
                string userInput = Console.ReadLine();
                
                //If a user types 'E' (case insensitive) then exit the loop and the application
                if (userInput.Equals("E", StringComparison.InvariantCultureIgnoreCase))
                {
                    //As we ar calling SpeakAsync we have to cancel any method calls still being called/to be called asynchronously to prevent exceptions
                    speechSynth.SpeakAsyncCancelAll();
                    break; //Exit
                }

                //SPEAK!
                Speak(userInput);
            }
        }

        /// <summary>
        /// Take in the users input and talk talk talk!!!
        /// </summary>
        /// <param name="userInput">The users input from the Console.</param>
        private static void Speak(string userInput)
        {
            /*
                Speak based on the users input (handle null, empty or whitespace input). We could create a more complicated PromptBuilder object here but 
                passing a string serves as a suitable, basic example
            */
            speechSynth.SpeakAsync(string.IsNullOrWhiteSpace(userInput) ? "You need to type something next time." : userInput);
        }

        #endregion Private Static Methods
    }
}
