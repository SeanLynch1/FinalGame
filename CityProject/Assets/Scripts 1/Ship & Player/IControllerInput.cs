public delegate void InputEvent();
public delegate void InputEventFloat(float value);
public delegate void InputEventVector3(float x, float y, float z);


public interface IControllerInput 
{
    event InputEvent FireEvent;
     event InputEventFloat SlideEvent;
     event InputEventFloat ForwardEvent;
     event InputEventFloat YawEvent;
     event InputEventFloat PitchEvent;
     event InputEventFloat RollEvent;
     event InputEventFloat SideStrafeEvent;
     event InputEventFloat VerticalStrafeEvent;
    event InputEventVector3 TurnEvent;

}
