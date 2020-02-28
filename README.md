# UWP_IssueBlurringTextInViewBox
This repo demonstrates blurring of text.

# Steps to reproduce
1. Run app.
2. Type in Name_A text box next text: 12345678. (Notice 2 textblocks - fine)
3. Type in Name_B text box next text: 12345678. (Notice another 2 textblocks, BUT 1 and 3 textblocks are blurred now):
[Picture here]
4. Now go to MainPage.xaml and change `Angle="270"` to `Angle="0"`. Notice, that all 4 textblocks are fine and not blurred.
