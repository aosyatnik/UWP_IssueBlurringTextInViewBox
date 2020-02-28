# UWP_IssueBlurringTextInViewBox
This repo demonstrates blurring of text.

# How it works
I want to share with you one of controls, that was developed for one of my projects. It's some kind of textblock, that is animated and can be pairwise scalled with another textblock. Check video how it updates with 0 rotation:
![all_good](Images/pairwise-scalling.gif?raw=true "Title")

# Steps to reproduce
1. Run app.
2. Type in Name_A text box next text: 12345678. (Notice 2 textblocks - fine)
3. Type in Name_B text box next text: 12345678. (Notice another 2 textblocks, BUT 1 and 3 textblocks are blurred now):

![bug](Images/bug.JPG?raw=true "Title")

Or you may try with texts, maybe it will be more clear:

![bug](Images/bug2.JPG?raw=true "Title")

So, the first text of pair is always blurred.

4. Now go to MainPage.xaml and change `Angle="270"` to `Angle="0"`. Notice, that all 4 textblocks are fine and not blurred.
