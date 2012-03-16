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
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

namespace MyMVVM
{
  public static class Reflection
  {
    public static MemberInfo GetProperty( LambdaExpression propertyExpression )
    {
      var memberExpression = propertyExpression.Body as MemberExpression;

      if ( memberExpression == null || !(memberExpression.Expression is ParameterExpression) )
        throw new ArgumentException( "Property expression must be of the form 'x => x.Property'" );

      return memberExpression.Member;
    }

    public static MemberInfo[] GetProperties( LambdaExpression propertyExpression )
    {
      var properties = new List<MemberInfo>();

      var expression = propertyExpression.Body;
      while ( expression is MemberExpression )
      {
        var memberExpression = (MemberExpression)expression;

        properties.Insert( 0, memberExpression.Member );

        expression = memberExpression.Expression;
      }

      if ( !(expression is ParameterExpression) )
        throw new ArgumentException( "Property expression must be of the form 'x => x.Property1.Property2'" );

      return properties.ToArray();
    }
  }
}
