using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionCreator : MonoBehaviour {

    public static PotiLightConnection createPotiDimmerConnection(string id, string idSource, string idTarget)
    {
        PotiLightConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is Poti)
         {
             c = new PotiLightConnection(id, (Poti)source, (DimmerActuator)target);
         }

        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }

    public static PotiHueConnection createPotiHueConnection(string id, string idSource, string idTarget)
    {
        PotiHueConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is Poti && target is HueDimmer)
        {
            c = new PotiHueConnection(id, (Poti)source, (HueDimmer)target);
        }

        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }

    public static ColorHueConnection createColorHueConnection(string id, string idSource, string idTarget)
    {
        ColorHueConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is ColorSensor && target is HueDimmer)
        {
            c = new ColorHueConnection(id, (ColorSensor)source, (HueDimmer)target);
        }

        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }


    public static SensActConnection createSensActConnection(string id, string idSource, string idTarget, float rightSide, LogicSenActInput.Operatoren condOperator, SensActConnection.commands targetCommand)
    {
        SensActConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is Sensor && target is Actuator)
        {
            c = new SensActConnection(id, (Sensor)source, (Actuator)target, rightSide, condOperator, targetCommand);
        }
       
        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }

    public static NfcActConnection createNfcActConnection(string id, string idSource, string idTarget, string rightSide, LogicSenActInput.Operatoren condOperator, SensActConnection.commands targetCommand)
    {
        NfcActConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is NfcReader && target is Actuator)
        {
            c = new NfcActConnection(id, (NfcReader)source, (Actuator)target, rightSide, condOperator, targetCommand);
        }

        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }

    public static MyoActConnection createMyoActConnection(string id, string idSource, string idTarget, string rightSide, LogicSenActInput.Operatoren condOperator, SensActConnection.commands targetCommand)
    {
        MyoActConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is MyoArmband && target is Actuator)
        {
            c = new MyoActConnection(id, (MyoArmband)source, (Actuator)target, rightSide, condOperator, targetCommand);
        }

        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }


    public static ActActConnection createActActConnection(string id, string idSource, string idTarget, ActActConnection.commands sourceCommand, ActActConnection.commands targetCommand)
    {
        ActActConnection c = null;

        Item source = ItemManager.getInstance().getItem(idSource);
        Item target = ItemManager.getInstance().getItem(idTarget);

        if (source is Actuator && target is Actuator)
        {
            c = new ActActConnection(id, (Actuator)source, (Actuator)target, sourceCommand, targetCommand);
        }
        if (c != null)
            ConnectionManager.getInstance().getConnections().Add(id, c);

        return c;
    }
}
