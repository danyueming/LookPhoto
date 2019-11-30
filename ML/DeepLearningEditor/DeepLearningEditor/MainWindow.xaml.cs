using Microsoft.Win32;
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

namespace DeepLearningEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM mainWindowVM;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowVM = new MainWindowVM(this);
            this.DataContext = mainWindowVM;
            imagListBox.SelectionChanged += mainWindowVM.SelectedIndexChanged;
            imagListBox.PreviewMouseWheel += mainWindowVM.PreviewMouseWheel;
            DownBtn.Click += mainWindowVM.DownClick;
            UpBtn.Click += mainWindowVM.UpClick;
        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowVM.OpenItem_Click(sender, e);
        }

        private void ImagListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }

    /// <summary>
    /// 图片信息类
    /// </summary>
    public class PictureInfo
    {
        /// <summary>
        /// 图片源
        /// </summary>
        public ImageSource CustomSource { get; set; }

        /// <summary>
        /// 图片信息
        /// </summary>
        public string Info { get; set; }
    }

}
