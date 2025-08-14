using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

public class VigemService
{
    private readonly ViGEmClient _client;
    private readonly IXbox360Controller _controller;

    public VigemService()
    {
        _client = new ViGEmClient();
        _controller = _client.CreateXbox360Controller();
    }

    public void ActivateController()
    {
        _controller.Connect();
    }

    public void DeactivateController()
    {
        _controller.Disconnect();
    }

    public void SetLeftStick(double x, double y)
    {
        // Convert -1..1 to -32768..32767 kay apparently amo an tatanggap han vigem driver
        short lx = (short)(x * 32767);
        short ly = (short)(y * 32767);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbX, lx);
        _controller.SetAxisValue(Xbox360Axis.LeftThumbY, ly);
    }

    public void SetRightStick(double x, double y)
    {
        // amo gihap didi
        short rx = (short)(x * 32767);
        short ry = (short)(y * 32767);
        _controller.SetAxisValue(Xbox360Axis.RightThumbX, rx);
        _controller.SetAxisValue(Xbox360Axis.RightThumbY, ry);
    }

    public void SetButtonState(Xbox360Button button, bool pressed)
    {
        _controller.SetButtonState(button, pressed);
    }

    public void SetTriggerValue(Xbox360Slider trigger, byte value)
    {
        _controller.SetSliderValue(trigger, value);
    }
}
