using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Windows.Forms;

namespace KTANE_Bot
{
    internal enum States
    {
        Checking,
        Waiting,
        Defusing
    }

    internal enum Solvers
    {
        Default,
        Check,
        Wires,
        Button,
        Symbols,
        Memory,
        Complicated,
        Simon,
        Sequence,
        WhoIsOnFirst,
        Morse,
        Knob,
        Password,
        Maze
    }
    
    public class KTANE_Speech
    {
        public SpeechRecognitionEngine RecognitionEngine { get; private set; }
        public bool Enabled { get; private set; }

        private KTANE_Module _defusingModule;
        private Bomb _bomb;
        private States _state;
        private Solvers _solvingModule;
        private bool BombParamsSkipped = false;
        private bool forceKeepBombCheck = false; 
        public Dictionary<string, int> BombProperties = new Dictionary<string, int>
        {
            {"Batteries", -1},
            {"Port",      -1},
            {"Freak",     -1},
            {"Car",       -1},
            {"Vowel",     -1},
            {"Digit",     -1}
        };

        private Dictionary<string, Solvers> _solvingGrammar = new Dictionary<string, Solvers>
        {
            { "Defuse wires", Solvers.Wires },
            { "Defuse button", Solvers.Button },
            { "Defuse symbols", Solvers.Symbols },
            { "Defuse memory", Solvers.Memory },
            { "Defuse complicated", Solvers.Complicated },
            { "Defuse simon", Solvers.Simon },
            { "Defuse sequence", Solvers.Sequence },
            { "Defuse who is on first", Solvers.WhoIsOnFirst },
            { "Defuse who first", Solvers.WhoIsOnFirst },
            { "Defuse morse", Solvers.Morse },
            { "Defuse knob", Solvers.Knob },
            { "Defuse password", Solvers.Password },
            { "Defuse maze", Solvers.Maze }
        };
        
        private SpeechSynthesizer _ktaneBot;
        public KTANE_Speech()
        {
            //speech synthesizer
            _ktaneBot = new SpeechSynthesizer();

            //switch to waiting state, so the bomb solver waits for a command.
            _state = States.Waiting;
            
            //initialize default
            RecognitionEngine = new SpeechRecognitionEngine();
            RecognitionEngine.SetInputToDefaultAudioDevice();
            RecognitionEngine.LoadGrammarAsync(DefuseGrammar.StandardDefuseGrammar);
            Disable();
        }

