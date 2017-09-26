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
using Hooks;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;

namespace ArcWardenAssistent
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents m_Events;
        public static List<string> AnyKeyList = new List<string>();
        private bool drug;
        private double tL;
        private double tT;

        public static event EventHandler<object> AnyKeyDown;
        public static event EventHandler<object> AnyKeyUp;
        public MainWindow()
        {
            InitializeComponent();
            //Hooks.KBDHook.InstallHook();
            SubscribeGlobal();
            AnyKeyDown += MainWindow_AnyKey;
            AnyKeyUp += MainWindow_AnyKeyUp;
        }

        private void MainWindow_AnyKeyUp(object sender, object e)
        {
            tbLog.Text = "";
            if (AnyKeyList.Count>0)
            {
                foreach (string item in AnyKeyList)
                {
                    tbLog.Text += item + " ";
                }
            }
          
        }

        private void MainWindow_AnyKey(object sender, object e)
        {
            tbLog.Text = "";

            foreach (string item in AnyKeyList)
            {
                tbLog.Text += item + " ";
            }
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;

            m_Events.MouseUp += M_Events_MouseUp;
            m_Events.MouseDown += M_Events_MouseDown;
            m_Events.KeyDown += M_Events_KeyDown;
            m_Events.KeyUp += M_Events_KeyUp;
            m_Events.MouseMove += M_Events_MouseMove;
        }

        private void M_Events_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(drug)
            {
                this.Top -= tT - e.Y;
                this.Left -= tL - e.X;
                
            }
                tT = e.Y;
                tL = e.X;
        }

        private void M_Events_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            AnyKeyList.Add(GetShortKeyName(e.Button.ToString()));
            AnyKeyDown(sender, GetShortKeyName(e.Button.ToString()));
        }

        private void M_Events_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
            AnyKeyList.Remove(GetShortKeyName(e.Button.ToString()));
            AnyKeyUp(sender, GetShortKeyName(e.Button.ToString()));
        }

        private void M_Events_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {            
            AnyKeyList.Remove(GetShortKeyName(e.KeyCode.ToString()));
            AnyKeyUp(sender, GetShortKeyName(e.KeyCode.ToString()));
        }

        private void M_Events_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
            {
            if (AnyKeyList.IndexOf(GetShortKeyName(e.KeyCode.ToString()))==-1) {
                AnyKeyList.Add(GetShortKeyName(e.KeyCode.ToString()));
                AnyKeyDown(sender, GetShortKeyName(e.KeyCode.ToString()));
            }
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            
            m_Events.KeyDown -= M_Events_KeyDown;
            m_Events.KeyUp -= M_Events_KeyUp;
            m_Events.MouseUp -= M_Events_MouseUp;
            m_Events.MouseDown -= M_Events_MouseDown;

            m_Events.MouseMove -= M_Events_MouseMove;

            m_Events.Dispose();
            m_Events = null;
        }
     

       

        private string GetShortKeyName(string v)
        {
            switch (v)
            {
                case "Left":
                    return "ML";
                case "Right":
                    return "MR";
                case "XButton1":
                    return "M4";
                case "XButton2":
                    return "M5";
                default:
                    return v;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Hooks.KBDHook.UnInstallHook();
            Unsubscribe();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drug = true;
                    }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            drug = false;
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {

            drug = false;
        }
        

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void BHide_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BMinim_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
