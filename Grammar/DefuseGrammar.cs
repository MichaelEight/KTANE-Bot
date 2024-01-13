using System.IO;
using System.Security.Policy;
using System.Speech.Recognition;

namespace KTANE_Bot
{
    public static class DefuseGrammar
    {
        public static Grammar StandardDefuseGrammar => _StandardDefuseGrammar();
        public static Grammar BombCheckGrammar => _BombCheckGrammar();
        public static Grammar ButtonGrammar => _ButtonGrammar();
        public static Grammar MemoryGrammar => _MemoryGrammar();
        public static Grammar WiresGrammar => _WiresGrammar();
        public static Grammar KnobGrammar => _KnobGrammar();
        public static Grammar MazeGrammar => _MazeGrammar();
        public static Grammar MorseGrammar => _MorseGrammar();
        public static Grammar SymbolsGrammar => _SymbolsGrammar();
        public static Grammar PasswordGrammar => _PasswordGrammar();
        public static Grammar SequenceGrammar => _SequenceGrammar();
        public static Grammar SimonSaysGrammar => _SimonSaysGrammar();
        public static Grammar ComplicatedGrammar => _ComplicatedGrammar();
        public static Grammar WhoIsOnFirstGrammar => _WhoIsOnFirstGrammar();
        

        //METHODS TO RETRIEVE GRAMMARS.
        private static Grammar _StandardDefuseGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Defuse.txt")))) {Name = "Standard Defuse Grammar"};
        }
        
        //bomb checking grammar
        private static Grammar _BombCheckGrammar()
        {
            var batteryChoices = new Choices(new string[] {"none", "0", "1", "2", "more than 2", "3", "4", "5", "6"});
            var countBatteries = new GrammarBuilder(batteryChoices);
            var trueOrFalse = new Choices(new string[] {"yes", "no", "true", "false"});
            var oddEven = new Choices(new string[] {"odd", "even"});

            // Add "done" command
            var done = new GrammarBuilder("done");
            var escape = new GrammarBuilder("ESCAPE MODULE");

            //batteries
            var battery = new GrammarBuilder("Batteries");
            battery.Append(countBatteries);
                
            //parallel port
            var parallelPort = new GrammarBuilder("Port");
            parallelPort.Append(trueOrFalse);
                
            //frk, interpreted as the word "freak".
            var frk = new GrammarBuilder("Freak");
            frk.Append(trueOrFalse);
                
            //car, interpreted as the word "car".
            var car = new GrammarBuilder("Car");
            car.Append(trueOrFalse);
                
            //vowel in serial number
            var vowel = new GrammarBuilder("Vowel");
            vowel.Append(trueOrFalse);
                
            //last number of serial number
            var digit = new GrammarBuilder("Digit");
            digit.Append(oddEven);

            var allChoices = new Choices(new GrammarBuilder[] { battery, vowel, parallelPort, digit, frk, car, done, escape });
            return new Grammar(allChoices) {Name = "Bomb Check Grammar"};
        }

        private static Grammar _ButtonGrammar()
        {
            var labelChoices = new Choices(new string[] { "detonate", "hold", "press", "abort", "stripe"});
            var red = new GrammarBuilder("Red");
            var yellow = new GrammarBuilder("Yellow");
            var blue = new GrammarBuilder("Blue");
            var white = new GrammarBuilder("White");
            
            red.Append(labelChoices);
            yellow.Append(labelChoices);
            blue.Append(labelChoices);
            white.Append(labelChoices);

            // Add "done" command
            var done = new GrammarBuilder("ESCAPE MODULE");

            var allChoices = new Choices(new GrammarBuilder[] {red, yellow, blue, white, done});
            return new Grammar(allChoices) {Name = "Button Grammar"};
        }

        private static Grammar _MemoryGrammar()
        {
            var grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Numbers", 1, 7);
            grammarBuilder.AppendDictation(category: "numbers");
            grammarBuilder.Append(new Choices("ESCAPE MODULE")); // Add "done" command

            return new Grammar(grammarBuilder) { Name = "Memory Grammar" };
        }

        private static Grammar _WiresGrammar()
        {
            return new Grammar(new GrammarBuilder(new Choices(new string[] {"yellow", "blue", "black", "white", "red", "done", "ESCAPE MODULE", "wrong"}))) {Name = "Wires Grammar"};
        }

        private static Grammar _KnobGrammar()
        {
            var choices = new Choices(File.ReadAllLines(@"Knob.txt"));
            choices.Add("ESCAPE MODULE"); // Add "done" command
            return new Grammar(new GrammarBuilder(choices)) { Name = "Knob Grammar" };
        }

