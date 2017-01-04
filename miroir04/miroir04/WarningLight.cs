using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace miroir04
{
    class WarningLight
    {
        //ATTRIBUTS
        private const int LED_PIN = 6;
        private GpioPin ledPin;
        //private GpioPinValue ledPinValue = GpioPinValue.High;
        private string report { get; set; }

        //CONSTRUCTEUR
        public WarningLight()
        {
            InitGPIO();
        }

        //METHODES
        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                report = "There is no GPIO controller on this device.";
                return;
            }

            ledPin = gpio.OpenPin(LED_PIN);

            // Initialize LED to the OFF state by first writing a HIGH value
            // We write HIGH because the LED is wired in a active LOW configuration
            ledPin.Write(GpioPinValue.High);
            ledPin.SetDriveMode(GpioPinDriveMode.Output);

            report = "GPIO pins initialized correctly.";
        }

        public void method()
        {
            //nothing
        }

        public void method02()
        {

        }

        //EVENTS LISTENED
        public void OnButtonPressed(object source, EventArgs e)
        {
            // toggle the state of the LED every time the button is pressed
            //if (e.Edge == GpioPinEdge.FallingEdge)
            //{
            /*ledPinValue = (ledPinValue == GpioPinValue.Low) ?
                GpioPinValue.High : GpioPinValue.Low;
            ledPin.Write(ledPinValue);*/

            if (ledPin.Read() == GpioPinValue.Low)
            {
                ledPin.Write(GpioPinValue.High);
            }
            else
            {
                ledPin.Write(GpioPinValue.Low);
            }
            //}
        }
    }
}
