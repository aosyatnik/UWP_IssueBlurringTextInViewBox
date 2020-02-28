using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Threading.Tasks;
using UWP_IssueBlurringTextInViewBox.Animations;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UWP_IssueBlurringTextInViewBox.CustomTextBlockControl
{
    public abstract class BaseFadeInOutAnimationControl : Grid
    {
        protected const double DEFAULT_ANIMATION_DURATION = 0.5;
        private readonly Storyboard _showStoryBoard;
        private readonly Storyboard _hideStoryBoard;
        protected FrameworkElement _content;

        public BaseFadeInOutAnimationControl()
        {
            _showStoryBoard = new Storyboard();
            _hideStoryBoard = new Storyboard();

            InitUI();
        }

        internal virtual void InitUI()
        {
            MaxHeight = 0;
        }

        public bool IsHidden { get; private set; } = true;

        private async Task FadeOutOpacity(double duration = DEFAULT_ANIMATION_DURATION, bool shouldCompletelyHide = false)
        {
            if (_hideStoryBoard.GetCurrentState() != ClockState.Stopped)
            {
                _hideStoryBoard.StopAndClear();
            }

            // Animate opacity.
            _hideStoryBoard.FadeOutAnimation(this, duration);
            await _hideStoryBoard.BeginAsync();
            Opacity = 0;
            _hideStoryBoard.StopAndClear();

            // Animate height to 0, if control should be comletely hidden.
            if (shouldCompletelyHide)
            {
                _hideStoryBoard.ChangeSizePropertyAnimation(this, "Height", ActualHeight, 0, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));
                await _hideStoryBoard.BeginAsync();

                MaxHeight = 0;
                IsHidden = true;

                _hideStoryBoard.StopAndClear();
            }
        }

        private async Task FadeInOpacity(double duration = DEFAULT_ANIMATION_DURATION)
        {
            if (_showStoryBoard.GetCurrentState() != ClockState.Stopped)
            {
                _showStoryBoard.StopAndClear();
            }

            // Show control if it was hidden.
            if (IsHidden)
            {
                // Enforce measure to get new DesiredSize.
                MaxHeight = int.MaxValue;
                Measure(new Size(int.MaxValue, int.MaxValue));

                _showStoryBoard.ChangeSizePropertyAnimation(this, "Height", 0, DesiredSize.Height, DEFAULT_ANIMATION_DURATION, new RepeatBehavior(1));
                await _showStoryBoard.BeginAsync();

                _showStoryBoard.StopAndClear();
                IsHidden = false;
            }

            // Animate opacity.
            _showStoryBoard.FadeInAnimation(this, duration);
            await _showStoryBoard.BeginAsync();
            Opacity = 1;
            _showStoryBoard.StopAndClear();
        }

        // <summary>
        /// Animates any control with the help of FadeInOutAnimationControl.
        /// </summary>
        /// <param name="animationControl">Custom FadeInOutAnimationControl</param>
        /// <param name="bindingUpdate">Callback, that should update bindings</param>
        /// <param name="shouldCompletelyHide">If set to true Height of FadeInOutAnimationControl will be also animated from actual value to 0</param>
        public async Task FadeInOutAnimation(Action bindingUpdate, bool shouldCompletelyHide = false, double duration = DEFAULT_ANIMATION_DURATION)
        {
            // Hide control.
            await FadeOutOpacity(duration, shouldCompletelyHide);

            // Update bindings.
            bindingUpdate();

            if (shouldCompletelyHide)
            {
                return;
            }

            // Wait until bindings updated. It's almost simultaneously with update of Text property on textBlock.
            // But without this await we are too fast and ActualSize is not updated.
            // 300 milliseconds should be enough. User won't notice it.
            await Task.Delay(300);

            await FadeInOpacity(duration);
        }
    }
}
