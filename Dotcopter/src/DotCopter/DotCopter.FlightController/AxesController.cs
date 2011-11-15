
using System;
using DotCopter.Avionics;
using DotCopter.ControlAlgorithms.Implementations.PID;
using DotCopter.ControlAlgorithms.PID;

namespace DotCopter.FlightController
{
    public class AxesController
    {
        public readonly ControlAlgorithms.PID.PID PitchController;
        public readonly ControlAlgorithms.PID.PID RollController;
        public readonly ControlAlgorithms.PID.PID YawController;

        public AxesController(PIDSettings pitchSettings, PIDSettings rollSettings, PIDSettings yawSettings, bool useWindup)
        {
            if (useWindup)
            {
                PitchController = new PIDWindup(pitchSettings);
                RollController = new PIDWindup(rollSettings);
                YawController = new PIDWindup(yawSettings);
            }
            else
            {
                PitchController = new ControlAlgorithms.Implementations.PID.PID(pitchSettings);
                RollController = new ControlAlgorithms.Implementations.PID.PID(rollSettings);
                YawController = new ControlAlgorithms.Implementations.PID.PID(yawSettings);
            }
            Axes = new AircraftPrincipalAxes(){Pitch = 0, Roll = 0, Yaw = 0};
        }

        public void Update(AircraftPrincipalAxes radio, AircraftPrincipalAxes gyro, float deltaTimeGain)
        {
            PitchController.Update(radio.Pitch, gyro.Pitch, deltaTimeGain);
            RollController.Update(radio.Roll, gyro.Roll, deltaTimeGain);
            YawController.Update(radio.Yaw, gyro.Yaw, deltaTimeGain);
            Axes.Pitch = PitchController.Output;
            Axes.Roll = RollController.Output;
            Axes.Yaw = YawController.Output;
        }

        public AircraftPrincipalAxes Axes;

        public void ZeroIntegral()
        {
            PitchController.ZeroIntegral();
        }
    }
}
