using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            bool isAddUId = false;
            List<string> ports = new List<string>();

            Console.WriteLine("Versin 1.0: Access denied hatasina cozum araniyor.\nLutfen 20 saniye icinde usb baglantisini saglayiniz.");
            Thread.Sleep(20000);
            GetPortNames();
            List<SerialPort> serialPorts = new List<SerialPort>();

            Console.WriteLine("port isimleri okunuyor");
            //Thread readThread = new Thread(PortsRead);

            PortsOpen();

            // readThread.Start();
            PortsRead();

            void GetPortNames()
            {

                /* foreach (var item in SerialPort.GetPortNames())
                  {
                      ports.Add(item);
                      Console.WriteLine("port eklendi: " + item);
                  }
                  if (ports.Count == 2)
                  {
                      Console.WriteLine("port okunamadı.");
                      return;
                  }
                
           
                ports.Add("/dev/serial0");
                ports.Add("/dev/serial1");
                ports.Add("/dev/serial2");
                ports.Add("/dev/serial3");
               
                 */
                ports.Add("/dev/ttyUSB0");
                ports.Add("/dev/ttyUSB1");
                ports.Add("/dev/ttyUSB2");
                ports.Add("/dev/ttyUSB3");
                Console.WriteLine("port name /dev/ttyUSB");
            }

            void PortsOpen()
            {
                SerialPort port1 = new SerialPort() { PortName = ports[0], BaudRate = 115200, WriteTimeout = 600, ReadTimeout = 600 };


                SerialPort port2 = new SerialPort() { PortName = ports[1], BaudRate = 115200, WriteTimeout = 600, ReadTimeout = 600 };

                
                 SerialPort port3 = new SerialPort() { PortName = ports[2], BaudRate = 115200, WriteTimeout = 600, ReadTimeout = 600 };

                 SerialPort port4 = new SerialPort() { PortName = ports[3], BaudRate = 115200, WriteTimeout = 600, ReadTimeout = 600 };




                serialPorts.Add(port1);
                serialPorts.Add(port2);
                serialPorts.Add(port3);
                serialPorts.Add(port4);

                Console.WriteLine("ports are created");

            }



            void Read(SerialPort item)//, SerialPort port3)//, SerialPort port4)
            {

                if (!item.IsOpen)
                {
                    Console.WriteLine("port opening");
                    item.Open();
                    item.RtsEnable= true;
                    item.DtrEnable=true;
                   
                }
                
                
                string UId = string.Empty;

                try
                {
                    Thread.Sleep(200);
                     UId = item.ReadLine(); // Wait for data reception

                    if (UId.Length != 0)
                    {
                        Console.WriteLine(UId);
                        var value = UId.Split(',');

                        if (UId[0] == 'A')
                        {
                            UId = UId.Substring(1, UId.Length - 1);
                            AddUId(UId);
                        }

                        else if (UIdControl(value[0]))
                        {
                            item.WriteLine("true.");

                        }
                        else
                        {
                            item.WriteLine("false.");
                        }


                        UId = "";
                    }

                    //item.Close();

                }
                catch (TimeoutException Ex)//Catch Time out Exception
                {
                    //item.Close();
                    //Console.WriteLine(Ex.Message);

                }




            }







            bool UIdControl(string UId)
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

            void AddUId(string UId)
            {


                string[] value = UId.Split(',');
                List<string> satirlarList = new List<string>();
                string filelocation = @"./";
                string filename = "data.txt";
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
                    if (value[0] == item)
                    {
                        UIdDelete(satirlarList, filelocation, filename, item);
                        return;
                    }

                }

                if (File.Exists(filelocation + filename))
                {
                    StreamWriter sw = new StreamWriter(filelocation + filename, true);
                    sw.WriteLine(value[0]);
                    sw.Close();


                    UId = "";

                    Console.WriteLine("ekleme islemi basarili.");
                    return;
                }
                else
                {
                    // Dosya yok  
                    return;
                }

            }
            void PortsRead()
            {

                // readThread.Start();
                while (true)
                {
                    foreach (var item in serialPorts)
                    {
                        //if (item.IsOpen)
                        //{
                        //   item.Close();
                        //}
                        Read(item);
                    }

                }

            }
        }



        private static void UIdDelete(List<string> satirlarList, string filelocation, string filename, string item)
        {
            satirlarList.Remove(item);
            using (TextWriter tw = new StreamWriter(filelocation + filename))
            {
                tw.Write("");
                tw.Close();
            }
            using (StreamWriter sw = new StreamWriter(filelocation + filename))
            {
                foreach (var x in satirlarList)
                {

                    sw.WriteLine(x);

                }
                sw.Close();
            }
            Console.WriteLine("Silme işlemi basarili.");
        }
    }
}