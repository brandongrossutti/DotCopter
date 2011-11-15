using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using DotCopter.Configurator.UserControls;

namespace DotCopter.Configurator
{
    public partial class frmConfigurator : Form
    {
        private MultiCopterSupportedConfiguration _supportedConfiguration;
        private MultiCopter _configuration = new MultiCopter();
        public frmConfigurator()
        {
            InitializeComponent();
            cbMulticopter.SelectedIndex = 0;
            OpenTreeviewConfig();
            _supportedConfiguration = new MultiCopterSupportedConfiguration();
        }

        private void MulticopterDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void PnlMulticopterDragDrop(object sender, DragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            UpdateConfig(node);
        }

        private void UpdateConfig(TreeNode node)
        {
            switch (node.Parent.Text)
            {
                case "MicroControllers":
                    {
                        _configuration.MicrocontrollerSettings.ControllerType = node.Text;
                        break;
                    }
                case "MultiFunctional":
                    {
                        _configuration.SensorSettings.AddMultiFunctional(node.Text);
                        break;
                    }
                case "Gyros":
                    {
                        _configuration.SensorSettings.AddGyro(node.Text);
                        break;
                    }
                case "Accellerometer":
                    {
                        _configuration.SensorSettings.AddAccellerometer(node.Text);
                        break;
                    }
                case "Motors":
                    {
                        _configuration.MotorSettings.Name = node.Text;
                        break;
                    }
                case "Radios":
                    {
                        _configuration.RadioSettings.Name = node.Text;
                        break;
                    }
                case "PID Algorithms":
                    {
                        _configuration.PidSettings.Strategy = node.Text;
                        break;
                    }
            }
            LoadGraph();

        }

        private void TreeView1ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = (TreeNode)e.Item;
            if (node.Tag != null)
            {
                DoDragDrop(e.Item, DragDropEffects.Copy);
            }
        }

        private void cbMulticopter_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void LoadDefaultsToolStripMenuItemClick(object sender, EventArgs e)
        {
            LoadDefaultConfig();
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveConfiguration(openFileDialog.FileName);
            }
        }

        private void SaveConfiguration(string fileName)
        {
            Stream stream = new FileStream(fileName, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(MultiCopter));
            serializer.Serialize(stream, _configuration);
            stream.Close();
        }

        private void LoadDefaultConfig()
        {
            try
            {
                Stream stream = new FileStream(@"DefaultConfiguration.xml", FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(MultiCopter));
                _configuration = (MultiCopter)serializer.Deserialize(stream);
                stream.Close();
                LoadGraph();
            }
            catch (Exception)
            {
                MessageBox.Show(@"Invalid Config");
            }
        }

        private void LoadGraph()
        {
            SaveConfiguration("temp.xml");
            TextReader textReader = new StreamReader("temp.xml");
            richTextBox1.Text = textReader.ReadToEnd();
            textReader.Close();
            lstSelectedHardware.Items.Clear();
            lstSelectedHardware.Items.Add(_configuration.MicrocontrollerSettings.ControllerType);
            lstSelectedHardware.Items.Add(_configuration.SensorSettings.Gyro);
            lstSelectedHardware.Items.Add(_configuration.SensorSettings.MultiFunctional);
            lstSelectedHardware.Items.Add(_configuration.SensorSettings.Accellerometer);
            lstSelectedHardware.Items.Add(_configuration.RadioSettings.Name);
            lstSelectedHardware.Items.Add(_configuration.MotorSettings.Name);
            lstSelectedHardware.Items.Add(_configuration.PidSettings.Strategy);
            lstSelectedHardware.Items.Add("Controller Loop Settings");
        }


        private void LoadConfig()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Stream stream = openFileDialog.OpenFile();
                    XmlSerializer serializer = new XmlSerializer(typeof(MultiCopterSupportedConfiguration));
                    _configuration = (MultiCopter)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Invalid Config");
                }
            }
        }


        private void OpenTreeviewConfig()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Stream stream = new FileStream("TreeView.xml", FileMode.Open);
                treeView1.Nodes.Clear();
                XmlSerializer serializer = new XmlSerializer(typeof(MultiCopterSupportedConfiguration));
                MultiCopterSupportedConfiguration config = (MultiCopterSupportedConfiguration)serializer.Deserialize(stream);
                config.ToTreeView(treeView1);
                stream.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Invalid Config");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LstSelectedHardwareSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lstSelectedHardware.SelectedIndex)
            {
                case 1:
                    {
                        ucI2cSettings i2CSettings = new ucI2cSettings();
                        AddControlToSettings(i2CSettings);
                        break;
                    }
                case 2:
                    {
                        ucI2cSettings i2CSettings = new ucI2cSettings();
                        AddControlToSettings(i2CSettings);
                        break;
                    }
                case 3:
                    {
                        ucI2cSettings i2CSettings = new ucI2cSettings();
                        AddControlToSettings(i2CSettings);
                        break;
                    }
                case 5:
                    {
                        ucMotors motorSettings = new ucMotors();
                        AddControlToSettings(motorSettings);
                        break;
                    }
                case 6:
                    {
                        ucPID pidSettings = new ucPID();
                        AddControlToSettings(pidSettings);
                        break;
                    }
                case 7:
                    {
                        ucLoopSettings loopSettings = new ucLoopSettings();
                        AddControlToSettings(loopSettings);
                        break;
                    }

                default:
                    {
                        gpSettings.Controls.Clear();
                        break;
                    }
            }
        }

        private void AddControlToSettings(Control control)
        {
            gpSettings.Controls.Clear();
            control.Location = new Point(10, 20);
            gpSettings.Controls.Add(control);
        }
    }
}