        public string AnalyzeSpeech(string command)
        {
            switch (_state)
            {
                case States.Checking:
                    var propertiesDictionary = new Dictionary<string, int>
                    {
                        { "yes", 1 },
                        { "true", 1 },
                        { "odd", 1 },
                        { "false", 0 },
                        { "no", 0 },
                        { "none", 0 },
                        { "even", 0 },
                        { "many", int.MaxValue },
                        { "more", int.MaxValue }
                    };

                    var commands = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var message = new StringBuilder();

                    for (int i = 0; i < commands.Length; i += 2)
                    {
                        if (i + 1 >= commands.Length)
                        {
                            if(commands[i] != "done" && commands[i] != "escape"){
                                message.Append("Incomplete command. ");
                                continue;
                            }
                        }

                        string property = commands[i];
                        string valueStr = "";
                        if (i + 1 < commands.Length)
                            valueStr = commands[i + 1];
                        int value = 0;

                        // Special handling for "battery" and "batteries"
                        if (property.Equals("Battery", StringComparison.OrdinalIgnoreCase) ||
                            property.Equals("Batteries", StringComparison.OrdinalIgnoreCase))
                        {
                            property = "Batteries"; // Use a consistent key for both
                        }

                        if (property != "done" && property != "escape")
                        {
                            if (propertiesDictionary.TryGetValue(valueStr, out value))
                            {
                                BombProperties[property] = value;
                            }
                            else if (int.TryParse(valueStr, out value))
                            {
                                BombProperties[property] = value;
                            }
                            else
                            {
                                message.Append($"Invalid value for {property}. ");
                                continue;
                            }
                        }

                        // Process the message for each property
                        switch (property)
                        {
                            case "ESCAPE":
                            case "done":
                                BombParamsSkipped = true;
                                forceKeepBombCheck = false;
                                _state = States.Waiting;
                                SwitchDefaultSpeechRecognizer(Solvers.Default);

                                Random rng = new Random();

                                foreach (var key in BombProperties.Keys.ToList())
                                {
                                    if (BombProperties[key] == -1)
                                    {
                                        // Randomize value for each missing property
                                        BombProperties[key] = rng.Next(0, 2); // Assuming 'rng' is a Random instance
                                    }
                                }

                                return message.ToString() + "Done";
                            case "Batteries":
                            case "Battery":
                                message.Append(value == int.MaxValue ? "Many " : $"{value} " + (value == 1 ? "battery " : "batteries "));
                                break;
                            case "Port":
                                message.Append(value == 0 ? "No port" : "Yes port ");
                                break;
                            case "Freak":
                                message.Append(value == 0 ? "No FRK " : "Lit FRK ");
                                break;
                            case "Car":
                                message.Append(value == 0 ? "No CAR " : "Lit CAR ");
                                break;
                            case "Vowel":
                                message.Append(value == 0 ? "No vowel " : "Yes Vowel ");
                                break;
                            case "Digit":
                                message.Append(value == 0 ? "Digit even " : "Digit odd ");
                                break;
                            default:
                                message.Append($"Unknown property: {property}. ");
                                break;
                        }
                    }

                    if (!BombProperties.ContainsValue(-1) && !forceKeepBombCheck)
                    {
                        _bomb = new Bomb(BombProperties["Batteries"],
                            BombProperties["Port"] == 1,
                            BombProperties["Freak"] == 1,
                            BombProperties["Car"] == 1,
                            BombProperties["Vowel"] == 1,
                            BombProperties["Digit"] == 0);
                        message.Append(" Done.");
                        _state = States.Waiting;
                        SwitchDefaultSpeechRecognizer(Solvers.Default);
                    }

                    return message.ToString();



                case States.Waiting:
                    if (command == "Random Bomb")
                    {
                        InitializeRandomBomb();
                        return "Generated random bomb";
                    }

                    if (command == "Skip Bomb")
                    {
                        return "Bomb check skipped";
                    }

                    //if the user hasn't yet initialized the bomb
                    if ((_bomb == null || BombProperties.ContainsValue(-1)) && !BombParamsSkipped)
                    {
                        if (command != "Bomb check")
                            return "Initialize bomb by saying \"Bomb check\"";

                        SwitchDefaultSpeechRecognizer(Solvers.Check);
                        _state = States.Checking;
                        return "Start checking";
                    }

                    if(command == "Bomb check")
                    {
                        forceKeepBombCheck = true;
                        SwitchDefaultSpeechRecognizer(Solvers.Check);
                        _state = States.Checking;
                        return "Start checking";
                    }

                    
                    //if the bomb is initialized.
                    try
                    {
                        SwitchDefaultSpeechRecognizer(_solvingGrammar[command]);
                        _solvingModule = _solvingGrammar[command];
                        _state = States.Defusing;

                        var additionalInfo = string.Empty;

                        switch (command)
                        {
                            case "Defuse memory":
                                additionalInfo = ", Stage 1";
                                break;
                            case "Defuse morse":
                                additionalInfo = ", first letter";
                                break;
                            case "Defuse who is on first":
                            case "Defuse who first":
                                additionalInfo = "first, what's displayed?";
                                break;
                            case "Defuse maze":
                                additionalInfo = "; green circle coordinates";
                                break;
                            case "Defuse password":
                                additionalInfo = "; Column 1";
                                break;
                        }
                        
                        //this is just a long way to say "return the module with first letter being capital."
                        return $"{char.ToUpper(command.Split(' ')[1][0]) + command.Split(' ')[1].Substring(1)} {additionalInfo}";
                    }
                    catch (KeyNotFoundException)
                    {
                        var rng = new Random();
                        
                        switch (command)
                        {
                            case "The bomb exploded":
                                var dismissingMessages = new[]
                                {
                                    "Aww :(",
                                    "It's your fault.",
                                    "Think faster.",
                                    "You're useless.",
                                    "We tried our best.",
                                    "Better luck next time.",
                                    "That was close!",
                                    "Oops, that wasn't supposed to happen.",
                                    "Back to square one.",
                                    "So close, yet so far.",
                                    "Not our day, huh?",
                                    "That was a tough one.",
                                    "Mistakes were made.",
                                    "Well, that escalated quickly.",
                                    "I guess it's just not your day."
                                };

                                if (Form1.ResetBomb)
                                    ResetBomb();
                                
                                return dismissingMessages[rng.Next(0, dismissingMessages.Length)];
                            
                            case "The bomb is defused":
                                var congratulatoryMessages = new[]
                                {
                                    "Good job!",
                                    "Nice!",
                                    "You did it!",
                                    "Yay!",
                                    "Woo-hoo!",
                                    "Congratulations!",
                                    "That's how it's done!",
                                    "You're a natural!",
                                    "Expertly handled!",
                                    "You're on fire!",
                                    "Absolutely brilliant!",
                                    "You nailed it!",
                                    "That was impressive!",
                                    "You're a hero!",
                                    "Masterfully done!"
                                };

                                if (Form1.ResetBomb)
                                    ResetBomb();
                                
                                return congratulatoryMessages[rng.Next(0, congratulatoryMessages.Length)];
                            
                            case "Stop":
                                StopSpeaking();
                                return "";
                            
                            default:
                                return "No.";
                        }
                    }
                case States.Defusing when _bomb != null:

                    switch (_solvingModule)
                    {
                        // WIRES SOLVER.
                        case Solvers.Wires:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Wires(_bomb);

                            var wires = (Wires)_defusingModule;
                            var colorCommands = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var color in colorCommands)
                            {
                                if (color.Equals("done", StringComparison.OrdinalIgnoreCase))
                                {
                                    var result = wires.Solve();
                                    if(result.StartsWith("You must give"))
                                    {
                                        return result;
                                    }
                                    else
                                    {
                                        SwitchToDefaultProperties();
                                        return "Done; " + result;
                                    }
                                }

                                if (color.Equals("wrong", StringComparison.OrdinalIgnoreCase))
                                {
                                    return wires.WireCount < 1 ? "No wires yet" : wires.DeletePreviousWire();
                                }

                                wires.AppendWire(color);
                                if (wires.WireCount >= 6) break; // Stop if 6 wires are reached
                            }

                            if (wires.WireCount < 6) return $"{command}; next";

                            SwitchToDefaultProperties();
                            return $"{command}; done. {wires.Solve()}";



                        //BUTTON SOLVER.
                        case Solvers.Button:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (new Button(_bomb, command.Split(' ')[0], command.Split(' ')[1]).Solve() ==
                                "Press and immediately release.")
                            {
                                SwitchToDefaultProperties();
                                return @"Press and immediately release.";
                            }
                            
                            if (_defusingModule == null)
                            {
                                _defusingModule = new Button(_bomb, command.Split(' ')[0], command.Split(' ')[1]);
                                return _defusingModule.Solve();
                            }

                            SwitchToDefaultProperties();
                            return Button.Solve(command.Split(' ')[0]);

                        // SYMBOLS SOLVER.
                        case Solvers.Symbols:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Symbols(_bomb);

                            var symbols = (Symbols)_defusingModule;
                            var allSymbols = File.ReadAllLines(@"Symbols.txt");

                            // Process the command for multi-word symbols
                            var words = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var symbolBuilder = new StringBuilder();
                            foreach (var word in words)
                            {
                                symbolBuilder.Append(word);
                                var currentSymbol = symbolBuilder.ToString();

                                if (allSymbols.Contains(currentSymbol))
                                {
                                    symbols.AppendSymbol(currentSymbol);
                                    symbolBuilder.Clear(); // Reset for the next symbol
                                }
                                else
                                {
                                    symbolBuilder.Append(" "); // Add space for multi-word symbols
                                }

                                if (symbols.InputLength == 4) break; // Stop if 4 symbols are reached
                            }

                            if (symbols.InputLength < 4) return $"{command}; next.";

                            SwitchToDefaultProperties();
                            return $"{command}; done. {symbols.Solve()}";



                        //MEMORY SOLVER.
                        case Solvers.Memory:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }
                            if (_defusingModule == null)
                                _defusingModule = new Memory(_bomb);

                            var memory = (Memory)_defusingModule;

                            if (!memory.SetNumbers(command.Split(' ')))
                                return @"Try again.";

                            if (memory.Stage != 5) return memory.Solve();
                            
                            SwitchToDefaultProperties();
                            return memory.Solve();

                        //COMPLEX WIRES SOLVER.
                        case Solvers.Complicated:
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }
                            
