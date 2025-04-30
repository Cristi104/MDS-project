using UnityEngine;
using System.Collections.Generic;
public class ReplayData
{
    public List<Vector2> positions;
    public List<string> animations;
    public List<int> facingDirections;

    public ReplayData(){
        this.positions = new List<Vector2>();
        this.animations = new List<string>();
        this.facingDirections = new List<int>();
    }
    public ReplayData(List<Vector2> positions, List<string> animations, List<int> facingDirections){
        this.positions = positions;
        this.animations = animations;
        this.facingDirections = facingDirections;
    }

    public ReplayData DeepCopy(){
        return new ReplayData(new List<Vector2>(positions), new List<string>(animations), new List<int>(facingDirections));
    }
}
