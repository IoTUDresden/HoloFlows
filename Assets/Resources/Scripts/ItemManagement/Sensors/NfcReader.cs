using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class NfcReader : Sensor {
    private OrderedDictionary ids;
    public string niceState;

    public NfcReader(string name, string state, string shortName) : base(name, state, "", shortName, 0, "Icons/NFCReader")
    {
        ids = new OrderedDictionary();
        ids.Add("04a9e11a092980", "o");
        ids.Add("049fe11a092980", "x");
        ids.Add("0476fb621c2380", "Office");
        if (!state.Equals(""))
        {
            this.niceState = ids[state].ToString();
        }
        else niceState = "";
    }

    /*
     * add new tag to dictionary or update existing assignment. If no key is given, use the tag as key
     */
    public void addOrUpdateTag(string tag, string key=null)
    {
        if(key!=null)
            ids[key] = tag;
        ids[tag] = tag;
    }

    public new void update(string id, string state, string shortName, string unit = null)
    {
        this.id = id;
        this.shortName = shortName;
        this.state = state;

        if (!state.Equals(""))
        {
            this.niceState = ids[state].ToString();
        }
        else this.niceState = "";

        if(!ids.Contains(state) && !state.Equals(""))
            addOrUpdateTag(state, state);
    }

    public string getName()
    {
        return (string)ids[state];
    }

    public string getNamebyIndex(int Zahl)
    {
        return (string)ids[Zahl];
       
        
    }

    public IDictionary getDict()
    {
        return ids;
    }

}
