﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Data;

namespace FootballClient.UWP.Converters
{
    public class CaseSet : List<ICase>
    {
        public static readonly object UndefinedObject = new object();
    }

    public interface ICase
    {
        object Key { get; set; }
        object Value { get; set; }
        Type KeyType { get; set; }
    }

    public class Case : ICase
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public Type KeyType { get; set; }
    }

    public interface ICompositeConverter : IValueConverter
    {
        IValueConverter PostConverter { get; set; }
        object PostConverterParameter { get; set; }
    }

    public interface ISwitchConverter : IValueConverter
    {
        CaseSet Cases { get; }
        object Default { get; set; }
        bool TypeMode { get; set; }
    }
}