                            if (_defusingModule == null)
                                _defusingModule = new ComplexWires(_bomb);

                            var complexWire = (ComplexWires)_defusingModule;
                            complexWire.InterpretInput(command);
                            return complexWire.Solve(); 
                        
                        //SIMON SAYS SOLVER.
                        case Solvers.Simon:
                            if (_defusingModule == null)
                                _defusingModule = new Simon(_bomb);

                            var simon = (Simon)_defusingModule;

                            if (command.Contains("strikes"))
                            {
                                simon.SetStrikes(int.Parse(command.Split(' ')[1]));
                                return $"{command.Split(' ')[1]} " + (command.Split(' ')[1] == "1"? "strike" : "strikes");
                            }
                            
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }
                            
                            simon.AppendColor(command);
                            return simon.Solve();


                        // WIRE SEQUENCE SOLVER.
                        case Solvers.Sequence:
                            if (command == "done")
                            {
                                SwitchToDefaultProperties();
                                return "";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Sequence(_bomb);

                            var sequence = (Sequence)_defusingModule;
                            var sequenceCommands = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var responses = new List<string>(); // List to store responses for each pair

                            // Process each color-letter pair
                            for (int i = 0; i < sequenceCommands.Length; i += 2)
                            {
                                if (i + 1 >= sequenceCommands.Length)
                                {
                                    return "Incomplete sequence command. Please provide both color and letter.";
                                }
                                sequence.InitializeValues(sequenceCommands[i], sequenceCommands[i + 1]);
                                responses.Add(sequence.Solve());
                            }

                            // Join and return all responses
                            return string.Join(", ", responses);



                        //WHO'S ON FIRST.
                        case Solvers.WhoIsOnFirst:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new WhoIsOnFirst(_bomb);

                            var whoIsOnFirst = (WhoIsOnFirst)_defusingModule;

                            if (whoIsOnFirst.Display == string.Empty)
                            {
                                whoIsOnFirst.Display = command;
                                var ans = whoIsOnFirst.Solve();

                                if(ans.StartsWith("Error"))
                                {
                                    whoIsOnFirst.Display = string.Empty;
                                    return ans;
                                }
                                else
                                {
                                    return whoIsOnFirst.Solve();
                                }
                            }

                            whoIsOnFirst.Button = command;
                            SwitchToDefaultProperties();
                            return whoIsOnFirst.Solve();
                        
                        //MORSE CODE SOLVER.
                        case Solvers.Morse:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }
                            
