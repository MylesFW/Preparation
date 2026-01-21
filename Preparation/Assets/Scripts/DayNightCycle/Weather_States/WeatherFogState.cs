using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class WeatherFogState : State
{
    public ObjectContext self;

    // Called once on State enter
    public override void Enter()
    {
        float _tempMod = Random.Range(-4.0f, 4.0f);
        float _AirTarget = -10.0f - _tempMod;
        self.weatherController.targetAmbientAirTemp = _AirTarget;
    }
    // Called once per frame until the State is switched
    public override void Run()
    {

    }
    // Called once on State switch
    public override void Exit()
    {

    }

    //Constructor
    public WeatherFogState(FiniteStateMachine _fsm, ObjectContext _context, string _name = "WeatherFogState", int _priority = 0, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
