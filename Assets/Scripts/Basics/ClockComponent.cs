using System;
using UnityEngine;

namespace Basics
{
    public class ClockComponent : MonoBehaviour
    {
        private const double HoursToDegrees = -30.0;
        private const double MinutesToDegrees = -6.0;
        private const double SecondsToDegrees = -6.0;

        [SerializeField] private Transform _hoursPivot;
        [SerializeField] private Transform _minutesPivot;
        [SerializeField] private Transform _secondsPivot;
        [SerializeField] private bool _isAnalog;

        private void Update()
        {
            SetClockTime();
        }

        private void SetClockTime()
        {
            if (this._isAnalog)
                SetAnalogClock();
            else
                SetDiscreteClock();
        }

        private void SetAnalogClock()
        {
            var currentTime = DateTime.Now.TimeOfDay;
            SetTime(
                RotationFrom(HoursToDegrees, currentTime.TotalHours),
                RotationFrom(MinutesToDegrees, currentTime.TotalMinutes),
                RotationFrom(SecondsToDegrees, currentTime.TotalSeconds)
            );
        }

        private void SetDiscreteClock()
        {
            var currentTime = DateTime.Now;
            SetTime(
                RotationFrom(HoursToDegrees, currentTime.Hour),
                RotationFrom(MinutesToDegrees, currentTime.Minute),
                RotationFrom(SecondsToDegrees, currentTime.Second)
            );
        }

        private static Quaternion RotationFrom(double degreesConversion, double currentDateValue)
        {
            return Quaternion.Euler(0.0f, 0.0f, (float)(degreesConversion * currentDateValue));
        }

        private void SetTime(
            Quaternion hoursQuaternion, 
            Quaternion minutesQuaternion,
            Quaternion secondsQuaternion)
        {
            this._hoursPivot.localRotation = hoursQuaternion;
            this._minutesPivot.localRotation = minutesQuaternion;
            this._secondsPivot.localRotation = secondsQuaternion;
        }
    }
}