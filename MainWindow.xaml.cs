using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using DataStructures.View;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataStructures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly Tree _tree = new Tree();
        private readonly Queue _queue = new Queue();
        private readonly LinkedList _list = new LinkedList();
        private readonly Stack _stack = new Stack();


        private void ClearCanvas(Canvas canvas)
        {
            canvas.Children.Clear();
        }

        #region List functions

        private void CreateListElement(int numElement)
        {
            var el = new ListElement { Data = _list.GetNodeAt(numElement).Data };
            Canvas.SetLeft(el, -60);
            canvas1.Children.Add(el);
            var duration = new Duration(TimeSpan.FromSeconds(0.1 * numElement));
            var da1 = new DoubleAnimation(numElement * 58, duration) { DecelerationRatio = 0.5 };
            el.BeginAnimation(Canvas.LeftProperty, da1);
        }

        private void RemoveListElement(int numElement)
        {
            canvas1.Children.RemoveAt(numElement);

            for (int i = numElement; i < canvas1.Children.Count; i++)
            {
                var duration = new Duration(TimeSpan.FromSeconds(0.2));
                var da1 = new DoubleAnimation
                {
                    Duration = duration,
                    By = -58
                };
                canvas1.Children[i].BeginAnimation(Canvas.LeftProperty, da1);
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (ItemList.Text != "")
            {
                int numElement = _list.Insert(ItemList.Text);
                UpdateList();
                CreateListElement(numElement - 1);
                ItemList.Focus();
                ItemList.SelectAll();
                ItemList.Text = "";
            }
        }

        private void UpdateList()
        {
            ClearSearch();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ItemList.Text != "")
            {
                int numRemove = _list.Find(ItemList.Text);
                if (numRemove >= 0)
                {
                    RemoveListElement(numRemove);
                    _list.Remove(ItemList.Text);
                }
                UpdateList();
            }
            ItemList.Focus();
            ItemList.SelectAll();
            ItemList.Text = "";
        }

        private void ClearSearch()
        {
            for (int i = 0; i < canvas1.Children.Count; i++)
                ((ListElement)canvas1.Children[i]).Fill = Brushes.LightSalmon;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (ItemList.Text != "")
            {
                int numProcura = _list.Find(ItemList.Text);
                if (numProcura >= 0)
                {
                    for (int i = 0; i < _list.Count; i++)
                    {
                        ((ListElement)canvas1.Children[i]).Fill = i == numProcura ? Brushes.Red : Brushes.LightSalmon;
                    }
                }
                else
                {
                    ClearSearch();
                    MessageBox.Show("Not found the element", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            ItemList.Focus();
            ItemList.SelectAll();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            _list.Clear();
            ClearCanvas(canvas1);
            ItemList.Focus();
            ItemList.SelectAll();
        }

        #endregion

        #region Stack functions

        private void CreateStackElement(int numElement)
        {
            var el = new StackElement { Data = ItemStack.Text };
            Canvas.SetTop(el, 0);
            Canvas.SetLeft(el, canvas2.ActualWidth / 2 - 40);
            canvas2.Children.Add(el);
            var duration = new Duration(TimeSpan.FromSeconds(0.2));
            var da1 = new DoubleAnimation(canvas2.ActualHeight - 20 * numElement - 50, duration);
            da1.DecelerationRatio = 1;
            el.BeginAnimation(Canvas.TopProperty, da1);
        }

        private void RemoveStackElement(int numElement)
        {
            var duration = new Duration(TimeSpan.FromSeconds(0.2));
            var da1 = new DoubleAnimation(0, duration) { AccelerationRatio = 0.5 };
            da1.Completed += da1_Completed;
            canvas2.Children[numElement].BeginAnimation(Canvas.TopProperty, da1);
        }

        private void da1_Completed(object sender, EventArgs e)
        {
            canvas2.Children.RemoveAt(canvas2.Children.Count - 1);
        }

        private void btnPush_Click(object sender, RoutedEventArgs e)
        {
            if (ItemStack.Text != "")
            {
                _stack.Push(ItemStack.Text);
                int numItens = _stack.Count;
                CreateStackElement(numItens - 1);
            }
            ItemStack.Focus();
            ItemStack.SelectAll();
        }

        private void btnPop_Click(object sender, RoutedEventArgs e)
        {
            if (_stack.Count > 0)
            {
                _stack.Pop();
                RemoveStackElement(_stack.Count);
            }
            else
                MessageBox.Show("The stack is empty", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            ItemStack.Focus();
            ItemStack.SelectAll();
        }

        private void btnPeek_Click(object sender, RoutedEventArgs e)
        {
            if (_stack.Count > 0)
            {
                string item = _stack.Peek();
                MessageBox.Show(string.Format("{0} on top of stack\nCount of stack items: {1}", item,
                                              _stack.Count));
            }
            else
                MessageBox.Show("The stack is empty", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            ItemStack.Focus();
            ItemStack.SelectAll();
        }

        private void btnClearStack_Click(object sender, RoutedEventArgs e)
        {
            _stack.Clear();
            ClearCanvas(canvas2);
            ItemStack.Focus();
            ItemStack.SelectAll();
        }

        #endregion

        #region Queue functions

        private void CriaElementoFila(int numElement)
        {
            var el = new QueueElement();
            el.Data = ItemQueue.Text;
            Canvas.SetLeft(el, canvas3.ActualWidth + 60);
            canvas3.Children.Add(el);
            var duration = new Duration(TimeSpan.FromSeconds(0.2));
            var da1 = new DoubleAnimation(numElement * 58, duration);
            da1.DecelerationRatio = 1;
            el.BeginAnimation(Canvas.LeftProperty, da1);
        }

        private void RemoveElementoFila()
        {
            var duration = new Duration(TimeSpan.FromSeconds(0.2));
            canvas3.Children.RemoveAt(0);
            for (int i = 0; i < canvas3.Children.Count; i++)
            {
                var da1 = new DoubleAnimation();
                da1.Duration = duration;
                da1.By = -58;
                canvas3.Children[i].BeginAnimation(Canvas.LeftProperty, da1);
            }
        }

        private void btnEnqueue_Click(object sender, RoutedEventArgs e)
        {
            if (ItemQueue.Text != "")
            {
                _queue.Enqueue(ItemQueue.Text);
                CriaElementoFila(_queue.Count - 1);
            }
            ItemQueue.Focus();
            ItemQueue.SelectAll();
        }

        private void btnDequeue_Click(object sender, RoutedEventArgs e)
        {
            if (_queue.Count > 0)
            {
                _queue.Dequeue();
                RemoveElementoFila();
            }
            else
                MessageBox.Show("There are no items in the queue", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            ItemQueue.Focus();
            ItemQueue.SelectAll();
        }

        private void btnClearQueue_Click(object sender, RoutedEventArgs e)
        {
            _queue.Clear();
            ClearCanvas(canvas3);
            ItemQueue.Focus();
            ItemQueue.SelectAll();
        }

        #endregion

        #region Tree functions

        private BitmapImage CreateImage(string imageName)
        {
            return new BitmapImage(new Uri("\\images\\" + imageName, UriKind.Relative));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (ItemTree.Text != "")
            {
                try
                {
                    _tree.Add(ItemTree.Text);
                    RedrawTree();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ItemTree.Focus();
            ItemTree.SelectAll();
            ItemTree.Text = "";
        }

        private void btnRemoveTreeItem_Click(object sender, RoutedEventArgs e)
        {

            if (ItemTree.Text != "")
            {
                _tree.Remove(ItemTree.Text);
                RedrawTree();
            }
            ItemTree.Focus();
            ItemTree.SelectAll();
            ItemTree.Text = "";
        }

        private void DrawNode(string data, double startX, double endX, double posY)
        {
            var currentData = ItemTree.Text;
            TreeElement el = new TreeElement
            {
                Data = data,
                Image = CreateImage(currentData == data ? "nodetree2.png" : "nodetree1.png")
            };
            Canvas.SetLeft(el, (endX + startX) / 2 - 15);
            Canvas.SetTop(el, posY);
            canvas4.Children.Add(el);
        }

        private void RedrawTree()
        {
            ClearCanvas(canvas4);
            if (_tree.Count != 0)
            {
                var startPos = 0.0;
                var endPos = canvas4.ActualWidth;
                var posY = 5;
                var node = _tree.Root;
                VisitNode(node, startPos, endPos, posY);
            }
        }

        private void VisitNode(TreeNode node, double startPos, double endPos, int posY)
        {
            DrawNode(node.Data, startPos, endPos, posY);
            if (node.Left != null)
            {
                var newEnd = (startPos + endPos) / 2;
                DrawLine((endPos + startPos) / 2, posY + 15, (newEnd + startPos) / 2, posY + 65);
                VisitNode(node.Left, startPos, newEnd, posY + 50);
            }
            if (node.Right != null)
            {
                var newStart = (endPos + startPos) / 2;
                DrawLine((endPos + startPos) / 2, posY + 15, (endPos + newStart) / 2, posY + 65);
                VisitNode(node.Right, newStart, endPos, posY + 50);
            }
        }

        private void DrawLine(double startX, double startY, double endX, double endY)
        {
            var line = new Line { X1 = startX, Y1 = startY, X2 = endX, Y2 = endY, Stroke = Brushes.LightBlue, StrokeThickness = 1 };
            Panel.SetZIndex(line, -1);
            canvas4.Children.Add(line);
        }

        private void btnSearchTreeItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemTree.Text != "")
                try
                {
                    if (!_tree.Exists(ItemTree.Text))
                    {
                        MessageBox.Show("Item does not exist", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    RedrawTree();
                    MessageBox.Show("The item you want to search have in the tree", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Item does not exist", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            ItemTree.Focus();
            ItemTree.SelectAll();

        }

        private void btnClearTree_Click(object sender, RoutedEventArgs e)
        {
            _tree.Clear();
            RedrawTree();
            ItemTree.Focus();
            ItemTree.SelectAll();
        }

        #endregion
    }
}
