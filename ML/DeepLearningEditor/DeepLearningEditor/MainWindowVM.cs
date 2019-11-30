using Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeepLearningEditor
{
    public class MainWindowVM : IBaseModel
    {
        private MainWindow mainWindow;
        public MainWindowVM()
        {

        }

        public MainWindowVM(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            LoadImageData();
        }

        private void LoadImageData()
        {
            PictureInfo pictureInfo1 = new PictureInfo();

            pictureInfo1.CustomSource = new BitmapImage(new Uri(@"/Picture/11.jpg", UriKind.Relative));
            pictureInfo1.Info = "first";
            imageList.Add(pictureInfo1);

            PictureInfo pictureInfo2 = new PictureInfo();
            pictureInfo2.CustomSource = new BitmapImage(new Uri(@"/Picture/12.jpg", UriKind.Relative));
            pictureInfo2.Info = "second";
            imageList.Add(pictureInfo2);
        }

        /// <summary>
        /// 图片列表
        /// </summary>
        private ObservableCollection<PictureInfo> imageList = new ObservableCollection<PictureInfo>();
        public ObservableCollection<PictureInfo> ImageList
        {
            get { return imageList; }
            set
            {

                NotifyChanged("ImageList");
                imageList = value;

            }
        }

        /// <summary>
        /// 当前选中的图片索引
        /// </summary>
        private int currentIndex = 0;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {

                NotifyChanged("CurrentIndex");
                currentIndex = value;

            }
        }


        /// <summary>
        /// 文件名称列表
        /// </summary>
        private List<string> fileList;

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "图片(*.jpg)|*.jpg|图片(*.png)|*.png|图片(*.bmp)|*.bmp|所有文件(*.*)|*.*";
            openFile.Multiselect = true;
            openFile.Title = "请选择文件";

            if (openFile.ShowDialog() == true)
            {
                try
                {
                    string[] filenames = openFile.FileNames;
                    fileList = filenames.ToList();
                    ReadImage(fileList);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.StackTrace, err.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        /// <summary>
        /// 从磁盘读取图片
        /// </summary>
        /// <param name="templist"></param>
        private void ReadImage(List<string> templist)
        {
            foreach (var item in templist)
            {
                byte[] b = ImageProcess.GetPictureData(item);
                BitmapImage myimg = ImageProcess.ByteArrayToBitmapImage(b);
                PictureInfo pictureInfo = new PictureInfo();
                pictureInfo.CustomSource = myimg;
                imageList.Add(pictureInfo);
            }
        }


        private int LastIndex = 0;
        /// <summary>
        /// ListBox的selectChange事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            double perWidth = 120;//ListItem的宽度
            var children = VisualTreeHelper.GetChild(mainWindow.imagListBox, 0);//Border
            ScrollViewer sv = VisualTreeHelper.GetChild(children, 0) as ScrollViewer;//ScrollViewer
            this.mainWindow.imagListBox.UpdateLayout();
            int tempInterver = 0;
            tempInterver = Math.Abs(CurrentIndex - LastIndex);//索引之间的间隔
            if (LastIndex > CurrentIndex)
            {
                sv.ScrollToHorizontalOffset(sv.HorizontalOffset - perWidth * tempInterver);
            }
            else
            {
                if ((CurrentIndex - 3) > 0)
                {
                    sv.ScrollToHorizontalOffset(sv.HorizontalOffset + perWidth * tempInterver);
                }
            }
            LastIndex = CurrentIndex;
        }

        /// <summary>
        /// 鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = null;
            ItemsControl depObj = (ItemsControl)sender;
            scrollViewer = FindVisualChild<ScrollViewer>(depObj);

            if (scrollViewer != null)
            {
                if (e.Delta > 0)
                {
                    scrollViewer.LineRight();
                }
                else
                {
                    scrollViewer.LineLeft();
                }
            }

            e.Handled = true;
        }

        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        /// <summary>
        /// 点击左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DownClick(object sender, RoutedEventArgs e)
        {
            if (CurrentIndex > 0)
            {
                mainWindow.imagListBox.SelectedIndex--;
            }

        }

        /// <summary>
        /// 点击右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpClick(object sender, RoutedEventArgs e)
        {
            if (CurrentIndex < imageList.Count)
            {
                mainWindow.imagListBox.SelectedIndex++;

            }

        }


    }

}
