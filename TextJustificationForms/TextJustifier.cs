using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextJustificationForms
{
    public static class TextJustifier
    {
        /// <summary>
        /// Justifies text using purely string and StringBuilder
        /// </summary>
        /// <param name="text">The text to justify</param>
        /// <param name="width">The desired width of each line of text</param>
        /// <param name="maxLastLineSpacePad">The maximum amount of space to pad words on the last line of the text; purely for beauty purposes</param>
        /// <returns></returns>
        public static string JustifyString(string text, int width, int maxLastLineSpacePad)
        {
            StringBuilder outputBuilder = new StringBuilder();
            List<string> lines = new List<string> { text };
            for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
            {
                var words = lines[lineIndex].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                while (lines[lineIndex].Length >= width)
                {
                    if (words[words.Count - 1].Contains('\n') && words[words.Count - 1][0] != '\n') //splits word at newline character, preserving the newline character in the rightmost word
                    {
                        string newLinedWord = words[words.Count - 1].Substring(words[words.Count - 1].LastIndexOf('\n'));
                        words[words.Count - 1] = words[words.Count - 1].Remove(words[words.Count - 1].LastIndexOf(newLinedWord));
                        words.Add(newLinedWord);
                        continue;
                    }
                    else if (lineIndex == lines.Count - 1)
                    {
                        lines.Add(words[words.Count - 1]);
                    }
                    else if (lines[lineIndex + 1][0] == '\n')
                    {
                        lines.Insert(lineIndex + 1, words[words.Count - 1]);
                    }
                    else
                    {
                        lines[lineIndex + 1] = lines[lineIndex + 1].Insert(0, $"{words[words.Count - 1]} ");
                    }
                    lines[lineIndex] = lines[lineIndex].Remove(lines[lineIndex].LastIndexOf(words[words.Count - 1]));
                    words.RemoveAt(words.Count - 1);
                }

                int spacesToAdd = width - lines[lineIndex].Replace(" ", "").Length; //# of spaces that need to be inserted between words
                for (int wordIndex = 0; wordIndex < words.Count - 1; wordIndex++)
                {
                    int spacesAfterWord = (int)Math.Ceiling((double)spacesToAdd / (words.Count - wordIndex - 1)); //how many spaces needed after the current word
                    spacesAfterWord = spacesAfterWord > maxLastLineSpacePad && (lineIndex == lines.Count - 1 || lines[lineIndex + 1][0] == '\n') ? maxLastLineSpacePad : spacesAfterWord; //removes gross over-spacing on the last line before a new line
                    outputBuilder.Append(words[wordIndex].PadRight(words[wordIndex].Length + spacesAfterWord)); //adds spaces after word and appends to string builder
                    spacesToAdd -= spacesAfterWord;
                }
                outputBuilder.Append($"{words[words.Count - 1]}\n"); //appends final word and newline character
            }

            return outputBuilder.ToString();
        }

        /// <summary>
        /// Justifies text using a fancy Rope implmentation
        /// </summary>
        /// <param name="text">The text to justify</param>
        /// <param name="width">The desired width of each line of text</param>
        /// <param name="maxLastLineSpacePad">The maximum amount of space to pad words on the last line of the text; purely for beauty purposes</param>
        /// <returns></returns>
        public static string JustifyRope(string text, int width, int maxLastLineSpacePad)
        {
            var rope = new Rope();
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList(); //gets every word in the text
            rope.Append(text.Replace(" ", ""));

            var lineLengths = new List<int>() { 0 };
            var wordsInLine = new List<int>() { 0 };
            for(int i = 0; i < words.Count;i++)
            {
                if(words[i].Contains("\n") && words[i][0] != '\n')
                {
                    words.Insert(i + 1, words[i].Substring(words[i].IndexOf('\n')));
                    words[i] = words[i].Substring(0, words[i].IndexOf('\n'));
                }
                if(lineLengths[lineLengths.Count - 1] + wordsInLine[wordsInLine.Count - 1] + words[i].Length > width || words[i][0] == '\n') //if the current line (and expected spaces) is too large when the next word is added
                { 
                    lineLengths.Add(0);
                    wordsInLine.Add(0);
                }
                lineLengths[lineLengths.Count - 1] += words[i][0] == '\n' ? words[i].Length - 1 : words[i].Length;
                wordsInLine[wordsInLine.Count - 1]++;
            }

            int currentLineIndex = 0; //index of the current line
            int currentTextIndex = 0; //current index in the text, helps to determine where to insert spaces
            int currentWordIndexOnLine = 0;
            bool isEndOfParagraph = false; //determines whether the next line begins with a user inserted new line; used to enforce maxLastLineSpacePad
            for(int i = 0; i < words.Count; i++)
            {
                if (currentWordIndexOnLine == 0 && rope.Report(currentTextIndex + lineLengths[currentLineIndex], 1) == "\n") //sets isEndOfParagraph
                {
                    isEndOfParagraph = true;
                }

                currentTextIndex += words[i].Length;

                if (currentWordIndexOnLine == wordsInLine[currentLineIndex] - 1) //resets to a new line
                {
                    currentLineIndex++;
                    currentWordIndexOnLine = 0;
                    if(!isEndOfParagraph && i != words.Count - 1)
                    {
                        rope.Insert("\n", currentTextIndex);
                        currentTextIndex++;
                    }
                    isEndOfParagraph = false;
                    continue;
                }

                int spacesAfterWord = (int)Math.Ceiling((double)(width - lineLengths[currentLineIndex]) / (wordsInLine[currentLineIndex] - currentWordIndexOnLine - 1)); //amount of spaces after current word
                spacesAfterWord = spacesAfterWord > maxLastLineSpacePad && (currentLineIndex == lineLengths.Count - 1 || isEndOfParagraph) ? maxLastLineSpacePad : spacesAfterWord; //removes gross over-spacing on the last line before a new line
                rope.Insert("".PadRight(spacesAfterWord), currentTextIndex); //inserts an amount of spaces after the current word based on spacesAfterWord

                lineLengths[currentLineIndex] += spacesAfterWord;
                currentTextIndex += spacesAfterWord;
                currentWordIndexOnLine++;
            }

            return rope.ReportAll();
        }
    }
}
