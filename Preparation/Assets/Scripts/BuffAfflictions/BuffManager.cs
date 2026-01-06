using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public SimTime simTime;
    public PlayerController playerController;
    public bool enableLogging;

    public List<Buff> _buffs = new List<Buff>();
    private Buff buffIndex;
    private Buff recycledBuff;
    private Buff guiBuffIndex;
    
    private int heightDisplacement = 20;

    // Methods
    private void LogNewBuff(Buff _buff)
    {
        if (!enableLogging)
        {
            return;
        }

        if (_buff is BenefitBuff)
        {
            Debug.Log("You have received " + _buff.name);
        }
        else if (_buff is AfflictionBuff)
        {
            Debug.Log("You have contracted " + _buff.name);
        }
    }
    private void LogRemovedBuff(Buff _buff)
    {
        if (!enableLogging)
        {
            return;
        }

        if (_buff is BenefitBuff)
        {
            Debug.Log(_buff.name + " has worn off");
        }
        else if (_buff is AfflictionBuff)
        {
            Debug.Log(_buff.name + " has been cured");
        }
    }
    public bool IsBuffListEmpty()
    {
        if (_buffs.Count == 0)
        {
            return true;
        }
        return false;
    }
    public bool BuffExistsByName(string _name)
    {
        for (int i = 0; i < _buffs.Count; i++)
        {
            buffIndex = _buffs[i];
            if (_name == buffIndex.name)
            {
                return true;
            }
        }
        return false;
    }
    public int GetBuffIndexByName(string myName)
    {
        for (int i = 0; i < _buffs.Count; i++)
        {
            buffIndex = _buffs[i];
            if (myName == buffIndex.name)
            {
                return i;
            }        
        }    
        return -1;
    }
    public void SetBuffIndefinite(string _name, bool _indefinite) //"Cure" buff
    {
        if (BuffExistsByName(_name) == true)
        {
            int i = GetBuffIndexByName(_name);
            _buffs[i].indefinite = _indefinite;
        }
    }
    public void HandleBuffs()
    {
        if (_buffs.Count == 0)
        {
            return;
        }

        for (int i = 0; i < _buffs.Count; i++)
        {
            buffIndex = _buffs[i];
            buffIndex.Run();
        }
    }
    public void EnqueueBuff(Buff _buff)
    {
        if (_buff.stackable == true)
        {
            AddBuff(_buff);
            return;
        }
        else if (_buff.stackable == false)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                buffIndex = _buffs[i];
                if (_buff.name == buffIndex.name)
                {
                    return;
                }                
            }       
            AddBuff(_buff);
        }
    }
    private void AddBuff(Buff new_buff)
    {
        new_buff.Enter();
        _buffs.Add(new_buff);
        LogNewBuff(new_buff);
    }
    public void RecycleEmpties()
    {
        if (_buffs.Count > 0)
        {
            _buffs.Sort((left, right) => left.duration.CompareTo(right.duration));
            int i = _buffs.Count - 1;

            buffIndex = _buffs[i];

            if (buffIndex.duration == 0)
            {
                buffIndex.Exit();
                LogRemovedBuff(buffIndex);
                _buffs.RemoveAt(i);                
            }
        }
    }
    public void ElapseIndexLifeTime(int i)
    {
        Buff _ibuffs = _buffs[i];
        if (_ibuffs.indefinite == true)
        {
            _ibuffs.duration = _ibuffs.maxDuration;
            return;
        }
        else if (_ibuffs.indefinite == false)
        {
            _ibuffs.duration -= 1;
            _ibuffs.duration = Mathf.Clamp(_ibuffs.duration, 0, _ibuffs.maxDuration);
        }
    }
    public void HandleBuffListLifetime()
    {
        if (_buffs.Count != 0)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                ElapseIndexLifeTime(i);
            }
        }
    }

    private void Awake()
    {
        simTime.OnSimulationTick += HandleBuffListLifetime;    
    }

    // Update is called once per frame
    void Update()
    {
        HandleBuffs();
        RecycleEmpties();
    }

    private void OnGUI()
    {
        if (_buffs.Count != 0)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                guiBuffIndex = _buffs[i];
                GUI.Label(new Rect(1050, 380 + heightDisplacement * i, 300, 20), guiBuffIndex.name);
            }
        }    
    }
}