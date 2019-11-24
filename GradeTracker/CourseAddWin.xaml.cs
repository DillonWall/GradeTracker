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
using System.Windows.Shapes;
using System.IO;

namespace GradeTracker
{
    /// <summary>
    /// Interaction logic for courseAddWin.xaml
    /// </summary>
    public partial class CourseAddWin : Window
    {
        private MainWindow mainWindow; //Used to access the list of courses in main window
        private courseList _courses;

        public CourseAddWin(MainWindow mainWin, courseList courses)
        {
            InitializeComponent();
            mainWindow = mainWin;
            _courses = courses;
        }

        private void crsAddOKBtn(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("asdasdas {0}", crsAddTextBox.Text);
            _courses.Add(new course(crsAddTextBox.Text, 0));
            this.Close();
        }

        private void crsAddCancelBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}