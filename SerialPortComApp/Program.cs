using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SerialPortCommunication
{

    internal class Program
    {

        static void Main(string[] args)
        {
            string masterCard = "116 30 144 171 ";
            List<string> ports = new List<string>();
            Console.WriteLine("Hello World! derleme 2");
            GetPortNames();
            List<SerialPort> serialPorts = new List<SerialPort>();
            Console.WriteLine("port isimleri okunuyor");
            Thread readThread = new Thread(PortsRead);
            PortsOpen();
            Console.WriteLine("port bağlantısı başladı");
            // UIdControl("asd1",false);
            void GetPortNames()
            {

                //foreach (var item in SerialPort.GetPortNames())
                //{
                //    ports.Add(item);
                //    Console.WriteLine("port eklendi: "+item);
                //}
                //if (ports.Count == 2)
                //{
                //    Console.WriteLine("port okunamadı.");
                //    return;
                //}
                ports.Add("/dev/serial0");
                ports.Add("/dev/serial1");
                ports.Add("/dev/serial2");
                ports.Add("/dev/serial3");
                //ports.Add("COM5");
                //ports.Add("COM4");

            }

            void PortsOpen()
            {
                SerialPort port1 = new SerialPort() { PortName = ports[0], BaudRate = 115200 };


                SerialPort port2 = new SerialPort() { PortName = ports[1], BaudRate = 115200 };


                SerialPort port3 = new SerialPort() { PortName = ports[2], BaudRate = 115200 };
               
                SerialPort port4 = new SerialPort() { PortName = ports[3], BaudRate = 115200 };
               


                serialPorts.Add(port1);
                serialPorts.Add(port2);
                serialPorts.Add(port3);
                serialPorts.Add(port4);
                readThread.Start();
            }

            void PortsRead()//, SerialPort port3)//, SerialPort port4)
            {

                Console.WriteLine("ports read open");
                while (true)
                {
                    foreach (var item in serialPorts)
                    {


                        string UId = string.Empty;
                        item.WriteTimeout = 500;
                        item.ReadTimeout = 500;
                        item.Open();

                        try
                        {
                            UId = item.ReadLine(); // Wait for data reception
                            Thread.Sleep(200);
                            if (UId.Length != 0)
                            {
                                Console.WriteLine(UId);
                                var value = UId.Split(',');

                                //string[] value = UId.Split(',');

                                if (value[0] == masterCard)
                                {
                                    AddUId(item);
                                }
                                else
                                {

                                    if (UIdControl(value[0], false))
                                    {
                                        item.WriteLine("true.");

                                    }

                                }
                                UId = "";
                            }

                            item.Close();

                        }
                        catch (TimeoutException Ex)//Catch Time out Exception
                        {
                            item.Close();

                        }

                    }

                }
            }







            bool UIdControl(string UId, bool isNewUId)
            {


                List<string> satirlarList = new List<string>();
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    string satir; //burada okuduğunuz her satırı atamamız için gerekli değişkeni tanımlıyoruz.
                    while ((satir = sr.ReadLine()) != null) //Döngü kurup eğer satır boş değilse, satirlarList List'ine ekleme yapıyoruz.
                    {
                        satirlarList.Add(satir);

                    }
                    sr.Close();
                }
                foreach (var item in satirlarList)
                {
                    if (item == UId)
                    {
                        Console.WriteLine("kapı açıldı");
                        return true;

                    }
                }

                Console.WriteLine("kapı açılamadı");
                return false;
            }

            void AddUId(SerialPort port)
            {
                if (port.IsOpen)
                {
                    string UId = port.ReadLine();
                    string[] val = UId.Split(',');
                    if (UId.Length == 0 || val[0] == masterCard)
                    {
                        UId = "";
                        AddUId(port);

                    }
                    else
                    {

                        string[] value = UId.Split(',');


                        string filelocation = @"./";
                        string filename = "data.txt";
                        if (File.Exists(filelocation + filename))
                        {
                            StreamWriter sw = new StreamWriter(filelocation + filename, true);
                            sw.WriteLine(value[0]);
                            sw.Close();


                            UId = "";
                            port.ReadExisting();
                        }
                        else
                        {
                            // Dosya yok  
                        }


                    }
                }
            }

        }
    }
}