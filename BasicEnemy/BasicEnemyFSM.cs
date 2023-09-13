using Godot;
using System;
using System.Collections.Generic;

//Inherits FSM class
public partial class BasicEnemyFSM : FSM
{
    //Export variable to show the current state
    [Export] public Label StateText = new();

    //Dictionary variable for all states of the player
    public Dictionary<string, State> PlayerStates = new();

    //State variable for the current state
    public State CurrentState = new();

    //Variable for health
    public float Health;

    //Variable for death
    public bool Dead;

    //Variable for global damage indicator
    public DamageIndicator DamageIndicatorGlobal = new();

    /// <summary>
	/// Called when the node enters the scene tree for the first time.
	/// </summary>
    public override void _Ready()
    {
        //Set the health
        Health = MaxHealth;

        //Set the death
        Dead = false;

        //Set the damage indicator global
        DamageIndicatorGlobal = GetNode<DamageIndicator>("/root/DamageIndicator");

        //Add each child to the PlayerStates dictionary
        foreach (State child in GetChildren())
        {
            //Connect each child signal to the FSM function
            child.StateTransition += OnStateTransition; 
            PlayerStates.Add(key: child.Name, value: child);
        }

        //Enter the initial state
        if (InitialState != null)
        {
            InitialState.StateEnter();
            CurrentState = InitialState;
        }
    }

    /// <summary>
    /// Called every physics frame. 'delta' is the elapsed time since the previous physics frame.
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        //Execute the state physics process
        if (CurrentState != null)
        {
            CurrentState.PhysicsProcess(delta);
        }
    }

    /// <summary>
    /// Called every frame. 'delta' is the elapsed time since the previous frame.
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        //Execute the state update process
        if (CurrentState != null)
        {
            CurrentState.UpdateProcess(delta);
            StateText.Text = CurrentState.Name;
        }
    }

    /// <summary>
    /// Handles key input events
    /// </summary>
    /// <param name="event"></param>
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        //Execute the state key input
        if (CurrentState != null)
        {
            CurrentState.UnhandledKeyInput(@event);
        }
    }

    /// <summary>
    /// Handle state transition called on by the signal
    /// </summary>
    /// <param name="emittingState"></param>
    /// <param name="targetState"></param>
    public void OnStateTransition(State emittingState, string targetState)
    {
        //The current state is not emitting the signal
        if (emittingState != CurrentState)
        {
            return;
        }

        //Get the state of the target state from the 
        State newState = PlayerStates.GetValueOrDefault(key: targetState);

        //State does not exit
        if (newState == null)
        {
            return;
        }

        //Exit the current state
        if (CurrentState != null)
        {
            CurrentState.StateExit();
        }

        //Enter the valid new state
        newState.StateEnter();

        //Set the current state to the new state
        CurrentState = newState;
    }

    /// <summary>
    /// Function for damaging the subject body
    /// </summary>
    /// <param name="damage"></param>
    public override void Hit(float damage)
    {
        if (Health > 0)
        {
            if (CurrentState == PlayerStates.GetValueOrDefault(key: "BasicEnemyHurt"))
            {
                CurrentState.StateEnter();
            }
            else
            {
                OnStateTransition(emittingState: CurrentState, targetState: "BasicEnemyHurt");
            }
        }
        else if (Health <= 0 && !Dead)
        {
            Dead = true;
            OnStateTransition(emittingState: CurrentState, targetState: "BasicEnemyDead");
        }

        DamageIndicatorGlobal.EmitSignal("DamageIndicate", GetParent(), -damage);
        Health -= damage;
    }
}
