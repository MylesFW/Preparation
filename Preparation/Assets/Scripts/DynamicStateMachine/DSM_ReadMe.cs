using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSM_ReadMe { }

/*
DYNAMIC STATE MACHINE README
Brennan Sands 12/16/25

States own unique gameobject behavior, but don't get to decide on future state decisions. The FSM handles 
when and what states get selected and switched to. Dividing the two features decouples GameObjects Dynamic 
actions.

//state.cs - The base class inherited during the creation of each unique state subclass (Substates);
This does not neet to be edited or attached anywhere.

//TemplateState.cs - An empty substate, waiting to be filled with unique behavior. Create a new script, 
then copy/paste the contents of the template state and get to coding. Note: I'ts best to store states
in folders grouped by their role (ObjectContext) eg. Player States, Enemy States, Game States etc...

//FiniteStateMachine.cs - The brains behind the state switching. States get queued and the FSM decides
which how the switches occur. Additionally the FSM creates acts as a delegate for the data manipulated 
by each state. Note: This script component must be attached to a GameObject to function.

//ObjectContext.cs - Contains explicit script components to be prefilled (Usually by the GameObjects
controller script). This script may be left alone.

//Test.cs - Shows how to declare and instantiate the FSM and ObjectContext before it can be used.
Correct instantiation is nessasary for the FSM and states to function properly. Typically, the GameObject's
Controller script would instantiate.

*/