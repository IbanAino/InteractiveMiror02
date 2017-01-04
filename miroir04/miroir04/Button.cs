using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace miroir04
{
    class Button
    {
        //ATTRIBUTS
        private const int BUTTON_PIN = 5;
        private GpioPin buttonPin;

        //definition of the delagate
        public delegate void ButtonEventEndler(object source, EventArgs args);

        //definition of the event associated to the delagate
        public event ButtonEventEndler buttonPressed;


        //CONSTRUCTEUR
        public Button()
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
                return;
            }

            buttonPin = gpio.OpenPin(BUTTON_PIN);

            // Check if input pull-up resistors are supported
            if (buttonPin.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                buttonPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                buttonPin.SetDriveMode(GpioPinDriveMode.Input);

            // Set a debounce timeout to filter out switch bounce noise from a button press
            buttonPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);

            // Register for the ValueChanged event so our buttonPin_ValueChanged 
            // function is called when the button is pressed
            buttonPin.ValueChanged += buttonPin_ValueChanged;
        }

        // EVENTS LISTEND AND EXECUTED
        private void buttonPin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            OnButtonPressed();
        }

        //EVENTS SENDED TO OTHER CLASSES
        protected virtual void OnButtonPressed()
        {
            if (buttonPressed != null)
                buttonPressed(this, EventArgs.Empty);

        }
    }
}
