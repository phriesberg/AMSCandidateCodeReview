using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplianceDemo
{
    public class ComplianceMapper
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Please enter your map data:");

            string consoleLine = Console.ReadLine();

            List<List<String>> allSets = new List<List<String>>();
            List<String> thisSet = new List<String>();

            int numRowsInSet = 0;
            int incomingRowNum = 0;

            try
            {
                // As user enters data, load it into collections
                while (consoleLine != "0 0")
                {
                    string firstChar = consoleLine.Substring(0, 1);

                    // if first char of line is not a number this will be a data row (or a data entry error)
                    if (firstChar == "*" || firstChar == ".")
                    {
                        // If this is the first row of a new set the first char should be a number. This check catches entries that have more rows than stated.
                        if (incomingRowNum == 0) { throw new Exception(); }

                        thisSet.Add(consoleLine);
                    }
                    // If the first entry on a line is not a data character (* or .), it should be a number that indicates the number of rows. If it is, get that number, if not, throw error.
                    else if (!int.TryParse(consoleLine.Substring(0, consoleLine.IndexOf(char.Parse(" "))), out numRowsInSet))
                    {
                        throw new Exception();
                    }

                    //once the number of iterations matches the stated number of rows, add the set to the list of all sets and reset counter
                    if (incomingRowNum == numRowsInSet)
                    {
                        allSets.Add(thisSet);
                        thisSet = new List<String>();
                        incomingRowNum = 0;
                    }
                    else
                    {
                        incomingRowNum++;
                    }

                    //wait for next line, or read next line from file
                    consoleLine = Console.ReadLine();
                }

                if (allSets.Count > 0)
                {
                    int thisResultSet = 0;

                    Console.WriteLine();

                    // for each entered set
                    foreach (List<string> currentSet in allSets)
                    {
                        thisResultSet++;
                        // write set header
                        Console.WriteLine(String.Format("Set #{0}:", thisResultSet));

                        int thisRow = 0;

                        foreach (string currentRow in currentSet)
                        {
                            // using string builder for the result row as it could be appended to as many as 100 times
                            StringBuilder resultRow = new StringBuilder("");
                            int currentRowPosition = 0;

                            foreach (char thisMapPoint in currentRow)
                            {
                                // storing this here because the variable is used many times possibly, so we only want to char.Parse once.
                                char hitChar = char.Parse("*");

                                if (thisMapPoint == char.Parse("."))
                                {
                                    // if this character is at the beginning or end of the line only search 2 positions per line
                                    int searchLength = currentRowPosition == currentRow.Length - 1 || currentRowPosition == 0 ? 2 : 3;

                                    //if this character is at the beginning of the line, start search at that character instead of 1 previous
                                    int searchStartPosition = currentRowPosition == 0 ? 0 : currentRowPosition - 1;

                                    // get search string for current line - not using stringbuilder for search string. It only gets appended a max of 2 times and probably isn't worth the overhead of the stringbuilder
                                    String searchString = currentRow.Substring(searchStartPosition, searchLength);

                                    // get search string for previous line (if not the first line in set)
                                    if (!thisRow.Equals(0))
                                    {
                                        searchString += currentSet[thisRow - 1].Substring(searchStartPosition, searchLength);
                                    }

                                    // get search string for the next line (if not the last line in set)
                                    if (!thisRow.Equals(currentSet.Count() - 1))
                                    {
                                        searchString += currentSet[thisRow + 1].Substring(searchStartPosition, searchLength);
                                    }

                                    // count up the hits and write the output row
                                    resultRow.Append(searchString.Count(thisChar => thisChar == hitChar));
                                }
                                else
                                {
                                    // ignore asterisks, and throw error if not asterisk
                                    if (thisMapPoint.Equals(hitChar))
                                    {
                                        resultRow.Append(thisMapPoint);
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }

                                currentRowPosition++;

                            }

                            Console.WriteLine(resultRow);

                            thisRow++;

                        }

                        Console.WriteLine();

                    }

                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Invalid data entry detected.");
            }
        }
    }
}


