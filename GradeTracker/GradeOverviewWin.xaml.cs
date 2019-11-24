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

namespace GradeTracker
{
    /// <summary>
    /// Interaction logic for GradeOverviewWin.xaml
    /// </summary>
    public partial class GradeOverviewWin : Window
    {
        //private MainWindow _mainWindow;
        private courseList _courses;
        private int _selectedCourseIndex;

        public GradeOverviewWin(courseList courses, int selectedCourse)
        {
            InitializeComponent();
            //_mainWindow = mainWindow;
            _courses = courses;
            _selectedCourseIndex = selectedCourse;
            FinalScoreLabel.Content = _courses[selectedCourse].getCourseFinalScore();
            _courses[selectedCourse].updateAllCategoryScores();
            GradeOverviewCategoryBreakdown.ItemsSource = _courses[selectedCourse].categories;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
