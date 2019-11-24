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
    /// Interaction logic for AssignmentAddWin.xaml
    /// </summary>
    public partial class AssignmentAddWin : Window
    {
        private CourseDetailsWin mainWindow; //Used to access the list of courses in main window
        private courseList _courses;
        private int _selectedCourseIndex;
        private int _selectedCategoryIndex;
        private readonly int indexOfSelectedCategory;

        public AssignmentAddWin(CourseDetailsWin mainWin, courseList courses, int selectedCourseIndex, int selectedCategoryIndex)
        {
            InitializeComponent();
            mainWindow = mainWin;
            _courses = courses;
            _selectedCourseIndex = selectedCourseIndex;
            _selectedCategoryIndex = selectedCategoryIndex;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            double num1 = 0, num2 = 0;
            if (NameTextBox.Text != "" && double.TryParse((ScoreTextBox.Text), out num1) && double.TryParse((MaxScoreTextBox.Text), out num2) && indexOfSelectedCategory > -1)
            {
                Assignment newAssigment = new Assignment(NameTextBox.Text, _courses[_selectedCourseIndex].categories[_selectedCategoryIndex].Name, Int32.Parse(ScoreTextBox.Text), Int32.Parse(MaxScoreTextBox.Text), (Double.Parse(ScoreTextBox.Text) / Double.Parse(MaxScoreTextBox.Text)) * 100);
                _courses[_selectedCourseIndex].categories[_selectedCategoryIndex].addAssignment(newAssigment);
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
