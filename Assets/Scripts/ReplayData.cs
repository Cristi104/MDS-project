using UnityEngine;
using System.Collections.Generic;
public class ReplayData
{
    public List<Vector2> Positions;
    public List<string> Animations;
    public List<int> FacingDirections;

    public ReplayData()
    {
        this.Positions = new List<Vector2>();
        this.Animations = new List<string>();
        this.FacingDirections = new List<int>();
    }

    public ReplayData(List<Vector2> positions, List<string> animations, List<int> facingDirections)
    {
        this.Positions = positions;
        this.Animations = animations;
        this.FacingDirections = facingDirections;
    }

    public ReplayData DeepCopy()
    {
        return new ReplayData(new List<Vector2>(Positions), new List<string>(Animations), new List<int>(FacingDirections));
    }
}
