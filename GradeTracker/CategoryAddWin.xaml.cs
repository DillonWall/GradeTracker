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
    /// Interaction logic for CategoryAddWin.xaml
    /// </summary>
    public partial class CategoryAddWin : Window
    {
        private CourseDetailsWin mainWindow; //Used to access the list of courses in main window
        private courseList _courses;
        private int _selectedCourseIndex;

        public CategoryAddWin(CourseDetailsWin mainWin, courseList courses, int selectCourseIndex)
        {
            InitializeComponent();
            mainWindow = mainWin;
            _courses = courses;
            _selectedCourseIndex = selectCourseIndex;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            double num = 0;
            
            if (NameTextBox.Text != "" && WeightTextBox.Text != "" && double.TryParse((WeightTextBox.Text), out num) && NumberCheck(Int32.Parse(WeightTextBox.Text)))
            {
                Category newCategory = new Category(NameTextBox.Text, Int32.Parse(WeightTextBox.Text));
                _courses[_selectedCourseIndex].addCategory(newCategory);
                mainWindow.AddBtn.IsEnabled = true;
                mainWindow.SelectLastCategory();
                this.Close();
            }

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool NumberCheck(int number)
        {
            bool result = false;
            if (number > 0 && number <= 100)
            {
                result = true;
            }
            return result;
        }

    }
}
