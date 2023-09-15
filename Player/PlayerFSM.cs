using Godot;
using System;
using System.Collections.Generic;

//Inherits the FSM class
public partial class PlayerFSM : FSM
{
    //Export variable to show the current state
    [Export] public Label StateText = new();

    //Export variable for skill timers
    [Export] public Timer RollTimer = new();
    [Export] public Timer SuperAttack1Timer = new();
    [Export] public Timer SuperAttack2Timer = new();

    //Dictionary variable for all states of the player
    public Dictionary<string, State> PlayerStates = new();

    //State variable for the current state
    public State CurrentState = new();

    //Variable for death
    public bool Dead;

    //Variable for global player variables
    public PlayerGlobals PlayerGlobalsVariable = new();

    //Array for skill timers
    public Timer[] SkillTimers = new Timer[3];

    PackedScene scene = GD.Load<PackedScene>("res://stat.tscn");

    /// <summary>
	/// Called when the node enters the scene tree for the first time.
	/// </summary>
    public override void _Ready()
	{
        //Set the global player variables
        PlayerGlobalsVariable = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        //Connect special attack 3 signal
        PlayerGlobalsVariable.Connect("SpecialAttack3", Callable.From(OnSpecialAttack3));

        //Conenct levell up signal
        PlayerGlobalsVariable.Connect("LevelUp", Callable.From(OnLevelUp));

        //Set the array
        SkillTimers[0] = RollTimer;
        SkillTimers[1] = SuperAttack1Timer;
        SkillTimers[2] = SuperAttack2Timer;

        //Set the deatg
        Dead = false;

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
        if (Input.IsActionPressed("TestStat")) 
        { 
            OnLevelUp(); 
        }

        //Execute the state physics process
        if (CurrentState != null)
        {
            CurrentState.PhysicsProcess(delta);

            if (PlayerGlobalsVariable.CurrentHealth <= 0 && !Dead)
            {
                Dead = true;
                OnStateTransition(emittingState: CurrentState, targetState: "PlayerDeath");
            }
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
        //Invulnerable during roll
        if (CurrentState != PlayerStates.GetValueOrDefault("PlayerRoll"))
        {
            if (CurrentState != PlayerStates.GetValueOrDefault("SuperAttack2"))
            {
                if (PlayerGlobalsVariable.CurrentHealth > 0)
                {
                    OnStateTransition(emittingState: CurrentState, targetState: "PlayerHurt");
                }
            }
            PlayerGlobalsVariable.CurrentHealth -= damage;
        }
    }


    /// <summary>
    /// Signal function for special attack 3
    /// </summary>
    public void OnSpecialAttack3()
    {
        if (PlayerGlobalsVariable.IsSpecialAttack3 == 1)
        {
            PlayerGlobalsVariable.JumpStrength *= 1.25f;
            PlayerGlobalsVariable.Damage *= 2;
            PlayerGlobalsVariable.Speed *= 2;
            PlayerGlobalsVariable.JumpStrength *= 1.25f;
            PlayerGlobalsVariable.RollSpeed *= 2f;
            for (int i = 0; i < SkillTimers.Length; i++)
            {
                SkillTimers[i].WaitTime *= PlayerGlobalsVariable.Cooldown;
            }
        }

        else 
        {
            PlayerGlobalsVariable.JumpStrength /= 1.25f;
            PlayerGlobalsVariable.Damage /= 2;
            PlayerGlobalsVariable.Speed /= 2;
            PlayerGlobalsVariable.JumpStrength /= 1.25f;
            PlayerGlobalsVariable.RollSpeed /= 2f;
            for (int i = 0; i < SkillTimers.Length; i++)
            {
                SkillTimers[i].WaitTime /= PlayerGlobalsVariable.Cooldown;
            }
        }
    }

    /// <summary>
    /// Timeout function for special attack 3
    /// </summary>
    public void OnSuperAttack3Timeout()
    {
        PlayerGlobalsVariable.CurrentHealth -= PlayerGlobalsVariable.MaxHealth*0.1f;
    }

    /// <summary>
    /// Function for level up signal
    /// </summary>
    public void OnLevelUp()
    {
        //Pause the entire game
        GetTree().Paused = true;

        stat statInstance = (stat)scene.Instantiate();

        //Add ui to player
        GetParent().AddChild(statInstance);
        CharacterBody2D parent = (CharacterBody2D)GetParent();

        //Set ui position to player position
        statInstance.GlobalPosition = parent.GlobalPosition;
        statInstance.StatFinished += OnStatFinished;
    }

    /// <summary>
    /// Function to execute once player finishes picking a stat to upgrade
    /// </summary>
    public void OnStatFinished()
    {
        //Unpase game
        GetTree().Paused = false;
    }
}
