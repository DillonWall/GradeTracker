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
    /// Interaction logic for courseDetailsWin.xaml
    /// </summary>
    public partial class CourseDetailsWin : Window
    {
        private courseList _courses;
        private int _selectedCourse;
        private int _selectedCategory;
        Assignment selectedAssignment;
        private int indexOfSelectedCategory =-1;
        public CourseDetailsWin(courseList courses, int selectedCourse)
        {
            InitializeComponent();
            _courses = courses;
            _selectedCourse = selectedCourse;
            CourseDetailsTabControl.ItemsSource = _courses[selectedCourse].categories;
        }

        private void AddAssignmentClick(object sender, RoutedEventArgs routedEventArgs)
        {
            AssignmentAddWin assignmentAddWin = new AssignmentAddWin(this, _courses, _selectedCourse, _selectedCategory);
            assignmentAddWin.Owner = this;
            assignmentAddWin.ShowDialog();
        }

        private void HomeClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_courses);
            App.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }

        //private void CategoryTab_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //}

        //Temporary for demo (should be sender based and generic)
        private void OpenCategoryEdit_Click(object sender, RoutedEventArgs e)
        {
            if (indexOfSelectedCategory >= 0)
            {
                CategoryEditWin categoryEditWin = new CategoryEditWin(this, _courses, _selectedCourse, _courses[_selectedCourse].categories[_selectedCategory], _selectedCategory);
                categoryEditWin.Owner = this;
                categoryEditWin.ShowDialog();
            }
        }

        private void OpenAssignmentEdit_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(CourseDetailsAssignmentsListBox.SelectedItem.ToString());
            //var ItemsInsideOfSelectedItem = TreeHelper.GetChildObjects((DependencyObject)CourseDetailsAssignmentsListBox.SelectedItem);
            //Console.WriteLine(ItemsInsideOfSelectedItem.ToList()[0].ToString()); 
            //_courses[_selectedCourse].categories[_selectedCategory].findAssignmentByName((Category)CourseDetailsAssignmentsListBox.SelectedItem)
            if (selectedAssignment != null)
            {
                AssignmentEditWin assignmentEditWin = new AssignmentEditWin(this, _courses, _selectedCourse, _selectedCategory, (CourseDetailsAssignmentsListBox.SelectedItem as Assignment));
                assignmentEditWin.Owner = this;
                assignmentEditWin.ShowDialog();
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            _courses[_selectedCourse].deleteCategory(_courses[_selectedCourse].categories[_selectedCategory]);
            if (CourseDetailsTabControl.Items.Count == 0)
            {
                AddBtn.IsEnabled = false;
                CategoryEditBtn.IsEnabled = false;
                CategoryDeleteBtn.IsEnabled = false;
                CourseDetailsAssignmentsListBox.ItemsSource = null;
            }
        }

        private void OpenGradeOverviewClick(object sender, RoutedEventArgs e)
        {
            GradeOverviewWin categoryAddWin = new GradeOverviewWin(_courses, _selectedCourse);
            categoryAddWin.Owner = this;
            categoryAddWin.ShowDialog();
        }
        private void OpenCategoryAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryAddWin categoryAddWin = new CategoryAddWin(this, _courses, _selectedCourse);
            categoryAddWin.Owner = this;
            categoryAddWin.ShowDialog();
        }

        //Temporary for demo (should be sender based and generic)
        private void ListBoxItem_OnSelected(object sender, RoutedEventArgs e)
        {

            selectedAssignment = (Assignment)((ListBox) sender).SelectedItem;//_courses[_selectedCourse].categories[_selectedCategory].findAssignmentByName((sender as TextBlock).Text);

            if (selectedAssignment != null)
            {
                _courses[_selectedCourse].categories[_selectedCategory].Assignments.IndexOf(selectedAssignment);

                Score_Lbl.Content = selectedAssignment.Score;
                MaxScore_Lbl.Content = selectedAssignment.MaxScore;
                Percent_Lbl.Content = selectedAssignment.TotalPercent + "%";
                Category_Lb1.Content = selectedAssignment.Category;

                EditBtn.IsEnabled = true;
            }
            else
            {
                Score_Lbl.Content = "";
                MaxScore_Lbl.Content = "";
                Percent_Lbl.Content = "";
                Category_Lb1.Content = "";

                EditBtn.IsEnabled = false;
            }
        }

        private void CourseDetailsTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl)
            {
                //var tabControlChildren = TreeHelper.GetChildObjects((DependencyObject)sender);
                //var grid = TreeHelper.GetChildObjects((DependencyObject)labelInsideOfTheTabItem);
                //var label = TreeHelper.GetChildObjects((DependencyObject)grid);
                //var gridChildren = TreeHelper.GetChildObjects(tabControlChildren.ToArray()[0]);
                //Console.WriteLine(gridChildren.ToList()[0].ToString()); 
                //string str = ((Category)(((TabControl)sender).Items.OfType<Category>().ElementAt(0))).Name.TrimEnd('\r', '\n');
                //str = str.Remove(0, 31);
                //Category selectedCategory = ((TabControl) sender).Items.OfType<Category>().ElementAt(0);//_courses[_selectedCourse].findCategoryByName(str);
                indexOfSelectedCategory = ((TabControl) sender).SelectedIndex; //_courses[_selectedCourse].categories.IndexOf(selectedCategory);
                if (indexOfSelectedCategory != -1)
                {
                    _selectedCategory = indexOfSelectedCategory;
                    CourseDetailsAssignmentsListBox.ItemsSource =
                        _courses[_selectedCourse].categories[indexOfSelectedCategory].Assignments;
                }
            }
        }

        public void SelectLastCategory()
        {
            CourseDetailsTabControl.SelectedIndex = CourseDetailsTabControl.Items.Count - 1;
            CategoryEditBtn.IsEnabled = true;
            CategoryDeleteBtn.IsEnabled = true;
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

        private void OpenAbout(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowDialog();
        }
    }


    /// <summary>
    /// Helper methods for UI-related tasks.
    /// Got this helpful class online as a way to help traverse the visual tree! - L
    /// </summary>
    public static class TreeHelper
    {
        #region find parent

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                //use recursion to proceed with next level
                return TryFindParent<T>(parentObject);
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            //handle content elements separately
            ContentElement contentElement = child as ContentElement;
            if (contentElement != null)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                FrameworkContentElement fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            FrameworkElement frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        #endregion

        #region find children

        /// <summary>
        /// Analyzes both visual and logical tree in order to find all elements of a given
        /// type that are descendants of the <paramref name="source"/> item.
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the search. If the
        /// source is already of the requested type, it will not be included in the result.</param>
        /// <returns>All descendants of <paramref name="source"/> that match the requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source != null)
            {
                var childs = GetChildObjects(source);
                foreach (DependencyObject child in childs)
                {
                    //analyze if children match the requested type
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    //recurse tree
                    foreach (T descendant in FindChildren<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }


        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Keep in mind that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent)
        {
            if (parent == null) yield break;

            if (parent is ContentElement || parent is FrameworkElement)
            {
                //use the logical tree for content / framework elements
                foreach (object obj in LogicalTreeHelper.GetChildren(parent))
                {
                    var depObj = obj as DependencyObject;
                    if (depObj != null) yield return (DependencyObject)obj;
                }
            }
            else
            {
                //use the visual tree per default
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        #endregion

        #region find from point

        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position. 
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, Point point)
            where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point) as DependencyObject;

            if (element == null) return null;
            else if (element is T) return (T)element;
            else return TryFindParent<T>(element);
        }

        #endregion
    }
}