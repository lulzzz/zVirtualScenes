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
using zVirtualScenesModel;

namespace zVirtualScenes_WPF.DynamicSettingsControls
{
    /// <summary>
    /// Interaction logic for BoolSettingsControl.xaml
    /// </summary>
    public partial class NumericSettingsControl : UserControl, DynamicSettingsInterface
    {
        zvsLocalDBEntities context;
        plugin_settings plugin_setting;
        public decimal MinValue = -99999;
        public decimal MaxValue = 99999;
        public bool ForceWholeNumber = false;
        private bool _haschanged = false;

        public NumericSettingsControl()
        {
            InitializeComponent();
        }

        bool hasLoaded = false;
        public NumericSettingsControl(zvsLocalDBEntities context, plugin_settings plugin_setting)
        {
            InitializeComponent();

            this.context = context;
            this.plugin_setting = plugin_setting;

            if (plugin_setting != null)
            {
                this.Label.Text = plugin_setting.name;
                this.TextBox.Text = plugin_setting.friendly_name;
                this.Details.Text = plugin_setting.description;

                if (!String.IsNullOrEmpty(plugin_setting.value))
                {
                    this.TextBox.Text = plugin_setting.value;
                }
            }
            hasLoaded = true;
        }
               
        public bool hasChanged
        {
            get
            {
                return _haschanged;
            }
        }

        public void SaveToContext()
        {            
            decimal number = 0;
            
            if (plugin_setting != null &&
                decimal.TryParse(this.TextBox.Text, out number) &&
                number > MinValue &&
                number < MaxValue)
            {
                if(ForceWholeNumber)
                    plugin_setting.value = ((int)number).ToString();
                else
                    plugin_setting.value = number.ToString();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (hasLoaded)
                _haschanged = true;
        }
    }
}