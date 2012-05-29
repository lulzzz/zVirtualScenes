﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using zVirtualScenes;
using zVirtualScenes_WPF.DeviceControls;
using zVirtualScenes_WPF.Groups;
using zVirtualScenes_WPF.PluginManager;
using zVirtualScenesModel;
using System.Threading;

namespace zVirtualScenes_WPF
{
    /// <summary>
    /// interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private App application = (App)Application.Current;
        private zvsLocalDBEntities context;
        
        public MainWindow()
        {
            context = application.zvsCore.context;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                //Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["ListViewSource"];
                myCollectionViewSource.Source = application.zvsCore.Logger.LOG;
            }
            application.zvsCore.Logger.WriteToLog(Urgency.INFO, "Main window loaded.", "zVirtualScenes");
        }       

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void AddSampleData()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                plugin p = new plugin { name = "TEST", friendly_name = "Test plugin", description = "None" };
                context.plugins.Add(p);

                device_types controller_dt = new device_types { plugin = p, name = "CONTROLLER", friendly_name = "OpenZWave Controller", show_in_list = true };
                context.device_types.Add(controller_dt);

                device ozw_device = new device
                {
                    node_id = 1,
                    device_types = controller_dt,
                    current_status = "Active",
                    friendly_name = "Sample Controller"
                };

                context.devices.Add(ozw_device);
                context.SaveChanges();               
            }));
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
            {
                Thread thread = new Thread(new ThreadStart(AddSampleData));
                thread.Start();

                //AddSampleData();

                //device ozw_device1 = new device
                //{
                //    node_id = 2,
                //    device_type_id = 2,
                //    current_status = "On",
                //    friendly_name = "Switch Sample"
                //};

                //device ozw_device2 = new device
                //{
                //    node_id = 3,
                //    device_type_id = 3,
                //    current_status = "25",
                //    friendly_name = "Dimmer Sample"
                //};


                //device ozw_device3 = new device
                //{
                //    node_id = 99,
                //    device_type_id = 4,
                //    current_status = "75",
                //    friendly_name = "Thermo Sample"
                //};

                //device ozw_device4 = new device
                //{
                //    node_id = 4,
                //    device_type_id = 5,
                //    current_status = "Unlocked",
                //    friendly_name = "Doorlock Sample"
                //};

                lock (context)
                {





                    //context.devices.Add(ozw_device1);
                    //context.devices.Add(ozw_device2);
                    //context.devices.Add(ozw_device3);
                    //context.devices.Add(ozw_device4);

                }
            }

            if (e.Key == Key.F12)
            {
                SetNamesDevOnly();
            }
        }

        private void SetNamesDevOnly()
        {
            lock (context)
            {
                foreach (device d in context.devices)
                {
                    switch (d.node_id)
                    {
                        case 1:
                            d.friendly_name = "Aeon Labs Z-Stick Series 2";
                            break;
                        case 3:
                            d.friendly_name = "Master Bathtub Light";
                            break;
                        case 4:
                            d.friendly_name = "Masterbath Mirror Light";
                            break;
                        case 5:
                            d.friendly_name = "Masterbed Hallway Light";
                            break;
                        case 6:
                            d.friendly_name = "Masterbed East Light";
                            break;
                        case 7:
                            d.friendly_name = "Masterbed Bed Light";
                            break;
                        case 8:
                            d.friendly_name = "Office Light";
                            break;
                        case 9:
                            d.friendly_name = "Family Hallway Light";
                            break;
                        case 10:
                            d.friendly_name = "Outside Entry Light";
                            break;
                        case 11:
                            d.friendly_name = "Entryway Light";
                            break;
                        case 12:
                            d.friendly_name = "Can Lights";
                            break;
                        case 13:
                            d.friendly_name = "Porch Light";
                            break;
                        case 14:
                            d.friendly_name = "Dining Table Light";
                            break;
                        case 15:
                            d.friendly_name = "Fan Light";
                            break;
                        case 16:
                            d.friendly_name = "Kitchen Light";
                            break;
                        case 17:
                            d.friendly_name = "Rear Garage Light";
                            break;
                        case 18:
                            d.friendly_name = "Driveway Light";
                            break;
                        case 19:
                            d.friendly_name = "TV Backlight";
                            break;
                        case 20:
                            d.friendly_name = "Fireplace Light";
                            break;
                        case 22:
                            d.friendly_name = "Label Printer";
                            break;
                        case 23:
                            d.friendly_name = "Brother Printer";
                            break;
                        case 24:
                            d.friendly_name = "South Thermostat";
                            break;
                        case 25:
                            d.friendly_name = "Masterbed Window Fan";
                            break;
                        case 26:
                            d.friendly_name = "Masterbed Thermostat";
                            break;
                        case 27:
                            d.friendly_name = "Aeon Labs Z-Stick Series 1 (Secondary)";
                            break;
                        case 28:
                            d.friendly_name = "Xmas Lights";
                            break;
                    }
                    context.SaveChanges();

                    //zvsEntityControl.CallonSaveChanges(null,
                    //  new List<zVirtualScenesCommon.Entity.zvsEntityControl.onSaveChangesEventArgs.Tables>() 
                    //   { 
                    //       zVirtualScenesCommon.Entity.zvsEntityControl.onSaveChangesEventArgs.Tables.device 
                    //   },
                    //  zvsEntityControl.onSaveChangesEventArgs.ChangeType.Modified);
                }

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(GroupEditor))
                {
                    window.Activate();
                    return;
                }
            }

            GroupEditor groupEditor = new GroupEditor();
            groupEditor.Owner = this;
            groupEditor.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PluginManagerWindow))
                {
                    window.Activate();
                    return;
                }
            }

            PluginManagerWindow new_window = new PluginManagerWindow();
            new_window.Owner = this;
            new_window.Show();
        }


    }
}