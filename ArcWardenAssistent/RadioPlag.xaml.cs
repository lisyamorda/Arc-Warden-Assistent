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

namespace ArcWardenAssistent
{
    /// <summary>
    /// Логика взаимодействия для RadioPlag.xaml
    /// </summary>
    public partial class RadioPlag : UserControl
    {
        bool pLst = false;
        public RadioPlag()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pLst = !pLst;
            bHidden.Content = (pLst) ? "6" : "5";
            playList.Visibility = (pLst) ? Visibility.Visible:Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
