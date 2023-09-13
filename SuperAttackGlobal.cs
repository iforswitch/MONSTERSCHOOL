using Godot;
using System;

public partial class SuperAttackGlobal : Node
{
    //Global Signal for super attack
    [Signal] public delegate void SuperAttackFinishedEventHandler();
}
