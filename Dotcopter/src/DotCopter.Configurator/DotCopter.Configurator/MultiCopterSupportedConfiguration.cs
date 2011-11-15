using System;
using System.Windows.Forms;
using DotCopter.Configurator.Supported;

namespace DotCopter.Configurator
{
    [Serializable]
    public class MultiCopterSupportedConfiguration
    {
        public MicroControllers MicroControllers;
        public Sensors Sensors;
        public Motors Motors;
        public Radio Radio;
        public PidAlgorithm PidAlgorithm;


        public MultiCopterSupportedConfiguration()
        {
            MicroControllers = new MicroControllers();
            Sensors = new Sensors();
            Motors = new Motors();
           Radio = new Radio();
            PidAlgorithm = new PidAlgorithm();
        }
        public void ToTreeView(TreeView treeview)
        {
            TreeNode controllerNode = new TreeNode("MicroControllers");
            foreach (HardwareItem hardwareItem in MicroControllers.Boards)
            {
                TreeNode hardwareNode = new TreeNode(hardwareItem.Name);
                controllerNode.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }

            treeview.Nodes.Add(controllerNode);

            TreeNode sensorNode = new TreeNode("Sensors");
            TreeNode multiFunctional = new TreeNode("MultiFunctional");
            foreach (HardwareItem hardwareItem in Sensors.Multifunctional)
            {
                TreeNode  hardwareNode = new TreeNode(hardwareItem.Name);
                multiFunctional.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }

            sensorNode.Nodes.Add(multiFunctional);

            TreeNode gyros = new TreeNode("Gyros");
            foreach (HardwareItem hardwareItem in Sensors.Gyros)
            {
                TreeNode hardwareNode = new TreeNode(hardwareItem.Name);
                gyros.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }
            sensorNode.Nodes.Add(gyros);

            TreeNode accellerometer = new TreeNode("Accellerometer");
            foreach (HardwareItem hardwareItem in Sensors.Accelleromters)
            {
                TreeNode hardwareNode = new TreeNode(hardwareItem.Name);
                accellerometer.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }
            sensorNode.Nodes.Add(accellerometer);

            treeview.Nodes.Add(sensorNode);

            TreeNode radioNodes = new TreeNode("Radios");
            foreach (HardwareItem hardwareItem in Radio.Radios)
            {
                TreeNode hardwareNode = new TreeNode(hardwareItem.Name);
                radioNodes.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }
            treeview.Nodes.Add(radioNodes);

            TreeNode motorNodes = new TreeNode("Motors");
            foreach (HardwareItem hardwareItem in Motors.AllMotors)
            {
                TreeNode hardwareNode = new TreeNode(hardwareItem.Name);
                motorNodes.Nodes.Add(hardwareNode);
                hardwareNode.Tag = hardwareItem;
            }
            treeview.Nodes.Add(motorNodes);

            TreeNode pidNode = new TreeNode("PID Algorithms");
            foreach (string strategy in PidAlgorithm.Strategies)
            {
                TreeNode  algoNode = new TreeNode(strategy);
                pidNode.Nodes.Add(algoNode);
                algoNode.Tag = strategy;
            }
            treeview.Nodes.Add(pidNode);
        }
    }
}