        private static Grammar _MazeGrammar()
        {
            var numbers = new Choices("1", "2", "3", "4", "5", "6");

            var one = new GrammarBuilder("1");
            var two = new GrammarBuilder("2");
            var three = new GrammarBuilder("3");
            var four = new GrammarBuilder("4");
            var five = new GrammarBuilder("5");
            var six = new GrammarBuilder("6");
            var done = new GrammarBuilder("ESCAPE MODULE");
            
            one.Append(numbers);
            two.Append(numbers);
            three.Append(numbers);
            four.Append(numbers);
            five.Append(numbers);
            six.Append(numbers);
            
            
            var allChoices = new Choices(new GrammarBuilder[] { one, two, three, four, five, six, done });
            
            return new Grammar(allChoices) {Name = "Maze Grammar"};
        }

        private static Grammar _MorseGrammar()
        {
            var digits = new Choices("0", "1");
            var digitSequence = new GrammarBuilder();
            digitSequence.Append(digits, 1, 10); // Allow sequences of 1 to 10 digits

            var escapeCommand = new GrammarBuilder("ESCAPE MODULE");

            var choices = new Choices(new GrammarBuilder[] { digitSequence, escapeCommand });
            return new Grammar(choices) { Name = "Morse Grammar" };
        }


        private static Grammar _SymbolsGrammar()
        {
            var choices = new Choices(File.ReadAllLines(@"Symbols.txt"));
            choices.Add("ESCAPE MODULE"); // Add "done" command
            return new Grammar(new GrammarBuilder(choices)) { Name = "Symbols Grammar" };
        }


        private static Grammar _PasswordGrammar()
        {
            var choices = new Choices(File.ReadAllLines(@"Password.txt"));
            choices.Add("ESCAPE MODULE"); // Add "done" command
            return new Grammar(new GrammarBuilder(choices)) { Name = "Password Grammar" };
        }


        private static Grammar _SequenceGrammar()
        {
            var letters = new Choices("alpha", "bravo", "charlie");

            var red = new GrammarBuilder("red");
            var blue = new GrammarBuilder("blue");
            var black = new GrammarBuilder("black");
            var done = new GrammarBuilder("done");
            
            red.Append(letters);
            blue.Append(letters);
            black.Append(letters);

            var allChoices = new Choices(new GrammarBuilder[] { red, black, blue, done });

            return new Grammar(allChoices) {Name = "Sequence Grammar"};
        }

        private static Grammar _ComplicatedGrammar()
        {
            var properties = new Choices("nothing", "star", "light", "star and light", "light and star");

            var red = new GrammarBuilder("red");
            var redWhite = new GrammarBuilder("red and white");
            var whiteRed = new GrammarBuilder("white and red");
            
            var blue = new GrammarBuilder("blue");
            var blueWhite = new GrammarBuilder("blue and white");
            var whiteBlue = new GrammarBuilder("white and blue");
            
            var blueRed = new GrammarBuilder("blue and red");
            var redBlue = new GrammarBuilder("red and blue");

            var white = new GrammarBuilder("white");

            var done = new GrammarBuilder("done");
            
            red.Append(properties);
            redWhite.Append(properties);
            whiteRed.Append(properties);
            blue.Append(properties);
            blueWhite.Append(properties);
            whiteBlue.Append(properties);
            blueRed.Append(properties);
            redBlue.Append(properties);
            white.Append(properties);

            var allChoices = new Choices(new GrammarBuilder[] { red, redWhite, whiteRed, blue, blueWhite, whiteBlue, blueRed, redBlue, white, done });
            
            return new Grammar(allChoices) {Name = "Complicated Grammar"};
        }

        private static Grammar _SimonSaysGrammar()
        {
            var strikeChoices = new Choices("0", "1", "2");
            var strikeBuilder = new GrammarBuilder("strikes");
            
            strikeBuilder.Append(strikeChoices);

            var blue = new GrammarBuilder("blue");
            var green = new GrammarBuilder("green");
            var red = new GrammarBuilder("red");
            var yellow = new GrammarBuilder("yellow");
            var done = new GrammarBuilder("done");

            var allChoices = new Choices(new GrammarBuilder[] { strikeBuilder, blue, red, green, yellow, done });
            return new Grammar(allChoices) {Name = "Simon Says Grammar"};
        }

        private static Grammar _WhoIsOnFirstGrammar()
        {
            var choices = new Choices(File.ReadAllLines(@"WhoIsOnFirst.txt"));
            choices.Add("ESCAPE MODULE"); // Add "done" command
            return new Grammar(new GrammarBuilder(choices)) { Name = "Who's On First Grammar" };
        }

    }
}