using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete;
using System.IO.Ports;
namespace SDVS
{
    public partial class Form1 : Form
    {
        IUserService _userService;
        IContainer _container;
        public Form1(IContainer container)
        {
            InitializeComponent();
            _container = container;
            _userService=_container.Resolve<IUserService>();
        }
        private void userAdd()
        {
            bool _status = false;
            if (statusCombobox.SelectedIndex==0)
            {
                _status = true;
            }
          var result= _userService.add(new User { firstName=nameTextbox.Text, lastName=surnameTextbox.Text, UId=UIDTextbox.Text, userTypeId=userTypeCombobox.SelectedIndex+1, status=_status, lastLogin=DateTime.Now });
            MessageBox.Show(result.Message+"\n"+result.Success.ToString());
        }



        private void userDelete()
        { 
            var result = _userService.delete(_userService.getByUId(UIDTextbox.Text).Data);
        }


        private void userUpdate()
        {
            var result=_userService.update(_userService.getByUId(UIDTextbox.Text).Data);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UIDTextbox.Text = "Lütfen kartý okutunuz.";
            string[] ports = SerialPort.GetPortNames();
            string[] data = { };
            string UId = "";
            SerialPort serialPort = new SerialPort(ports[0], 9600);
            
           

            serialPort.Open();
            if (serialPort.IsOpen)
            {

                UId = serialPort.ReadLine();
                UIDTextbox.Text = UId.Substring(0, UId.Length - 2);
            }
            if(comboBox3.SelectedIndex == 2)
            {
                nameTextbox.Enabled = true;
                surnameTextbox.Enabled = true;
                statusCombobox.Enabled = true;
                userTypeCombobox.Enabled = true;
            }
            var result= _userService.getByUId(UId.Substring(0, UId.Length - 2));
                nameTextbox.Text = result.Data.firstName;
                surnameTextbox.Text = result.Data.lastName;
                userTypeCombobox.SelectedIndex=result.Data.userTypeId-1;
                if (result.Data.status)
                    statusCombobox.SelectedIndex = 0;
                else statusCombobox.SelectedIndex = 1;
           
            serialPort.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex==1|| comboBox3.SelectedIndex ==2)
            {
                nameTextbox.Enabled = false;
                surnameTextbox.Enabled = false;
                statusCombobox.Enabled = false;
                userTypeCombobox.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex==0)
            {
                userAdd();
            }
            if (comboBox3.SelectedIndex==1)
            {
                userDelete();
            }
        }
    }
}