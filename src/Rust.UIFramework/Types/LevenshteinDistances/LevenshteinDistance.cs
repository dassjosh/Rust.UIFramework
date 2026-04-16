using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Extensions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Types;

public class LevenshteinDistance(string start, string end)
{
    private int FrameCount => _frames.Count;
    private readonly List<string> _frames = Generate(start, end);
    
    public string GetFrame(int index) => _frames[index];
    public string GetFrame(float value)
    {
        float frameIndex = Mathf.Clamp01(value) * (FrameCount - 1);
        int frameIndexRounded = Mathf.RoundToInt(frameIndex);
        return GetFrame(frameIndexRounded);
    }

    private static List<string> Generate(string start, string end)
    {
        start ??= string.Empty;
        end ??= string.Empty;
        
        List<CodePoint> sourceCodePoints = StringToCodePoints(start);
        List<CodePoint> endCodePoints = StringToCodePoints(end);
        List<LevenshteinDistanceEdit> edits = GetEditPath(sourceCodePoints, endCodePoints);
        List<string> frames = GenerateFrames(start, end, sourceCodePoints, edits);
        frames.RemoveSequentialDuplicates();
        return frames;
    }

    private static List<string> GenerateFrames(string start, string end, List<CodePoint> sourceCodePoints, List<LevenshteinDistanceEdit> edits)
    {
        List<string> frames = [start];
        List<CodePoint> currentCodePoints = new(sourceCodePoints);

        int sourceIndex = 0;

        for (int i = 0; i < edits.Count; i++)
        {
            LevenshteinDistanceEdit nextEdit = edits[i];
            switch (nextEdit.Operation)
            {
                case LevenshteinDistanceOperationType.Deletion:
                    currentCodePoints.RemoveAt(sourceIndex);
                    break;
                case LevenshteinDistanceOperationType.Insertion:
                    currentCodePoints.Insert(sourceIndex, nextEdit.TargetCodePoint);
                    sourceIndex++;
                    break;
                case LevenshteinDistanceOperationType.Substitution:
                    currentCodePoints[sourceIndex] = nextEdit.TargetCodePoint;
                    sourceIndex++;
                    break;
                case LevenshteinDistanceOperationType.Match:
                    sourceIndex++;
                    break;
            }
            
            frames.Add(string.Join("", currentCodePoints.Select(cp => cp.ToString())));
        }
        
        if (frames.Count == 0)
        {
            frames.Add(end);
        }

        return frames;
    }

    private static List<CodePoint> StringToCodePoints(string str)
    {
        List<CodePoint> codePoints = [];
        for (int i = 0; i < str.Length; i++)
        {
            if (char.IsHighSurrogate(str[i]) && i + 1 < str.Length && char.IsLowSurrogate(str[i + 1]))
            {
                codePoints.Add(new CodePoint(str[i], str[i + 1]));
                i++; // Skip the low surrogate
                continue;
            }

            codePoints.Add(new CodePoint(str[i]));
        }
        
        return codePoints;
    }

    private static List<LevenshteinDistanceEdit> GetEditPath(List<CodePoint> sourceCodePoints, List<CodePoint> endCodePoints)
    {
        int startLength = sourceCodePoints.Count;
        int endLength = endCodePoints.Count;
        int[,] matrix = new int[startLength + 1, endLength + 1];

        // Initialize the matrix
        for (int i = 0; i <= startLength; i++) matrix[i, 0] = i;
        for (int j = 0; j <= endLength; j++) matrix[0, j] = j;

        // Fill the matrix
        for (int i = 1; i <= startLength; i++)
        {
            for (int j = 1; j <= endLength; j++)
            {
                int cost = sourceCodePoints[i - 1].Equals(endCodePoints[j - 1]) ? 0 : 1;
                matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
            }
        }

        // Backtrack to find the sequence of edits
        List<LevenshteinDistanceEdit> edits = [];
        int sourceIndex = startLength;
        int endIndex = endLength;

        while (sourceIndex > 0 || endIndex > 0)
        {
            if (sourceIndex > 0 && endIndex > 0 && sourceCodePoints[sourceIndex - 1].Equals(endCodePoints[endIndex - 1]))
            {
                edits.Add(new LevenshteinDistanceEdit(endCodePoints[endIndex - 1], LevenshteinDistanceOperationType.Match));
                sourceIndex--;
                endIndex--;
            }
            else if (sourceIndex > 0 && endIndex > 0 && matrix[sourceIndex, endIndex] == matrix[sourceIndex - 1, endIndex - 1] + 1)
            {
                edits.Add(new LevenshteinDistanceEdit(endCodePoints[endIndex - 1], LevenshteinDistanceOperationType.Substitution));
                sourceIndex--;
                endIndex--;
            }
            else if (endIndex > 0 && matrix[sourceIndex, endIndex] == matrix[sourceIndex, endIndex - 1] + 1)
            {
                edits.Add(new LevenshteinDistanceEdit(endCodePoints[endIndex - 1], LevenshteinDistanceOperationType.Insertion));
                endIndex--;
            }
            else if (sourceIndex > 0 && matrix[sourceIndex, endIndex] == matrix[sourceIndex - 1, endIndex] + 1)
            {
                edits.Add(new LevenshteinDistanceEdit(default, LevenshteinDistanceOperationType.Deletion));
                sourceIndex--;
            }
        }
        
        edits.Reverse();
        return edits;
    }
    
    private readonly record struct LevenshteinDistanceEdit(CodePoint TargetCodePoint, LevenshteinDistanceOperationType Operation);

    private enum LevenshteinDistanceOperationType : byte
    {
        Match,
        Substitution,
        Insertion,
        Deletion
    }
}