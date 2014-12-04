// Copyright 2014 Nicholas Costello <NicholasJCostello@gmail.com>

using System;
using Assets.Scripts.Enums;

public class SnakeInputEventArgs : EventArgs
{
    public SnakeInputEventArgs(Direction direction)
    {
        Direction = direction;
    }

    public Direction Direction { get; set; }
}