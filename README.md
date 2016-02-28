# C# Wordpress Post with GUI

# 0. Adding wordpress information

  User can add wordpress information (URL, Username, Password) manually also using with browse button user can select a text file and add to list. If there is a repetation on the text file, these lines are ignored.
  
![Screenshot1](Screenshot/screenshot1.PNG)

# 1. Selecting wordpress

  In this section, user can select wordpress URL to send a post.

![Screenshot1](Screenshot/screenshot2.PNG)

# 2. Getting post from a text file

  User can browse and select a text file, this text file has a structure. According this structure;<br/>
  First line = Title<br/>
  Second line = Category (Must be split with ',' character)<br/>
  Third line = Tags (Must be split with ',' character)<br/>
  Fourth line = Ping (This line must be 'open' or 'closed')<br/>
  Fifth line = Comment (This line must be 'open' or 'closed')<br/>
  Sixth line = Publish Date (If date is in the future, post will be automaticly scheduled to publish)<br/>
  Seventh line = Content is starting from this line until the end of the file.

![Screenshot1](Screenshot/screenshot3.PNG)

# 3. Report section

  In the report section, user can see the reports in each post.<br/>
  User can see the basic statistics.<br/>
  User can save the reports into a text file.<br/>
  User can send the reports to a mail adress. (Default sender mail is csharpwordpresspost@gmail.com)

![Screenshot1](Screenshot/screenshot4.PNG)

# 4. About section

![Screenshot1](Screenshot/screenshot5.PNG)

# 5. Licence

GNU General Public Licence v3.0
http://www.gnu.org/licenses/gpl-3.0.en.html
