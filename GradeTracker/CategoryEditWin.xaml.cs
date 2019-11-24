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
    public partial class CategoryEditWin : Window
    {
        private CourseDetailsWin mainWindow; //Used to access the list of courses in main window
        private courseList _courses;
        private int _selectedCourseIndex;
        private int _selectedCategoryIndex;
        private Category _category;

        public CategoryEditWin(CourseDetailsWin mainWin, courseList courses, int selectCourseIndex, Category category, int selectedCategoryIndex)
        {
            InitializeComponent();
            mainWindow = mainWin;
            _courses = courses;
            _selectedCourseIndex = selectCourseIndex;
            NameTextBox.Text = category.Name;
            WeightTextBox.Text = category._Weight.ToString();
            _selectedCategoryIndex = selectedCategoryIndex;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            Category newCategory = new Category(NameTextBox.Text, Int32.Parse(WeightTextBox.Text));
            _courses[_selectedCourseIndex].categories[_selectedCategoryIndex] = newCategory;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
