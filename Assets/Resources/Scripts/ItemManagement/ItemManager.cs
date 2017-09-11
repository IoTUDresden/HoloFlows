
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    private static ItemManager instance = new ItemManager();
    private Dictionary<string, Item> items;
    private Dictionary<string, GameObject> gameObjects;
    public static ItemManager getInstance()
    {
        return instance;
    }

    private ItemManager()
    {
        items = new Dictionary<string, Item>();
        gameObjects = new Dictionary<string, GameObject>();
    }

    private Item createItem(string name, string state, string shortName, string unit=null)
    {
        switch (name) {
            case Settings.AirPressure:
                return new AirPressure(name, state, unit, shortName);
            case Settings.AmbientLight:
                return new AmbientLight(name, state, unit, shortName);
            case Settings.AmbientTemp:
                return new AmbientTemp(name, state, unit, shortName);
            case Settings.Dimmer1 + "_Sensor":
                return new DimmerSensor(Settings.Dimmer1, state, null, shortName);
            case Settings.Dimmer1 + "_Actuator":
                return new DimmerActuator(Settings.Dimmer1, state, shortName);
            case Settings.Dimmer2 + "_Sensor":
                return new DimmerSensor(Settings.Dimmer2, state, null, shortName);
            case Settings.Dimmer2 + "_Actuator":
                return new DimmerActuator(Settings.Dimmer2, state, shortName);
            case Settings.DoorLock:
                return new DoorLock(name, state, shortName);
            case Settings.GongSound:
                return new GongSound(name, state, shortName);
            case Settings.GongLight:
                return new GongLight(name, state, shortName);
            case Settings.Humidity:
                return new Humidity(name, state, unit, shortName);
            case Settings.AmbientLight2:
                return new AmbientLight(name, state, unit, shortName);
            case Settings.LinearPoti:
                return new Poti(name, state, unit, shortName);
            case Settings.NFCReader:
                return new NfcReader(name, state, shortName);
            case Settings.CoffeeMachine:
                return new CoffeeMachineActuator(name, state, shortName);
            case Settings.myoArmband:
                return new MyoArmband(name, state, shortName);
            case Settings.colorSensor:
                return new ColorSensor(name, state, shortName);
            case Settings.hueLamp1dimmer:
                return new HueDimmer(name, state, shortName);
            case Settings.hueLamp2dimmer:
                return new HueDimmer(name, state, shortName);
            //Associate HueColor with HueDimmer if HueDimmer already exists
            case Settings.hueLamp1color:
                {
                    HueColor hc1 = new HueColor(name, state, shortName);
                    HueDimmer hd1 = (HueDimmer)ItemManager.getInstance().getItem(Settings.hueLamp1dimmer);
                    if (hd1!=null)
                    {
                        hd1.hueColor = hc1;
                    }
                    return hc1;
                }
            case Settings.hueLamp2color:
                HueColor hc2 = new HueColor(name, state, shortName);
                HueDimmer hd2 = (HueDimmer)ItemManager.getInstance().getItem(Settings.hueLamp2dimmer);
                if (hd2 != null)
                {
                    hd2.hueColor = hc2;
                }
                return hc2;
            default:
                Debug.Log("invalid name: " + name);
                break;
        }

        return null;
    }

    public void update(string key, Item item)
    {
        items[key] = item;
    }

    public void updateItem(string key, string name, string state, string shortName, string unit = null)
    {
        //Debug.LogFormat("updating key {0} name {1} state {2} shortName {3} unit{4}", key, name, state, shortName, unit);
        if (items.ContainsKey(key))
        {
            Item i = items[key];
            //if (i == null)
                //Debug.Log("item was null");
            //else
                //Debug.Log("item wasnt null");
            string gameObjectName = name;
            
            if (name.Equals(Settings.NFCReader))
            {
                ((NfcReader)i).update(name, state, shortName, unit);
                gameObjectName = "Sensor_" + gameObjectName;
            }
            else if (name.Equals(Settings.myoArmband))
            {
                ((MyoArmband)i).update(name, state, shortName, unit);
                gameObjectName = "Sensor_" + gameObjectName;
            }
            else if (i.type.Equals("Sensor"))
            {
                ((Sensor)i).update(name,state,shortName,unit);
                gameObjectName = "Sensor_" + gameObjectName;
            }
            else if (i.type.Equals("Actuator"))
            {
                ((Actuator)i).update(name,state,shortName,unit);
                gameObjectName = "Actuator_" + gameObjectName;
            }
            
            //Associate HueColor with HueDimmer if HueDimmer didn't exist at item creation time
            if (name.Equals(Settings.hueLamp1dimmer))
            {
                HueDimmer hd1 = (HueDimmer)ItemManager.getInstance().getItem(name);
                if (hd1.hueColor==null)
                {
                    hd1.hueColor = (HueColor)ItemManager.getInstance().getItem(Settings.hueLamp1color);
                }
            }

            if (name.Equals(Settings.hueLamp2dimmer))
            {
                HueDimmer hd2 = (HueDimmer)ItemManager.getInstance().getItem(name);
                if (hd2.hueColor == null)
                {
                    hd2.hueColor = (HueColor)ItemManager.getInstance().getItem(Settings.hueLamp2color);
                }
            }

            if (!gameObjectName.Contains("Sensor") && !gameObjectName.Contains("homematic_dimmer"))
            {
                GameObject g = GameObject.Find(gameObjectName);
                if (!gameObjects.ContainsKey(gameObjectName))
                {
                    if (g != null)
                        gameObjects.Add(gameObjectName, g);
                    else
                        Debug.Log("tried to add " + gameObjectName + " but could not find reference");
                }

                //(de-)activate prefab
                if (gameObjects.ContainsKey(gameObjectName))
                {
                    GameObject gameObject = gameObjects[gameObjectName];
                    //Debug.Log("gameobject " + gameObjectName + " found");
                    if (state.Equals("error") | state.Equals("NULL") | state.Equals("UNDEF"))
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        gameObject.SetActive(true);
                    }
                }
                else
                    Debug.Log("could not find " + gameObjectName);
            }
        }
        else
        {
            if (Settings.Items.Contains(name))
            {
                items[key] = createItem(key, state, shortName, unit);
                Debug.Log("created item " + name + "with key " + key);
                if (GameObject.Find(items[key].type + "_" + name) == null)
                {
                    if (items[key].type.Equals("Sensor") && !name.Contains("homematic_dimmer"))
                    {
                        GameObject.Find("Managers").GetComponent<InstantiatePrefab>().InstantiateSensor(name);
                    }
                }
            }
        }
    }

    public Item getItem(string key)
    {
        //per default treat dimmer as an actuator. if the key states that it is explicitly requested as a sensor, return the sensor instance
        if (key.Contains("homematic_dimmer"))
        {
            if (!key.EndsWith("_Sensor"))
                key = key + "_Actuator";
        }

        if (items.ContainsKey(key))
            return items[key];
        else
            return null;
    }
}
