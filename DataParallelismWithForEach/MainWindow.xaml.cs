using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DataParallelismWithForEach
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cancelToken = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelToken.Cancel();
            cancelToken = new CancellationTokenSource();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() => ProcessFiles());
        }

        private void ProcessFiles()
        {
            ParallelOptions parOpts = new ParallelOptions
            {
                CancellationToken = cancelToken.Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            string[] files = Directory.GetFiles(@".\Images", "*.jpg",
                SearchOption.AllDirectories);
            string newDir = @".\ModifiedImages";
            Directory.CreateDirectory(newDir);

            try
            {
                Parallel.ForEach(files, parOpts, (currentFile) =>
                {
                    string fileName = Path.GetFileName(currentFile);

                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        Thread.Sleep(2000); // if you have an SSD drive
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, fileName));

                        this.Dispatcher.Invoke(() =>
                        {
                            this.Title = $"Processing {fileName} on thread {Thread.CurrentThread.ManagedThreadId}";
                        });
                    }
                });
                this.Dispatcher.Invoke(() => this.Title = "Done!");
            }
            catch (OperationCanceledException ex)
            {
                this.Dispatcher.Invoke(() => this.Title = ex.Message);
            }
        }
    }
}
