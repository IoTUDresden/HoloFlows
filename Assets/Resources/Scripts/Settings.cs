using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

static class Settings {
    public const string RestBaseURL = "http://192.168.1.3:8080/rest/items/";
    /*public static string URL_AmbientLight_GET = RestBaseURL + "tinkerforge_ambientLight_ambientLight_4";
    public static string URL_Humidity_GET = RestBaseURL + "tinkerforge_humidity_humidity_1";
    public static string URL_AirPressure_GET = RestBaseURL +  "tinkerforge_barometer_barometer_1";
    //To be deleted
    public static string URL_AmbientTemperature_GET = RestBaseURL + "tinkerforge_irTemp_ambTemp_1";
    public static string URL_IRTemperature_GET = RestBaseURL + "tinkerforge_irTemp_irTemp_1";
    public static string URL_LinearPoti_GET = RestBaseURL + "LinearPoti";
    //to be deleted
    public static string URL_MotionDetector_GET = RestBaseURL + "homematic_motionIndicator_motionIndicator_1";
    public static string URL_NFC_GET = RestBaseURL + "libnfcwrapper_idscan_idscanResultChannel_1";
    public static string URL_DoorLock_GET = RestBaseURL + "homematic_keymatic_keymatic_1";*/
    public const string URL_Dimmer1_POST = RestBaseURL + "homematic_dimmer_dimmer_1";
    public const string URL_Dimmer2_POST = RestBaseURL + "homematic_dimmer_dimmer_2";
    public const string URL_DoorLock_POST = RestBaseURL + "homematic_keymatic_keymatic_1";
    public const string URL_GongSound_POST = RestBaseURL + "homematic_gong_gong_1";
    public const string URL_GongLight_POST = RestBaseURL + "homematic_gong_lightGong_1";
    public const string POST_ON = "ON";
    public const string POST_OFF = "OFF";
    public const string POST_INCREASE = "INCREASE";
    public const string POST_DECREASE = "DECREASE";


    public const string AirPressure = "tinkerforge_barometer_barometer_1";
    public const string AmbientLight = "tinkerforge_ambientLight_ambientLight_4";
    public const string AmbientTemp = "tinkerforge_irTemp_ambTemp_1";
    public const string Dimmer1 = "homematic_dimmer_dimmer_1";
    public const string Dimmer2 = "homematic_dimmer_dimmer_2";
    public const string DoorLock = "homematic_keymatic_keymatic_1";
    public const string GongSound = "homematic_gong_gong_1";
    public const string GongLight = "homematic_gong_lightGong_1";
    public const string Humidity = "tinkerforge_humidity_humidity_1";
    public const string AmbientLight2 = "tinkerforge_ambientLight_ambientLight_3";
    public const string LinearPoti = "LinearPoti";
    public const string NFCReader = "libnfcwrapper_idscan_idscanResultChannel_1";
    public const string CoffeeMachine = "coffeemachine";
    public const string myoArmband = "MyoArmband";
    public const string colorSensor = "adafruit_playground_color";

    public const string hueLamp1dimmer = "hue_bulb210_dimmer_1";
    public const string hueLamp1color = "hue_bulb210_color_1";
    public const string hueLamp2dimmer = "hue_bulb210_dimmer_2";
    public const string hueLamp2color = "hue_bulb210_color_2";

    public static List<string> Sensors = new List<string> { AirPressure, AmbientLight, AmbientTemp, Humidity, LinearPoti, NFCReader, AmbientLight2, colorSensor, myoArmband };
    public static List<string> Actuators = new List<string> { CoffeeMachine, DoorLock, GongSound, GongLight, Dimmer1, Dimmer2, hueLamp1dimmer, hueLamp2dimmer, hueLamp1color, hueLamp2color};
    public static List<string> Items = Sensors.Concat(Actuators).ToList();

    //Deprecated
    //format: coffeebaseurl + brewinfix + cupcount(as integer, 1-10) + brewtypes + strengthinfix + strengthtypes
    //example: http://192.168.1.4:8080/machine_control/brew?cups=1&type=Beans&strength=Strong
    //public const string CoffeeMachineBaseURL = "http://192.168.1.3:8383/";
    //public const string URL_CoffeeMachine_GET_Brew = CoffeeMachineBaseURL + "machine_control/brew?cups=";
    //public const string Coffeemachine_type = "&type=";
    //public enum CoffeeMachine_Brewtypes { Unknown, Filter, Beans };
    //public const string Coffeemachine_strength = "&strength=";
    //public enum CoffeeMachine_Strengthtypes { Unknown, Weak, Normal, Strong };

    public const string CoffeeMachineBaseURL = "http://192.168.1.3:2080/";
    public const string CoffeeMachineIp = "192.168.1.4";
    public const string URL_CoffeeMachine_Status = CoffeeMachineBaseURL + "api/" + CoffeeMachineIp + "/status";
    public const string URL_CoffeeMachine_GET_Brew = CoffeeMachineBaseURL + "api/" + CoffeeMachineIp + "/start";

    public const string MyoServerIp = "192.168.1.42:8080";
    public const string URL_MyoArmband_Status = "http://" + MyoServerIp + "/myo";

    public const int requestUpdateNumber = 25;
    public const int statusUpdateNumber = 25;

    public const int coffeStatusUpdateNumber = 125;


}
