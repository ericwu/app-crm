﻿using System;
using Xamarin.Forms;
using XamarinCRM.Statics;
using XamarinCRM.Layouts;
using XamarinCRM.Converters;
using XamarinCRM.Views.Base;

namespace XamarinCRM.Cells
{
    public class OrderListItemCell : ViewCell
    {
        public Label PrimaryLabel { get; private set; }

        public Label SecondaryLabel { get; private set; }

        public Label TernaryLabel { get; private set; }

        public OrderListItemCell()
        {
            #region primary label
            PrimaryLabel = new Label()
            {
                TextColor = Device.OnPlatform(Color.Black, Color.White, Color.White),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)) * 1.2,
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.End,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            PrimaryLabel.SetBinding(
                Label.TextProperty,
                new Binding("Item"));
            #endregion

            #region secondary label
            SecondaryLabel = new Label()
            {
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                XAlign = TextAlignment.Start,
                YAlign = TextAlignment.Start,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            SecondaryLabel.SetBinding(
                Label.TextProperty,
                "DueDate", 
                converter: new ShortDatePatternConverter());
            #endregion

            #region ternary label
            TernaryLabel = new Label()
                {
                    TextColor = Color.Gray,
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    XAlign = TextAlignment.End,
                    YAlign = TextAlignment.Start,
                };

            TernaryLabel.SetBinding(
                Label.TextProperty,
                "Price", 
                converter: new CurrencyIntegerConverter());
            #endregion

            var contentView = new ContentView();

            contentView.Padding = new Thickness(20, 0);

            BoxView bottomBorder = new BoxView() { BackgroundColor = Palette._014, HeightRequest = 1 };

            RelativeLayout labelsRelativeLayout = new RelativeLayout() { HeightRequest = Sizes.LargeRowHeight };

            labelsRelativeLayout.Children.Add(
                view: PrimaryLabel,
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            labelsRelativeLayout.Children.Add(
                view: SecondaryLabel,
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            labelsRelativeLayout.Children.Add(
                view: TernaryLabel,
                xConstraint: Constraint.RelativeToView(SecondaryLabel, (parent, view) => view.Width),
                yConstraint: Constraint.RelativeToParent(parent => parent.Height / 2),
                widthConstraint: Constraint.RelativeToParent(parent => parent.Width / 2),
                heightConstraint: Constraint.RelativeToParent(parent => parent.Height / 2));

            contentView.Content = labelsRelativeLayout;

            View = new ContentViewWithBottomBorder() { Content = contentView };
        }
    }
}
