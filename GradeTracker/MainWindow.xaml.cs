using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.IO;

namespace GradeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class Assignment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Data:
        private string _name;
        private string _category;       //This could be changed to the actually class Category
        private int _score;
        private int _maxScore;
        private double _totalPercent;

        //Ctor
        public Assignment(string assignmentName, string category, int score, int maxScore, double totalPercent)
        {
            _name = assignmentName;
            _category = category;
            _score = score;
            _maxScore = maxScore;
            _totalPercent = Math.Round(totalPercent, 2, MidpointRounding.ToEven);
        }

        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        //Setters and getters:
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Notify("Name");
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                Notify("Category");
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                Notify("Score");
            }
        }

        public int MaxScore
        {
            get { return _maxScore; }
            set
            {
                _maxScore = value;
                Notify("MaxScore");
            }
        }

        public double TotalPercent
        {
            get { return _totalPercent; }
            set
            {
                _totalPercent = Math.Round(value, 2, MidpointRounding.ToEven);
                Notify("TotalPercent");
            }
        }
    }

    public class Category : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Data:
        private ObservableCollection<Assignment> _assignments;
        private string _name;
        private int _weight;
        private double _totalCategoryScore;

        public Category(string name, int weight)
        {
            _name = name;
            _weight = weight;
            _assignments = new ObservableCollection<Assignment>();
            _totalCategoryScore = 0;
        }

        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void addAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
        }

        public void removeAssignment(Assignment assignment)
        {
            _assignments.Remove(assignment);
        }

        //Setters and getters:
        public ObservableCollection<Assignment> Assignments
        {
            get { return _assignments; }
            set
            {
                _assignments = value;
                Notify("Assignments");
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; Notify("Name"); }
        }

        public double _TotalCategoryScore
        {
            get { return _totalCategoryScore; }
            set { _totalCategoryScore = value; Notify("_TotalCategoryScore"); }
        }

        public int _Weight
        {
            get { return _weight; }
            set { _weight = value; Notify("_Weight"); }
        }

        public Assignment findAssignmentByName(string name)
        {
            for (int i = 0; i < _assignments.Count; ++i)
            {
                if (_assignments[i].Name == name)
                {
                    return _assignments[i];
                }
            }

            Console.WriteLine("ERROR couldn't find that assignment by name");
            return null;
        }

        public void updateCategoryScores()
        {
            double score = 0.0;
            double maxScore = 0.0;

            for (int i = 0; i < _assignments.Count; ++i)
            {
                score += Assignments[i].Score;
                maxScore += Assignments[i].MaxScore;
            }

            _TotalCategoryScore = (score / maxScore) * 100;
        }
    }

    public class course : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private int totalScorePercent;
        public ObservableCollection<Category> categories;

        public course() : this("defaultName", 0)
        {
        }


        public course(string courseName, int totalScore)
        {
            this.name = courseName;
            this.totalScorePercent = totalScore;
            categories = new ObservableCollection<Category>();
        }

        public Category findCategoryByName(string name)
        {
            for (int i = 0; i < categories.Count; ++i)
            {
                if (categories[i].Name == name)
                {
                    return categories[i];
                }
            }
            Console.WriteLine("ERROR: Could not find that category!");
            return null;
        }

        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
            }
        }

        public int TotalScorePercent
        {
            get { return totalScorePercent; }
            set
            {
                totalScorePercent = value;
                Notify("TotalScorePercent");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                Notify("Categories");
            }
        }

        public void addCategory(Category newCategory)
        {
            categories.Add(newCategory);
            Notify("Categories");
        }

        public void deleteCategory(Category categoryToDelete)
        {
            categories.Remove(categoryToDelete);
            Notify("Categories");
        }

        public double getCourseFinalScore()
        {
            double TotalScore = 0.0;
            for (int i = 0; i < categories.Count; ++i)
            {
                double totalAssignmentPoints = 0.0;
                double totalAssignmentPointsEarned = 0.0;

                for (int j = 0; j < categories[i].Assignments.Count; ++j)
                {
                    totalAssignmentPoints += categories[i].Assignments[j].MaxScore;
                    totalAssignmentPointsEarned += categories[i].Assignments[j].Score;
                }

                TotalScore += (totalAssignmentPointsEarned / totalAssignmentPoints) * categories[i]._Weight;
            }

            return TotalScore;
        }

        public void updateAllCategoryScores()
        {
            for (int i = 0; i < categories.Count; ++i)
            {
                categories[i].updateCategoryScores();
            }
        }
    }

    public class courseList : ObservableCollection<course>
    {
    }
    public class categoryList : ObservableCollection<Category>
    {
    }

    //public class TrashButton : Button
    //{
    //    public static int NextPos = 0;
    //    private int _position;

    //    public int Position
    //    {
    //        get { return _position; }
    //        set { _position = value; }
    //    }

    //    public TrashButton() : base()
    //    {
    //        _position = NextPos;
    //        NextPos++;
    //    }
    //}

    public partial class MainWindow : Window
    {
        //Data members:
        courseList _courses;
        int IndexOfSelectedCourse;
        public MainWindow()
        {
            InitializeComponent();
            _courses = new courseList();
            listOfCoursesBox.ItemsSource = _courses;
        }

        public MainWindow(courseList courses)
        {
            InitializeComponent();
            _courses = courses;
            listOfCoursesBox.ItemsSource = _courses;
        }

        public void addCourseToList(String courseName)
        {
            _courses.Add(new course(courseName, 0));
        }

        private void nextBtn(object sender, RoutedEventArgs e)
        {
            dynamic selectedListCourse = listOfCoursesBox.SelectedItem;

            IndexOfSelectedCourse = _courses.IndexOf(selectedListCourse);
            //Pass the CourseDetailsWin the _courses
            if (IndexOfSelectedCourse >= 0)
            {
                CourseDetailsWin courseDetailsWin = new CourseDetailsWin(_courses, IndexOfSelectedCourse);
                App.Current.MainWindow = courseDetailsWin;
                this.Close();
                courseDetailsWin.Show();
            }

        }

        private void AddCourseBtn(object sender, RoutedEventArgs e)
        {
            CourseAddWin crsAddWin = new CourseAddWin(this, _courses);
            crsAddWin.Owner = this;
            crsAddWin.ShowDialog();
        }

        private void SelectedChanged(object sender, RoutedEventArgs e)
        {
        }

        private void ExitProgram(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = this;
            helpWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResourceDictionary skin1 = new ResourceDictionary();
            skin1.Source = new Uri("Style1.xaml", UriKind.Relative);
            Collection<ResourceDictionary> appMergedDictionaries =
                Application.Current.Resources.MergedDictionaries;
            // Remove the old skins
            if (appMergedDictionaries.Count != 0)
            {
                appMergedDictionaries.Remove(appMergedDictionaries[0]);
            }
            // Add the new skin
            appMergedDictionaries.Add(skin1);
        }

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            dynamic selectedListCourse = listOfCoursesBox.SelectedItem;

            IndexOfSelectedCourse = _courses.IndexOf(selectedListCourse);
            //Pass the CourseDetailsWin the _courses
            if (IndexOfSelectedCourse >= 0)
            {
                _courses.RemoveAt(IndexOfSelectedCourse);
            }

            NextBtn.IsEnabled = false;
        }

        private void ListOfCoursesBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NextBtn.IsEnabled = true;
        }

        private void OpenAbout(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }
    }
}
