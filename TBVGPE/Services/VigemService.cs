using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using Nefarius.ViGEm.Client.Targets.DualShock4;

public class VigemService
{
    private readonly ViGEmClient _client;
    private readonly IXbox360Controller _xBox360Controller;
    private readonly IDualShock4Controller _dualShock4Controller;

    private string _connectedController = "";

    public VigemService()
    {
        // Initialize ViGEm client
        _client = new ViGEmClient();

        // Initialize controllers
        _xBox360Controller = _client.CreateXbox360Controller();
        _dualShock4Controller = _client.CreateDualShock4Controller();
    }

    public void ConnectController(string controllerName)
    {
        if (_connectedController == controllerName)
        {
            return; // connected na ngadaan, ayaw na ig connect utro
        }
        else
        {
            DisconnectController(); // disconnect anay an previously connected controller, para iwas conflict
        }

        switch(controllerName)
        {
            case "Xbox360":
                _xBox360Controller.Connect();
                _connectedController = "Xbox360";
                break;

            case "DualShock4":
                _dualShock4Controller.Connect();
                _connectedController = "DualShock4";
                break;

            default:
                throw new ArgumentException("Unsupported controller type");
        }
    }

    public void DisconnectController()
    {
        // Disconnect the controllers
        _xBox360Controller.Disconnect();
        _dualShock4Controller.Disconnect();
        _connectedController = ""; // reset this shit
    }

    public void Set360LeftStick(double x, double y)
    {
        short leftX = (short)(x * 32767);
        short leftY = (short)(y * 32767);

        _xBox360Controller.SetAxisValue(Xbox360Axis.LeftThumbX, leftX);
        _xBox360Controller.SetAxisValue(Xbox360Axis.LeftThumbY, leftY);
    }

    public void Set360RightStick(double x, double y)
    {
        short rightX = (short)(x * 32767);
        short rightY = (short)(y * 32767);

        _xBox360Controller.SetAxisValue(Xbox360Axis.RightThumbX, rightX);
        _xBox360Controller.SetAxisValue(Xbox360Axis.RightThumbY, rightY);
    }

    public void SetDS4LeftStick(double x, double y)
    {
        byte leftX = (byte)((x + 1.0) * 127.5);
        byte leftY = (byte)((y + 1.0) * 127.5);

        _dualShock4Controller.SetAxisValue(DualShock4Axis.LeftThumbX, leftX);
        _dualShock4Controller.SetAxisValue(DualShock4Axis.LeftThumbY, leftY);
    }

    public void SetDS4RightStick(double x, double y)
    {
        byte rightX = (byte)((x + 1.0) * 127.5);
        byte rightY = (byte)((y + 1.0) * 127.5);

        _dualShock4Controller.SetAxisValue(DualShock4Axis.RightThumbX, rightX);
        _dualShock4Controller.SetAxisValue(DualShock4Axis.RightThumbY, rightY);
    }

    public void Set360ButtonState(Xbox360Button button, bool pressed)
    {
        _xBox360Controller.SetButtonState(button, pressed);
    }

    public void Set360TriggerValue(Xbox360Slider trigger, byte value)
    {
        _xBox360Controller.SetSliderValue(trigger, value);
    }

    public void SetDS4ButtonState(DualShock4Button button, bool pressed)
    {
        _dualShock4Controller.SetButtonState(button, pressed);
    }

    public void SetDS4DPadDirection(DualShock4DPadDirection direction)
    {
        _dualShock4Controller.SetDPadDirection(direction);
    }

    public void SetDS4TriggerValue(DualShock4Slider trigger, byte value)
    {
        _dualShock4Controller.SetSliderValue(trigger, value);
    }

    public void SetDS4Touchpad()
    {
        _dualShock4Controller.SubmitRawReport(new byte[]
        {
            //
        });
    }
}
