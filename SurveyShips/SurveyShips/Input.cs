using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SurveyShips
{

    //Now we implement IReadableSqlFile through the ReadOnlySqlFile class that reads only the text from read-only files.
    public interface ISurveyFormattedInput
    {
        SurveyInput<char> ParseSurveyInput(string inputValue);
    }

    public class SpaceDelimitedCharInstructionGridCommandPairInput : ISurveyFormattedInput
    {
        private static char InputDelimiter = ' ';
        public readonly Coordinate BottomLeftCoordinate = new Coordinate(0, 0);

        public SurveyInput<char> ParseSurveyInput(string inputValue)
        {
            int readableLineCount = 0;
            SurveyInput<char> input = new SurveyInput<char>();
            input.Commands = new List<ShipCommand<char>>();

            using (StringReader reader = new StringReader(inputValue))
            {
                string line;

                //loop through each line of the survey input, parsing each as we go
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length <= 1)
                    {
                        //if not readable line, skip execution
                        continue;
                    }

                    //attempt to parse each line
                    try
                    {
                        //split string if possible
                        string[] items = line.Split(InputDelimiter);
                        ShipCommand<char> command = new ShipCommand<char>();

                        //The first line of input is the top right (or north east) coordinates of the rectangular world
                        if (readableLineCount == 0)
                        {
                            input.TopRightCoordinate = new Coordinate(items);
                            readableLineCount++;
                            continue;
                        }

                        //read alternate lines
                        if (readableLineCount % 2 == 0)
                        {
                            //even lines represent ship instructions, so apply to last added item
                            input.Commands.Last().Instructions = line.ToCharArray();
                        }
                        else
                        {
                            //odd lines representship positions
                            command = new ShipCommand<char>();
                            command.Position = new ShipPosition(items);
                            input.Commands.Add(command);
                        }
                    }
                    //catch any parsing exceptions within the file
                    catch (FormatException formatException)
                    {
                        //TODO in the absence of a logger, simply output to console
                        Console.WriteLine(string.Format("Could not parse input : {0}", formatException.Message));
                        
                        //return null if failed parsing
                        input = null;
                        break;
                    }

                    readableLineCount++;
                }
            }

            return input;
        }
    }
}
