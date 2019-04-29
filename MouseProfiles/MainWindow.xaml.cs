using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.ObjectModel;
using System.IO;
using MouseProfiles.ViewModels;

namespace MouseProfiles
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MouseProfileViewModel MouseProfileViewModel { get; set; }
        public MainWindow()
        {
            MouseProfileViewModel = new MouseProfileViewModel();
            InitializeComponent();

            SelectProfile.SelectedIndex = 0;

            NotifyIcon ni = new NotifyIcon
            {
                Icon = new Icon("Main.ico"),
                Visible = true
            };
            ni.Click += NotifyIcon_Click;
            ni.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void UpdateTargets()
        {
            ButtonSwapCheck.GetBindingExpression(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty).UpdateTarget();
            MouseSpeedSlider.GetBindingExpression(System.Windows.Controls.Primitives.RangeBase.ValueProperty).UpdateTarget();
            WheelLinesSlider.GetBindingExpression(System.Windows.Controls.Primitives.RangeBase.ValueProperty).UpdateTarget();
            DoubleClickTimeSlider.GetBindingExpression(System.Windows.Controls.Primitives.RangeBase.ValueProperty).UpdateTarget();
            ProfileNameTextBox.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateTarget();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ((NotifyIcon)sender).Dispose();
            App.Current.Shutdown();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
                return;
            }
            this.WindowState = WindowState.Normal;
            this.Top = System.Windows.Forms.Control.MousePosition.Y - this.Height;
            this.Left = System.Windows.Forms.Control.MousePosition.X - this.Width;
            this.Focus();
        }
        public static System.Windows.Point GetMousePositionWindowsForms()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
        }

        private void SelectProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectindex = ((System.Windows.Controls.ComboBox)sender).SelectedIndex;
            if (selectindex == -1)
            {
                selectindex = 0;
                ((System.Windows.Controls.ComboBox)sender).SelectedIndex = 0;
            }
            MouseProfileViewModel.SelectedProfile = MouseProfileViewModel.Profiles[selectindex];
            IsEnabledChangeProfile(MouseProfileViewModel.SelectedProfile.Name != "Default");
            UpdateTargets();
        }

        private void IsEnabledChangeProfile(bool isEnabled)
        {
            if (isEnabled)
            {
                ButtonSwapCheck.IsEnabled = true;
                MouseSpeedSlider.IsEnabled = true;
                WheelLinesSlider.IsEnabled = true;
                DoubleClickTimeSlider.IsEnabled = true;
                DoubleClickTimeTextBox.IsEnabled = true;
                ProfileNameTextBox.IsEnabled = true;
                DeleteProfileButton.IsEnabled = true;
            }
            else
            {
                ButtonSwapCheck.IsEnabled = false;
                MouseSpeedSlider.IsEnabled = false;
                WheelLinesSlider.IsEnabled = false;
                DoubleClickTimeSlider.IsEnabled = false;
                DoubleClickTimeTextBox.IsEnabled = false;
                ProfileNameTextBox.IsEnabled = false;
                DeleteProfileButton.IsEnabled = false;
            }
        }

        private void ActivateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MouseProfileViewModel.ActivateSelectedProfile();
        }

        private void DoubleClickTimeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as System.Windows.Controls.TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !double.TryParse(fullText, out double val);
        }

        private void CreateProfile_Click(object sender, RoutedEventArgs e)
        {
            if (NewProfileNameTextBox.Text != string.Empty || NewProfileNameTextBox.Text != "Default")
            {
                MouseProfileViewModel.CreateNewProfile(NewProfileNameTextBox.Text);
                NewProfileNameTextBox.Text = string.Empty;
            }
        }

        private void DeleteProfile_Click(object sender, RoutedEventArgs e)
        {
            MouseProfileViewModel.DeleteSelectedProfile();
        }
    }
}
