// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngineInternal;

namespace Assets.Scripts
{
    public interface ISegment
    {
        void ConstructSegment(ISegment followSegment);
        void MoveSegment(Direction direction);
        Vector3 GetPosition();
        Direction SegmentDirection { get; }

    }
}