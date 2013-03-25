using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace ServoControl
{
    public class Program
    {
        public static void Main()
        {
            Thread.Sleep(350);

            var led = new OutputPort(Pins.ONBOARD_LED, false);
            var readIn = new SecretLabs.NETMF.Hardware.AnalogInput(Pins.GPIO_PIN_A0);

            while (true)
            {
                int readRaw = readIn.Read();
                double anVolt = readRaw * (3.3 / 1024);
                double distance = anVolt / 0.0064453125;

                Debug.Print(distance + " inches");
                SelectivelyLightLed(led, distance);

                Thread.Sleep(3000);
            }
        }

        private static void SelectivelyLightLed(OutputPort led, double distance)
        {
            led.Write(distance < 10.0);
        }
    }
}
