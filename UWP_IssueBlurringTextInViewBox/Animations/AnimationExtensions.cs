using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace UWP_IssueBlurringTextInViewBox.Animations
{
    public static class AnimationExtensions
    {
        /// <summary>
        /// Adds fade out animation, i.e. changes opacity from 1 to 0.
        /// </summary>
        /// <param name="control">UI element, that should be animated.</param>
        /// <param name="duration">Duration of animation.</param>
        public static void FadeOutAnimation(this Storyboard storyboard,
                                            DependencyObject control,
                                            double duration = 0.5)
        {
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = 1,
                To = 0
            };

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, control);
            Storyboard.SetTargetProperty(animation, "Opacity");
        }

        /// <summary>
        /// Adds fade in animation, i.e. changes opacity from 0 to 1.
        /// </summary>
        /// <param name="control">UI element, that should be animated.</param>
        /// <param name="duration">Duration of animation.</param>
        public static void FadeInAnimation(this Storyboard storyboard,
                                           DependencyObject control,
                                           double duration = 0.5)
        {
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = 0,
                To = 1
            };

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, control);
            Storyboard.SetTargetProperty(animation, "Opacity");
        }

        /// <summary>
        /// Stops and clears children in storyboard.
        /// </summary>
        public static void StopAndClear(this Storyboard storyboard)
        {
            storyboard.Stop();
            storyboard.Children.Clear();
        }

        /// <summary>
        /// Scales 1 size property. Can be performance-critical! Try to use ScaleX and ScaleY whenever is possible.
        /// </summary>
        /// <param name="control">UI element, that should be animated.</param>
        /// <param name="property">Can be min, max and normal Width, Height.</param>
        /// <param name="startWidth">Starting width.</param>
        /// <param name="endWidth">Ending width.</param>
        /// <param name="duration">Duration of animation.</param>
        /// <param name="repeat">How much times animation should be repeated.</param>
        public static void ChangeSizePropertyAnimation(this Storyboard storyboard,
            DependencyObject control,
            string property,
            double startValue,
            double endValue,
            double duration = 0.5,
            RepeatBehavior repeat = new RepeatBehavior())
        {
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = startValue,
                To = endValue,
                RepeatBehavior = repeat,
                EnableDependentAnimation = true
            };

            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, control);
            Storyboard.SetTargetProperty(animation, property);
        }
    }
}
