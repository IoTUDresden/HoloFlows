using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class MyoArmband : Sensor
{
    private OrderedDictionary commands;
    public string niceState;

    public MyoArmband(string name, string state, string shortName) : base(name, state, "", shortName, 0, "Icons/Myo")
    {
        commands = new OrderedDictionary();
        commands.Add("WAVE_OUT", "Wave Out");
        commands.Add("WAVE_IN", "Wave In");
        commands.Add("REST", "Rest");
        commands.Add("FINGERS_SPREAD", "Fingers Spread");
        commands.Add("FIST", "Fist");
        commands.Add("UNKNOWN", "Unknown");
        commands.Add("DOUBLE_TAP", "Double Tap");
        this.state = commands[state].ToString();
        this.niceState = commands[state].ToString();
    }

    public new void update(string id, string state, string shortName, string unit = null)
    {
        this.id = id;
        this.shortName = shortName;
        this.state = commands[state].ToString();
        this.niceState = commands[state].ToString();

    }

    public string getName()
    {
        return (string)commands[state];
    }

    public string getNamebyIndex(int Zahl)
    {
        return (string)commands[Zahl];
    }

    public IDictionary getDict()
    {
        return commands;
    }

    public string getKeyByValue(string value)
    {
        foreach (string current in commands.Keys)
        {
            if (commands[current].Equals(value))
                return current;
            else return "";
        }
        return "";
    }
}
