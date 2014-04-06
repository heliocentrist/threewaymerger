using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Merger
{
    public class ThreeWayMerger
    {
        /// <summary>
        /// Performs the three way merge operation upon lists of strings.
        /// </summary>
        /// <param name="originalList">The original list.</param>
        /// <param name="leftList">First version.</param>
        /// <param name="rightList">Second version.</param>
        /// <returns>The merged list.</returns>
        public static List<string> Merge(IEnumerable<string> originalList, IEnumerable<string> leftList, IEnumerable<string> rightList)
        {
            List<string> result = new List<string>();

            var original = new Version(originalList.ToList());
            var left = new Version(leftList.ToList());
            var right = new Version(rightList.ToList());

            while (!left.Ended || !right.Ended)
            {
                if (left.Ended || right.Ended)
                {
                    Version whatNotEnded = left.Ended ? right : left;

                    if (original.Ended)
                    {
                        whatNotEnded.AcceptUntilEnd(result);
                        break;
                    }
                    else
                    {
                        ResolveSingleVersion(result, original, whatNotEnded);
                        break;
                    }
                }
                else if (original.Ended)
                {
                    ResolveTwoWayLine(left, right, result);
                    continue;
                }

                if (CompareStrings(original.CurrentLine, left.CurrentLine) && CompareStrings(original.CurrentLine, right.CurrentLine))
                {
                    left.AcceptLine(result);
                    original.SkipLine();
                    right.SkipLine();
                }
                else if (!CompareStrings(original.CurrentLine, left.CurrentLine) || !CompareStrings(original.CurrentLine, right.CurrentLine))
                {
                    Version whichIsMaybeEqual = original.CurrentLine != left.CurrentLine ? right : left;
                    Version whichIsNotEqual = original.CurrentLine != left.CurrentLine ? left : right;

                    if (CompareStrings(original.CurrentLine,whichIsMaybeEqual.CurrentLine))
                    {
                        if (!whichIsNotEqual.FastForward(result, original.CurrentLine))
                        {
                            original.SkipLine();
                            whichIsMaybeEqual.SkipLine();
                        }
                    }
                    else if (CompareStrings(left.CurrentLine, right.CurrentLine))
                    {
                        left.AcceptLine(result);
                        right.SkipLine();
                    }
                    else
                    {
                        ResolveThreeWayConflict(left, right, original, result);
                    }
                }
            }

            return result;
        }

        private static bool CompareStrings(string first, string second)
        {
            return first.Trim() == second.Trim();
        }

        private static void ResolveSingleVersion(List<string> result, Version original, Version version)
        {
            while (!version.Ended || !version.Ended)
            {
                if (CompareStrings(original.CurrentLine, version.CurrentLine))
                {
                    original.SkipLine();
                    version.SkipLine();
                }
                else
                {
                    version.AcceptLine(result);
                }
            }
        }

        private static void ResolveThreeWayConflict(Version left, Version right, Version original, List<string> result)
        {
            int leftIndex = left.FindAnchor(original.CurrentLine);
            int rightIndex = right.FindAnchor(original.CurrentLine);

            if (!left.IndexFound(leftIndex) && !right.IndexFound(rightIndex))
            {
                result.Add(CheckForConflict(left.CurrentLine, right.CurrentLine));
                left.SkipLine();
                right.SkipLine();
                original.SkipLine();
            }
            else if (!left.IndexFound(leftIndex))
            {
                right.AcceptLines(result, rightIndex);
            }
            else if (!right.IndexFound(rightIndex))
            {
                left.AcceptLines(result, leftIndex);
            }
            else
            {
                result.AddRange(ResolveTwoWayList(left.Slice(leftIndex), right.Slice(rightIndex)));

                left.SkipLines(leftIndex);
                right.SkipLines(rightIndex);
            }
        }

        private static void ResolveTwoWayLine(Version left, Version right, List<string> result)
        {
            result.Add(CheckForConflict(left.CurrentLine, right.CurrentLine));
            left.SkipLine();
            right.SkipLine();
            return;
        }

        private static List<string> ResolveTwoWayList(List<string> left, List<string> right)
        {
            if (left.Count > right.Count)
            {
                return left.Zip(right, (x, y) => CheckForConflict(x, y)).Concat(left.Skip(right.Count)).ToList();
            }
            else
            {
                return right.Zip(left, (x, y) => CheckForConflict(y, x)).Concat(right.Skip(left.Count)).ToList();
            }
        }

        private static string CheckForConflict(string left, string right)
        {
            if (CompareStrings(left, right))
            {
                return left;
            }
            return "conflict: " + left + " " + right;
        }
    }
}
