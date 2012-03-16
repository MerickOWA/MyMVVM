using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MyMVVM
{
  public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
  {
    public void OnPropertyChanged<PropertyT>( Expression<Func<ViewModelBase, PropertyT>> property )
    {
      var handler = this.PropertyChanging;
      if ( handler != null )
        handler( this, new PropertyChangingEventArgs( Reflection.GetProperty( property ).Name ) );
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanging<PropertyT>( Expression<Func<ViewModelBase, PropertyT>> property )
    {
      var handler = this.PropertyChanging;
      if ( handler != null )
        handler( this, new PropertyChangingEventArgs( Reflection.GetProperty( property ).Name ) );
    }

    public event PropertyChangingEventHandler PropertyChanging;
  }

  public static class SetValueExtension
  {
    public static void SetValue<ViewModelT,PropertyT>( this ViewModelT viewModel, Expression<Func<ViewModelT,PropertyT>> property, PropertyT value, ref PropertyT field ) where ViewModelT : ViewModelBase
    {
      if ( !EqualityComparer<PropertyT>.Default.Equals( field, value ) )
      {
        viewModel.OnPropertyChanging( property );
        field = value;
        viewModel.OnPropertyChanged( property );
      }
    }
  }
}