                            if (_defusingModule == null)
                                _defusingModule = new Morse(_bomb);
                            
                            var morse = (Morse)_defusingModule;

                            if (command.Contains('.') || command.Contains('-'))
                            {
                                if (!morse.AddLetters(command.ToCharArray()))
                                    return @"Try again";
                            }
                            else if (!morse.AddLetters(command.Split(' ')))
                            {
                                return @"Try again.";
                            }
                            
                            if (morse.Solve().EndsWith("hertz."))
                                SwitchToDefaultProperties();

                            return morse.Solve();
                        
                        //KNOB SOLVER.
                        case Solvers.Knob:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Knob(_bomb);

                            var knob = (Knob)_defusingModule;
                            knob.Lights = command;
                            SwitchToDefaultProperties();
                            return knob.Solve();

                        // PASSWORD SOLVER.
                        case Solvers.Password:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Password(_bomb);

                            var password = (Password)_defusingModule;

                            // Handle multiple letter inputs
                            var letterCommands = command.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var letter in letterCommands)
                            {
                                var result = password.AssignLetters(letter);
                                if (result == -1)
                                {
                                    return "There can be no duplicate letters in the password. Try again.";
                                }
                                else if (result != 2)
                                {
                                    if (password.Solve().StartsWith("Try") || password.Solve().StartsWith("Something") || password.Solve().StartsWith("The"))
                                        SwitchToDefaultProperties();

                                    return password.Solve();
                                }
                            }

                            return $"{command}; next";


                        // MAZE SOLVER.
                        case Solvers.Maze:
                            if (command == "ESCAPE MODULE")
                            {
                                SwitchToDefaultProperties();
                                return "Cancelled";
                            }

                            if (_defusingModule == null)
                                _defusingModule = new Maze(_bomb);

                            var maze = (Maze)_defusingModule;
                            var coordinateCommands = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < coordinateCommands.Length; i += 2)
                            {
                                if (i + 1 >= coordinateCommands.Length)
                                {
                                    return "Incomplete coordinate pair.";
                                }

                                int x = int.Parse(coordinateCommands[i]);
                                int y = int.Parse(coordinateCommands[i + 1]);

                                // Assign circle coordinates
                                if (maze.TargetMaze == null)
                                {
                                    if (!maze.AssignCircle(x, y))
                                        return "Invalid circle coordinates. Try again.";
                                    continue;
                                }

                                // Set square location
                                if (maze.SquareLocation.X == -1 || maze.SquareLocation.Y == -1)
                                {
                                    maze.SetSquare(x, y);
                                    continue;
                                }

                                // Set triangle location
                                if (maze.TriangleLocation.X == -1 || maze.TriangleLocation.Y == -1)
                                {
                                    maze.SetTriangle(x, y);
                                    if (maze.TriangleLocation.X == maze.SquareLocation.X && maze.TriangleLocation.Y == maze.SquareLocation.Y)
                                    {
                                        maze.SetTriangle(0, 0);
                                        return "Square and triangle must be in different places; try again.";
                                    }
                                    break; // Break after setting triangle as it's the last step
                                }
                            }

                            // Check if all inputs are set
                            if (maze.TargetMaze == null || maze.SquareLocation.X == -1 || maze.TriangleLocation.X == -1)
                            {
                                if (maze.TargetMaze == null)
                                    return "where's green circle";
                                else if (maze.SquareLocation.X == -1)
                                    return "where's white square";
                                else if (maze.TriangleLocation.X == -1)
                                    return "where's triangle";
                            }

                            SwitchToDefaultProperties();
                            return maze.Solve();




                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
            }

            return "No";
        }

        private void SwitchDefaultSpeechRecognizer(Solvers solver)
        {
            RecognitionEngine.UnloadAllGrammars();

            var grammarDict = new Dictionary<Solvers, Grammar>
            {
                { Solvers.Default,      DefuseGrammar.StandardDefuseGrammar },
                { Solvers.Check,        DefuseGrammar.BombCheckGrammar },
                { Solvers.Wires,        DefuseGrammar.WiresGrammar },
                { Solvers.Button,       DefuseGrammar.ButtonGrammar },
                { Solvers.Symbols,      DefuseGrammar.SymbolsGrammar },
                { Solvers.Memory,       DefuseGrammar.MemoryGrammar },
                { Solvers.Complicated,  DefuseGrammar.ComplicatedGrammar },
                { Solvers.Simon,        DefuseGrammar.SimonSaysGrammar },
                { Solvers.Sequence,     DefuseGrammar.SequenceGrammar },
                { Solvers.WhoIsOnFirst, DefuseGrammar.WhoIsOnFirstGrammar },
                { Solvers.Morse,        DefuseGrammar.MorseGrammar },
                { Solvers.Knob,         DefuseGrammar.KnobGrammar },
                { Solvers.Password,     DefuseGrammar.PasswordGrammar },
                { Solvers.Maze,         DefuseGrammar.MazeGrammar }
            };

            RecognitionEngine.LoadGrammarAsync(grammarDict[solver]);
        }
        
        public void Enable()
        {
            Enabled = true;
            RecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        public void Disable()
        {
            Enabled = false;
            RecognitionEngine.RecognizeAsyncCancel();
        }

        public void StopSpeaking()
        {
            _ktaneBot.SpeakAsyncCancelAll();
        }

        public void Speak(string message)
        {
            _ktaneBot.SpeakAsync(message);
        }

        private void SwitchToDefaultProperties()
        {
            _defusingModule = null;
            SwitchDefaultSpeechRecognizer(Solvers.Default);
            _solvingModule = Solvers.Default;
            _state = States.Waiting;
        }

        public string[] GetAllVoices()
        {
            return (from voice in _ktaneBot.GetInstalledVoices() select voice.VoiceInfo.Name).ToArray();
        }

        public void ChangeVoice(string voiceName)
        {
            if (!GetAllVoices().Contains(voiceName))
                return;
            
            _ktaneBot.SelectVoice(voiceName);
        }

        public void ResetBomb()
        {
            _bomb = null;
            SwitchToDefaultProperties();

            foreach (var key in BombProperties.Keys.ToList())
                BombProperties[key] = -1;
        }

        public void InitializeRandomBomb()
        {
            var rng = new Random();
            
            foreach (var key in BombProperties.Keys.ToList())
            {
                if (key == "Batteries")
                {
                    BombProperties[key] = rng.Next(0, 7);
                    continue;
                }
                
                BombProperties[key] = rng.Next(0, 2);
            }
            
            _bomb = new Bomb(BombProperties["Batteries"], 
                BombProperties["Port"] == 1,
                BombProperties["Freak"] == 1, 
                BombProperties["Car"] == 1, 
                BombProperties["Vowel"] == 1,
                BombProperties["Digit"] == 0);
            _state = States.Waiting;
            SwitchDefaultSpeechRecognizer(Solvers.Default);
        }
    }
}