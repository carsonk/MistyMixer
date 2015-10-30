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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

using MistyMixer.Models;
using System.Collections.ObjectModel;

namespace MistyMixer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Cue> _cueList = new ObservableCollection<Cue>();

        private Point _dragStartPoint;

        public MainWindow()
        {
            InitializeComponent();

            _cueList.Add( new SoundCue { Title = "Sound Cue 1" } );
            _cueList.Add( new SoundCue { Title = "Sound Cue 2" } );
            _cueList.Add( new SoundCue { Title = "Sound Cue 3" } );
            
            cueListView.ItemsSource = _cueList;

            cueListView.PreviewMouseMove += CueList_PreviewMouseMove;

            Style itemContainerStyle = new Style(typeof(ListBoxItem));

            itemContainerStyle.Setters.Add(
                new Setter(
                    ListBoxItem.AllowDropProperty, 
                    true
                )
            );

            itemContainerStyle.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent, 
                    new MouseButtonEventHandler(CueList_PreviewMouseLeftButtonDown)
                )
            );

            itemContainerStyle.Setters.Add(
                new EventSetter(
                    ListBoxItem.DropEvent, 
                    new DragEventHandler(CueList_Drop)
                )
            );

            cueListView.ItemContainerStyle = itemContainerStyle;
        }

        private void CueList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void CueList_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;

            if(e.LeftButton == MouseButtonState.Pressed 
                && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListBox sendingListBox = sender as ListBox;
                ListBoxItem sendingListBoxItem = VisualTreeSearchHelper.FindVisualParent<ListBoxItem>((DependencyObject)e.OriginalSource);

                if(sendingListBoxItem != null)
                {
                    DragDrop.DoDragDrop(sendingListBoxItem, sendingListBoxItem.DataContext, DragDropEffects.Move);
                }
            }
        }

        private void CueList_Drop(object sender, DragEventArgs e)
        {
            if(sender is ListBoxItem)
            {
                SoundCue source = e.Data.GetData(typeof(SoundCue)) as SoundCue;
                SoundCue target = ((ListBoxItem)sender).DataContext as SoundCue;

                int sourceIndex = cueListView.Items.IndexOf(source);
                int targetIndex = cueListView.Items.IndexOf(target);

                if(sourceIndex < targetIndex)
                {
                    _cueList.Insert(targetIndex + 1, source);
                    _cueList.RemoveAt(sourceIndex);
                }
                else
                {
                    int removeIndex = sourceIndex + 1;
                    if(_cueList.Count + 1 > removeIndex)
                    {
                        _cueList.Insert(targetIndex, source);
                        _cueList.RemoveAt(removeIndex);
                    }
                }
            }
        }
    }
}
