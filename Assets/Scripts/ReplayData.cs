using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Stores and manages recorded player movement data for clone replay functionality.
/// </summary>
public class ReplayData
{
    public List<Vector2> positions;
    public List<string> animations;
    public List<int> facingDirections;

    /// <summary>
    /// Initializes a new empty ReplayData instance.
    /// </summary>
    public ReplayData()
    {
        this.positions = new List<Vector2>();
        this.animations = new List<string>();
        this.facingDirections = new List<int>();
    }

    /// <summary>
    /// Initializes a new ReplayData instance with predefined data.
    /// </summary>
    public ReplayData(List<Vector2> positions, List<string> animations, List<int> facingDirections)
    {
        this.positions = positions;
        this.animations = animations;
        this.facingDirections = facingDirections;
    }

    public ReplayData DeepCopy()
    {
        return new ReplayData(new List<Vector2>(positions), new List<string>(animations), new List<int>(facingDirections));
    }
}
