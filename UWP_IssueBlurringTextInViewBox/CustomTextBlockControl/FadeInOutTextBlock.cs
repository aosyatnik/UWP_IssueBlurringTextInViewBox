using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using UWP_IssueBlurringTextInViewBox.Animations;
using Windows.Foundation;
using Windows.UI.Xaml.Media;

namespace UWP_IssueBlurringTextInViewBox.CustomTextBlockControl
{
    /// <summary>
    /// FadeInOutTextBlock class implements the  BaseFadeInOutAnimationControl class and makes the text blocks that needs to be fade out and fade in for animation purpose reusable. 
    /// So where ever we need such text blocks instead of creating new text blocks and implementing the whole animation , we can use this class, assign all the dependency properties
    /// to set the text and styling.
    /// </summary>
    public class FadeInOutTextBlock : BaseFadeInOutAnimationControl
    {
        private Storyboard _scaleAnimation = new Storyboard();

        public TextBlock TextBlock
        {
            get
            {
                return (_content as Viewbox).Child as TextBlock;
            }
        }

        /// <summary>
        /// Adds textblock to visual tree.
        /// </summary>
        internal override void InitUI()
        {
            var textblock = new TextBlock();

            // View box is needed for correct work of sublings logic.
            var viewBox = new Viewbox() { StretchDirection = StretchDirection.DownOnly, HorizontalAlignment = HorizontalAlignment.Left };
            viewBox.Child = textblock;

            _content = viewBox;

            Children.Add(_content);

            base.InitUI();
        }

        /// <summary>
        /// Sets new text to FadeInOutTextBlock. With or without animation.
        /// </summary>
        /// <param name="newValue">new text</param>
        private async void UpdateText(string newValue)
        {
            if (newValue != null)
            {
                await FadeInOutAnimation(() =>
                {
                    TextBlock.Text = newValue;
                    if (Sibling != null)
                    {
                        KeepControlsSameSize(TextBlock, Sibling.TextBlock);
                    }
                },
                string.IsNullOrWhiteSpace(newValue));

            }
        }

        /// <summary>
        /// Keeps the same size of 2 controls.
        /// </summary>
        /// <param name="currentControl">Control, that has changed.</param>
        /// <param name="siblingControl">Brother control, that should be updated.</param>
        private void KeepControlsSameSize(FrameworkElement currentControl, FrameworkElement siblingControl)
        {
            double previousWidth = currentControl.ActualWidth > siblingControl.ActualWidth ? currentControl.ActualWidth : siblingControl.ActualWidth;
            currentControl.MinWidth = previousWidth;
            siblingControl.MinWidth = previousWidth;

            // Force control remeasure itself.
            currentControl.Measure(new Size(int.MaxValue, int.MaxValue));

            var newWidth = Math.Max(currentControl.ActualWidth, siblingControl.ActualWidth);

            _scaleAnimation.StopAndClear();

            _scaleAnimation.ChangeSizePropertyAnimation(currentControl, "MinWidth", previousWidth, newWidth, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));
            _scaleAnimation.ChangeSizePropertyAnimation(siblingControl, "MinWidth", previousWidth, newWidth, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));
            _scaleAnimation.ChangeSizePropertyAnimation(currentControl, "Width", currentControl.ActualWidth, newWidth, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));
            _scaleAnimation.ChangeSizePropertyAnimation(siblingControl, "Width", siblingControl.ActualWidth, newWidth, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));

            _scaleAnimation.Begin();
        }

        #region Text
        /// <summary>
        /// Property to set the text value of the text block
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FadeInOutTextBlock), new PropertyMetadata(null, TextUpdated));

        private static void TextUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FadeInOutTextBlock)d).UpdateText((string)e.NewValue);
        }

        #endregion

        #region Sibling
        /// <summary>
        /// Sibling is the property to set another text block that needs to be animated at the same time and to keep the same width
        /// </summary>
        public FadeInOutTextBlock Sibling
        {
            get { return (FadeInOutTextBlock)GetValue(SiblingProperty); }
            set { SetValue(SiblingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Sibling.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SiblingProperty =
            DependencyProperty.Register("Sibling", typeof(FadeInOutTextBlock), typeof(FadeInOutTextBlock), new PropertyMetadata(null));


        #endregion

        #region Styling
        /// <summary>
        /// Property to set the forntsize of the text block
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(FadeInOutTextBlock), new PropertyMetadata(12, FontSizeChanged));

        private static void FontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FadeInOutTextBlock)d).TextBlock.FontSize = (double)e.NewValue;
        }
        /// <summary>
        /// Property to set the fornt family of the text block
        /// </summary>
        public FontFamily FontFamily
        {
            get { return (FontFamily)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.Register("FontFamily", typeof(FontFamily), typeof(FadeInOutTextBlock), new PropertyMetadata(null, FontFamilyChanged));

        private static void FontFamilyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FadeInOutTextBlock)d).TextBlock.FontFamily = (FontFamily)e.NewValue;
        }

        /// <summary>
        /// Property to set the foreground of the text block
        /// </summary>
        public SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Foreground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(SolidColorBrush), typeof(FadeInOutTextBlock), new PropertyMetadata(null, ForegroundChanged));

        private static void ForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FadeInOutTextBlock)d).TextBlock.Foreground = (SolidColorBrush)e.NewValue;
        }

        #endregion
    }
}
