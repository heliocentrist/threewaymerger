using System.Collections.Generic;
using System.Linq;

namespace Merger
{
    public class Version
    {
        private List<string> lines;

        private int index;

        public bool Ended
        {
            get
            {
                return index >= lines.Count;
            }
        }

        public string CurrentLine
        {
            get
            {
                return lines[index];
            }
        }

        public Version(List<string> lines)
        {
            this.lines = lines;
        }

        public void SkipLine()
        {
            ++index;
        }

        public void SkipLines(int untilIndex)
        {
            index = untilIndex;
        }

        public void AcceptLine(List<string> result)
        {
            result.Add(CurrentLine);
            ++index;
        }

        public void AcceptLines(List<string> result, int untilIndex)
        {
            while (!Ended && index < untilIndex)
            {
                AcceptLine(result);
            }
        }

        public bool FastForward(List<string> result, string untilString)
        {
            int untilIndex = index + lines.Skip(index).ToList().IndexOf(untilString);

            if (untilIndex < index)
            {
                return false;
            }
            else
            {
                AcceptLines(result, untilIndex);
                return true;
            }
        }

        public void AcceptUntilEnd(List<string> result)
        {
            AcceptLines(result, lines.Count);
        }

        public int FindAnchor(string anchor)
        {
            return index + lines.Skip(index).ToList().IndexOf(anchor);
        }

        public bool IndexFound(int anchorIndex)
        {
            return anchorIndex >= index;
        }

        public List<string> Slice(int untilIndex)
        {
            return lines.Skip(index).Take(untilIndex - index).ToList();
        }
    }
}
