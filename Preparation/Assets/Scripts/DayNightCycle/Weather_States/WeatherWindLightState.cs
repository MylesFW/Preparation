using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherWindLightState : State
{
    // Called once on State enter
    public override void Enter()
    {
        float _tempMod = Random.Range(-2.0f, 2.0f);
        float _AirTarget = -5.0f - _tempMod;
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
    public WeatherWindLightState(FiniteStateMachine _fsm, ObjectContext _context, string _name = "WeatherWindLightState", int _priority = 0, bool _locked = false, bool _forceOverride = false)
    {
        fsm = _fsm;
        name = _name;
        self = _context;
        priority = _priority;
        locked = _locked;
        forceOverride = _forceOverride;
    }
}
